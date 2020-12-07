using SportApp.Shared.ViewModel;
using Common.DAL;
using Common.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using SportApp.Server.MetCalculation;
using SportApp.Server.Helpers;


namespace SportApp.Server.Services
{
    public interface IActivityServices
    {
        public void PostActivityStats(TrainingSession TrainingSession);
        public int ProcessActivity(Activity activity);
        public List<CaloriesGraph> GetCalories(int trainingSessionId);
        public TrainingSession[] GetTrainingSession(int usernId);
        public void DeleteTrainingSession(int trainingSession);
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

            TrainingSession TrainingSession = new TrainingSession();

            foreach (var lap in activity.Laps)
            {
                TrainingSession.DurationSeconds += lap.TotalTimeSeconds;
                TrainingSession.DistanceMeters += lap.DistanceMeters;
                TrainingSession.AverageHeartRateBpm += lap.AverageHeartRateBpm;
            }

            if (TrainingSession.DistanceMeters == 0)
                if (_unitOfWork.SportRepository.GetByID(activity.Id).IsVelocity)
                    return -1;  // error, activity with no distance was choosen as velocity activity

            TrainingSession.AverageVelocitykmh = (double)(TrainingSession.DistanceMeters / TrainingSession.DurationSeconds * 3.6);
            TrainingSession.SportId = activity.Id;
            TrainingSession.UserId = activity.UserId;
            TrainingSession.StartingTime = DateTime.Parse(activity.Laps[0].Tracks[0].TrackPoints[0].Timex);

            _unitOfWork.TrainingSessionRepository.Insert(TrainingSession);
            _unitOfWork.Save();

            bool isVelocity = _unitOfWork.SportRepository.Get(x => x.Id == activity.Id).Select(x => x.IsVelocity).FirstOrDefault();
            if (!isVelocity)
            {
                TrainingData trainingData = new TrainingData();
                trainingData.TrainingSessionId = TrainingSession.Id;
                trainingData.Time = TrainingSession.DurationSeconds;

                double met = _unitOfWork.MetRepository.Get(x => x.SportId == activity.Id).Select(x => x.Value).FirstOrDefault();
                CorrectedMet = met * 3.5 / CorrectedMetcoefficent;

                TrainingSession.Calories = CorrectedMet * Weight * TrainingSession.DurationSeconds / 3600; // kcal = CorectedMET * kg * h
                trainingData.Calories = CorrectedMet * Weight / 60; // average kcal/minute
                _unitOfWork.TrainingDataRepository.Insert(trainingData);
                _unitOfWork.TrainingSessionRepository.Update(TrainingSession);
                _unitOfWork.Save();
            }
            else
            {
                List<TrackPoint> Training = StaticMethods.LapsToTrackPoints(activity);

                if (Training.Count < 1)                    
                        return -1;

                int trackDuration = (int)Training.Count / 100;   // co ile pomiarów wpis do bazy

                double CaloriesALL = 0;
                double CoveredDistance = 0;
                double timeFromBegening = 0;
                double distanceFromBegening = 0;
                double Met = 0;
                List<TrainingData> trainingData = new List<TrainingData>();
                List<Met> MetTable = _unitOfWork.MetRepository.Get(x => x.SportId == activity.Id).ToList();
                MetTable.Sort((t1, t2) => t1.Value.CompareTo(t2.Value));

                int numberOfPoints = Training.Count / trackDuration;
                int lastPeriod = 0; // dla ostatniej iteracji

                for (int i = 1; i < numberOfPoints; i++)
                {
                    if(i == numberOfPoints - 1) // ostatnia iteracja
                        lastPeriod = Training.Count - (numberOfPoints * trackDuration);

                    TrainingData point = new TrainingData();
                    point.DistanceMeters = 0;

                    double time = (DateTime.Parse(Training[i * trackDuration + lastPeriod].Timex) - DateTime.Parse(Training[(i - 1) * trackDuration].Timex)).TotalSeconds; // czas odcinka
                    timeFromBegening += time;
                    point.Velocitykmh = (Training[i * trackDuration + lastPeriod].DistanceMeters - CoveredDistance) / time * 3.6;
                    CoveredDistance = Training[i * trackDuration + lastPeriod].DistanceMeters;

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
                TrainingSession.DurationSeconds = timeFromBegening;
                TrainingSession.AverageVelocitykmh = CoveredDistance / timeFromBegening * 3.6;

                _unitOfWork.TrainingSessionRepository.Update(TrainingSession);
                _unitOfWork.Save();
            }
            return TrainingSession.Id;
        }

        public List<CaloriesGraph> GetCalories(int trainingSessionId)
        {
            List<CaloriesGraph> calories = _unitOfWork.TrainingDataRepository.Get(x => x.TrainingSessionId == trainingSessionId).Select(x => new CaloriesGraph((double)x.Calories, (double)x.Time)).ToList();

            return calories;
        }

        public TrainingSession[] GetTrainingSession(int usernId)
        {
            TrainingSession[] calories = _unitOfWork.TrainingSessionRepository.Get(x => x.UserId == usernId, includeProperties: "Sport").OrderByDescending(x => x.StartingTime).ToArray();

            return calories;
        }
        public void DeleteTrainingSession(int trainingSession)
        {
            _unitOfWork.TrainingSessionRepository.Delete(trainingSession);
            _unitOfWork.TrainingDataRepository.Delete(x=>x.TrainingSessionId == trainingSession);
            _unitOfWork.Save();
        }
    }
}
