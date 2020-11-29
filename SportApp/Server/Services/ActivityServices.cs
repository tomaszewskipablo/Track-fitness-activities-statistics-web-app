using SportApp.Shared.ViewModel;
using Common.DAL;
using Common.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using SportApp.Server.MetCalculation;
using SportApp.Shared.ViewModel;


namespace SportApp.Server.Services
{
    public interface IActivityServices
    {
        public void PostActivityStats(TrainingSession TrainingSession);
        public int ProcessActivity(Activity activity);
        public List<CaloriesGraph> GetCalories(int trainingSessionId);
    }

    public class ActivityServices : IActivityServices
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public void PostActivityStats(TrainingSession TrainingSession)
        {
            try
            {
                _unitOfWork.TrainingSessionRepository.Insert(TrainingSession);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                ;
            }
        }

        public int ProcessActivity(Activity activity)
        {
            double HarrisBenedictBmr = _unitOfWork.UsersRepository.GetByID(activity.UserId).HarrisBenedictBmr;
            double Weight = _unitOfWork.UsersRepository.GetByID(activity.UserId).Weightkg;

            double CorrectedMetcoefficent = HarrisBenedictBmr / 7.2 / Weight;
            double CorrectedMet; // CorrectedMet = MET * 3.5/CorrectedMetcoefficent

            List<TrainingData> TrainingData = new List<TrainingData>();

            TrainingSession TrainingSession = new TrainingSession();

            // TRAINING SESSION Agregacja
            TrainingSession.DistanceMeters = 0;
            TrainingSession.DurationSeconds = 0;
            TrainingSession.AverageHeartRateBpm = 0;

            foreach (var lap in activity.Laps)
            {
                TrainingSession.DurationSeconds += lap.TotalTimeSeconds;
                TrainingSession.DistanceMeters += lap.DistanceMeters;
                TrainingSession.AverageHeartRateBpm += lap.AverageHeartRateBpm;
            }

            TrainingSession.AverageVelocitykmh = (double)(TrainingSession.DistanceMeters / TrainingSession.DurationSeconds * 3.6);
            // TRAINING SESSION Agregacja

            //double Met = _unitOfWork.MetRepository.Get(x => x.SportId == activity.Id
            //&& x.MinSpeedkmh < TrainingSession.AverageVelocitykmh && x.Speedkmh > TrainingSession.AverageVelocitykmh)
            //.Select(x => x.Value).FirstOrDefault();

            TrainingSession.SportId = activity.Id;
            TrainingSession.UserId = activity.UserId;
            TrainingSession.StartingTime = DateTime.Parse(activity.Laps[0].Tracks[0].TrackPoints[0].Timex);
            TrainingSession.Calories = 0;
            //CorrectedMet = Met * 3.5 / CorrectedMetcoefficent;



            _unitOfWork.TrainingSessionRepository.Insert(TrainingSession);
            _unitOfWork.Save();

            bool isVelocity = _unitOfWork.SportRepository.Get(x => x.Id == activity.Id).Select(x => x.IsVelocity).FirstOrDefault();
            if (!isVelocity)
            {
                TrainingData trainingData = new TrainingData();
                trainingData.TrainingSessionId = TrainingSession.Id;
                trainingData.Velocitykmh = 0;
                trainingData.Time = TrainingSession.DurationSeconds;
                //trainingData.Calories =
                double met = _unitOfWork.MetRepository.Get(x => x.SportId == activity.Id).Select(x => x.Value).FirstOrDefault();
                CorrectedMet = met * 3.5 / CorrectedMetcoefficent;

                TrainingSession.Calories = CorrectedMet * Weight * TrainingSession.DurationSeconds / 3600; // kcal = CorectedMET * kg * h
                trainingData.Calories = CorrectedMet * Weight / 60; // average for minute
                _unitOfWork.TrainingDataRepository.Insert(trainingData);
                _unitOfWork.TrainingSessionRepository.Update(TrainingSession);
                _unitOfWork.Save();                
            }
            else
            {
                List<TrackPoint> Training = new List<TrackPoint>();

                foreach (var lap in activity.Laps)
                {
                    foreach (var track in lap.Tracks)
                    {
                        foreach (var trackPoint in track.TrackPoints)
                        {
                            Training.Add(trackPoint);
                        }
                    }
                }

                int trackDuration = (int)Training.Count / 100;   // co ile pomiarów wpis do bazy
                //if (Training.Count < trackDuration)   // jesli ilosc pomiarow mniejsza nic trackDuration to policzmy 2 odcinki przynajmniej
                //    trackDuration = Training.Count / 2;

                double CaloriesALL = 0;
                double PreviousDistance = 0;
                double timeFromBegening = 0;
                double Met = 0;
                List<TrainingData> trainingData = new List<TrainingData>();
                List<Met> MetTable = _unitOfWork.MetRepository.Get(x => x.SportId == activity.Id).ToList();
                MetTable.Sort((t1, t2) => t1.Value.CompareTo(t2.Value));

                int numberOfPoints = Training.Count / trackDuration;

                for (int i = 1; i < numberOfPoints; i++)
                {
                    TrainingData point = new TrainingData();
                    point.DistanceMeters = 0;

                    double time = (DateTime.Parse(Training[i * trackDuration].Timex) - DateTime.Parse(Training[(i - 1) * trackDuration].Timex)).TotalSeconds; // czas odcinka
                    timeFromBegening += time;
                    point.Velocitykmh = (Training[i * trackDuration].DistanceMeters - PreviousDistance) / time * 3.6;
                    PreviousDistance = Training[i * trackDuration].DistanceMeters;


                    MetVelocity metVelocity = new MetVelocity();

                    Met = metVelocity.MetBasedOnVelocity(MetTable, (double)point.Velocitykmh);

                    CorrectedMet = Met * 3.5 / CorrectedMetcoefficent;
                    point.Calories = CorrectedMet * Weight * time / 3600; // kcal = CorectedMET * kg * h
                    point.Time = timeFromBegening;
                    point.TrainingSessionId = TrainingSession.Id;


                    CaloriesALL += (double)point.Calories;

                    point.Calories = point.Calories / time * 60; // ilosc kalorii spalana przez minute przy tej intensywności na tym odcniku czasu
                    _unitOfWork.TrainingDataRepository.Insert(point);
                }


                TrainingSession.Calories = CaloriesALL;

                _unitOfWork.TrainingSessionRepository.Update(TrainingSession);
                _unitOfWork.Save();
            }
            return TrainingSession.Id;
        }

        public List<CaloriesGraph> GetCalories(int trainingSessionId)
        {
            List<CaloriesGraph> calories = _unitOfWork.TrainingDataRepository.Get(x => x.TrainingSessionId == trainingSessionId).Select(x => new CaloriesGraph(x.Calories, (double)x.Time)).ToList();

            return calories;
        }
    }
}
