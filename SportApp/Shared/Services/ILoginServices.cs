using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SportApp.Shared.ViewModel;
using SportApp.Shared.Authenticate;

namespace SportApp.Shared.Services
{
    public interface ILoginServices
    {
        public Task<UserDTO> GetUser(int id);
        public Task<bool> Exist(AuthenticateRequest authenticateRequest);
    }
}

