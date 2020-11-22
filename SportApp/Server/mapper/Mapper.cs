using AutoMapper;
using Common.DAL.Models;
using SportApp.Shared.Authenticate;
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
            CreateMap<User, Users>();
            CreateMap<SportDTOCombobox, Sport>();
            CreateMap<Sport,SportDTOCombobox>();
            CreateMap<Users, SignupRequest>().ForMember(x=>x.Username,opt => opt.MapFrom(src=>src.Login));
            CreateMap<SignupRequest, Users>().ForMember(x => x.Login, opt => opt.MapFrom(src => src.Username));
        }
    }
}
