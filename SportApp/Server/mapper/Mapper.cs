using AutoMapper;
using Common.DAL.Models;
using SportApp.Shared.ViewModel;
using System.Security.Cryptography.X509Certificates;

namespace NCR.Server.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Users, UserDTO>();
            CreateMap<UserDTO, Users>();
        }
    }
}
