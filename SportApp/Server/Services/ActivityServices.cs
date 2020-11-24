using SportApp.Shared.ViewModel;
using Common.DAL;
using Common.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SportApp.Server.Services
{
    public interface IActivityServices
    {
        public void PostActivityStats(TrainingSession TrainingSession);
        public void ProcessActivity(Activity activity);
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

        public void ProcessActivity(Activity activity)
        {
            double HarrisBenedictBmr = _unitOfWork.UsersRepository.GetByID(activity.UserId).HarrisBenedictBmr;
            double Weight = _unitOfWork.UsersRepository.GetByID(activity.UserId).Weightkg;

            double CorrectedMetcoefficent = HarrisBenedictBmr / 7.2 / Weight;
            double CorrectedMet; // CorrectedMet = MET * 3.5/CorrectedMetcoefficent

            List<TrainingData> TrainingData = new List<TrainingData>();
            TrainingSession TrainingSession = new TrainingSession();

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

            double Met = _unitOfWork.MetRepository.Get(x => x.SportId == activity.Id
            && x.MinSpeedkmh < TrainingSession.AverageVelocitykmh && x.MaxSpeedkmh > TrainingSession.AverageVelocitykmh)
            .Select(x => x.Value).FirstOrDefault();

            TrainingSession.SportId = activity.Id;
            TrainingSession.UserId = activity.UserId;
            TrainingSession.StartingTime = DateTime.Parse(activity.Laps[0].Tracks[0].TrackPoints[0].Timex);

            CorrectedMet = Met * 3.5 / CorrectedMetcoefficent;

            TrainingSession.Calories = CorrectedMet * Weight * TrainingSession.DurationSeconds / 3600; // kcal = CorectedMET * kg * h

            _unitOfWork.TrainingSessionRepository.Insert(TrainingSession);
            _unitOfWork.Save();

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

            List<Met> MetTable = _unitOfWork.MetRepository.Get(x => x.SportId == activity.Id).ToList();
            double CaloriesALL = 0;
            double PreviousDistance=0;
            List<TrainingData> trainingData = new List<TrainingData>();
            int numberOfPoints = Training.Count / 10;
            for (int i = 1; i < numberOfPoints; i++) {
                TrainingData point = new TrainingData();
                point.DistanceMeters = 0;
                //for (int j = 0; j < 10; j++)
                //{
                //    point.DistanceMeters += Training[i*10+j].DistanceMeters;                                        
                //}


                point.Velocitykmh = (Training[i*10].DistanceMeters - PreviousDistance) / 10 * 3.6;
                PreviousDistance = Training[i * 10].DistanceMeters;

                Met = MetTable.Where(x => x.MinSpeedkmh < point.Velocitykmh && x.MaxSpeedkmh > point.Velocitykmh)
                    .Select(x=>x.Value).FirstOrDefault();
                CorrectedMet = Met * 3.5 / CorrectedMetcoefficent;
                point.Calories = CorrectedMet * Weight * 10 / 3600; // kcal = CorectedMET * kg * h
                point.AltitudeMeters = Training[i * 10].AltitudeMeters;
                point.LongitudeDegrees = Training[i * 10].Positionx[0].LongitudeDegrees;
                point.LatitudeDegrees = Training[i * 10].Positionx[0].LatitudeDegrees;
                point.Time = DateTime.Parse(Training[i * 10].Timex);
                point.TrainingSessionId = TrainingSession.Id;

                trainingData.Add(point);
                _unitOfWork.TrainingDataRepository.Insert(point);

                CaloriesALL += (double)point.Calories;
            }

            _unitOfWork.Save();
            double a = CaloriesALL;
        }
    }
}
