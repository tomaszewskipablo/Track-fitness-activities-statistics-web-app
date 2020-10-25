using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SportApp.Shared.ViewModel;

namespace SportApp.Shared.Services
{
    public interface ILoginServices
    {
        public Task<List<UserDTO>> GetUser(int id);
    }
}

