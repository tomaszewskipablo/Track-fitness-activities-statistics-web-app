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
        public Task<int> ProcessActivity(Activity activity);
        public Task<List<CaloriesGraph>> GetCalories(int trainingSession);
    }
}

