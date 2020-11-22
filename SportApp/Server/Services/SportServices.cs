using SportApp.Shared.ViewModel;
using Common.DAL;
using Common.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportApp.Server.Services
{
    public interface ISportServices
    {
        public Sport[] GetSports();
    }

    public class SportServices : ISportServices
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public Sport[] GetSports()
        {
            var sports = _unitOfWork.SportRepository.Get().ToArray();

            return sports;
        }
    }
}   
