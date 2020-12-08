using SportApp.Shared.ViewModel;
using Common.DAL;
using Common.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using SportApp.Server.MetCalc;
using SportApp.Server.Helpers;
using SportApp.Server.MetCalc;

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
            TrainingSession TrainingSession = new TrainingSession();
            foreach (var lap in activity.Laps)
            {
                TrainingSession.DurationSeconds += lap.TotalTimeSeconds;
                TrainingSession.DistanceMeters += lap.DistanceMeters;
                TrainingSession.AverageHeartRateBpm += lap.AverageHeartRateBpm;
            }

            TrainingSession.AverageVelocitykmh = (double)(TrainingSession.DistanceMeters / TrainingSession.DurationSeconds * 3.6);
            TrainingSession.SportId = activity.Id;
            TrainingSession.UserId = activity.UserId;
            TrainingSession.StartingTime = DateTime.Parse(activity.Laps[0].Tracks[0].TrackPoints[0].Timex);

            if (TrainingSession.DistanceMeters == 0)
                if (_unitOfWork.SportRepository.GetByID(activity.Id).IsVelocity)
                    return -1;  // error, activity with no distance was choosen as velocity activity

            _unitOfWork.TrainingSessionRepository.Insert(TrainingSession);
            _unitOfWork.Save();

            List<Met> MetTable = _unitOfWork.MetRepository.Get(x => x.SportId == activity.Id).ToList();
            double HarrisBenedictBmr = _unitOfWork.UsersRepository.GetByID(activity.UserId).HarrisBenedictBmr;
            double Weight = _unitOfWork.UsersRepository.GetByID(activity.UserId).Weightkg;
            bool IsMan= _unitOfWork.UsersRepository.GetByID(activity.UserId).IsMan;
            DateTime DateOfBirth = _unitOfWork.UsersRepository.GetByID(activity.UserId).DateOfBirth;

            MetCalculation metCalculation = new MetCalculation(HarrisBenedictBmr, Weight, MetTable);
            HRCalculation hRCalculation = new HRCalculation(IsMan, Weight, DateOfBirth);
                List<TrackPoint> Training = StaticMethods.LapsToTrackPoints(activity);

                if (Training.Count < 1)                    
                        return -1;

                int trackDuration = (int)Training.Count / 100;   // co ile pomiarów wpis do bazy
                double CoveredDistance = 0;                

                int numberOfPoints = Training.Count / trackDuration;
                int lastPeriod = 0; // dla ostatniej iteracji
            double CaloriesAll = 0;

                for (int i = 1; i < numberOfPoints; i++)
                {
                    if(i == numberOfPoints - 1) // ostatnia iteracja
                        lastPeriod = Training.Count - (numberOfPoints * trackDuration);

                    TrainingData point = new TrainingData();
                    point.DistanceMeters = 0;

                    double time = (DateTime.Parse(Training[i * trackDuration + lastPeriod].Timex) - DateTime.Parse(Training[(i - 1) * trackDuration].Timex)).TotalSeconds; // czas odcinka
                    TrainingSession.DurationSeconds += time;
                    point.Velocitykmh = (Training[i * trackDuration + lastPeriod].DistanceMeters - CoveredDistance) / time * 3.6;
                    CoveredDistance = Training[i * trackDuration + lastPeriod].DistanceMeters;                    
                    
                    point.Time = TrainingSession.DurationSeconds;
                    point.TrainingSessionId = TrainingSession.Id;

                    point.Calories = metCalculation.MetCalories((double)point.Velocitykmh, time);
                    TrainingSession.Calories += (double)point.Calories;

                if (Training[i * trackDuration + lastPeriod]?.HeartRateBpm != 0)
                {
                    double l = hRCalculation.HRCalories(Training[i * trackDuration + lastPeriod].HeartRateBpm, time);
                    CaloriesAll += l;
                }


                    point.Calories = point.Calories / time * 60; // ilosc kalorii spalana przez minute przy tej intensywności na tym odcniku czasu
                    _unitOfWork.TrainingDataRepository.Insert(point);
                }
                
                
                TrainingSession.AverageVelocitykmh = CoveredDistance / TrainingSession.DurationSeconds * 3.6;

                _unitOfWork.TrainingSessionRepository.Update(TrainingSession);
                _unitOfWork.Save();
            
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
