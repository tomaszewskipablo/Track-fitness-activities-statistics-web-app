using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SportApp.Shared.ViewModel;
using SportApp.Shared.Authenticate;

namespace SportApp.Client.IServices
{
    public interface ISportServices
    {
        public Task<SportDTOCombobox[]> GetSports();
    }
}