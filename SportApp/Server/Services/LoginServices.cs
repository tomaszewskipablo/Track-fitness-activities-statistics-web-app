using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportApp.Shared.ViewModel;
using Common.DAL;
using Common.DAL.Models;

namespace SportApp.Server.Services
{
    public interface ILoginServices
    {
        public List<Users> GetUsers(int id);
    }

    public class LoginServices : ILoginServices
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public List<Users> GetUsers(int id)
        {
            var table = _unitOfWork.UsersRepository.Get().Distinct().ToList();
            return table;
        }
    }
}
