using SportApp.Shared.ViewModel;
using Common.DAL;
using Common.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace SportApp.Server.Services
{
    public interface IActivityServices
    {
        public void PostActivityStats(TrenningSession trenningSession);
        public void ProcessActivity(Activity activity);
    }

    public class ActivityServices : IActivityServices
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        
        public void PostActivityStats(TrenningSession trenningSession)
        {
            try
            {
                _unitOfWork.TrenningSessionRepository.Insert(trenningSession);
                _unitOfWork.Save();
            }
            catch(Exception ex)
            {
                ;
            }
        }

        public void ProcessActivity(Activity activity)
        {
            double distanceM = 0;
            double timeS = 0;
            double velocity;
            double MET = 8;
            double weight = 70;
            foreach (var lap in activity.Laps)
            {
                distanceM += lap.DistanceMeters;
                timeS += lap.TotalTimeSeconds;
            }

            velocity = distanceM / timeS; // [m/s]

            double calories = timeS / 3600 * weight * MET;
        }        
    }
}   
