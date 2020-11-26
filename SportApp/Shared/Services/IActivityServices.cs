using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SportApp.Shared.ViewModel;
using SportApp.Shared.Authenticate;

namespace SportApp.Shared.Services
{
    public interface IActivityServices
    {
        public void PostActivity(Activity activity);
        public void ProcessActivity(Activity activity);
        public Task<List<double>> GetCalories(int trainingSession);
    }
}

