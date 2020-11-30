using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SportApp.Shared.Authenticate;
using SportApp.Shared.Services;
using SportApp.Shared.ViewModel;
using System.Text;

namespace SportApp.Client.Services
{

    public class LocalActivityServices : IActivityServices
    {
        private readonly HttpClient _http;
        public LocalActivityServices(HttpClient http)
        {
            _http = http;
        }

        public async void PostActivity(Activity activity)
        {
            string json = JsonConvert.SerializeObject(activity);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _http.PostAsync("TrenningSession/", httpContent);
        }

        public async Task<int> ProcessActivity(Activity activity)
        {
            string json = JsonConvert.SerializeObject(activity);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
           var resultGetAll = await _http.PostAsync("Activity/Process", httpContent);
            if (!resultGetAll.IsSuccessStatusCode)
                return 0;
            var jsonResultGetAll = await resultGetAll.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<int>(jsonResultGetAll);
        }
        public async Task<List<CaloriesGraph>> GetCalories(int trainingSession)
        {
            var resultGetAll = await _http.GetAsync("Activity/Calories?trainingSessionId=" + trainingSession);
            if (!resultGetAll.IsSuccessStatusCode)
                return new List<CaloriesGraph>();
            var jsonResultGetAll = await resultGetAll.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CaloriesGraph>>(jsonResultGetAll);
        }

        public async Task<TrainingSessionDTO[]> GetTrainingSession(int user)
        {
            var resultGetAll = await _http.GetAsync("Activity/Session?userId=" + user);
            if (!resultGetAll.IsSuccessStatusCode)
                return new TrainingSessionDTO[] { };
            var jsonResultGetAll = await resultGetAll.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TrainingSessionDTO[]>(jsonResultGetAll);
        }

        public async Task<List<CaloriesGraph>> GetVelocity(int trainingSession)
        {
            var resultGetAll = await _http.GetAsync("Activity/Velocity?trainingSession=" + trainingSession);
            if (!resultGetAll.IsSuccessStatusCode)
                return new List<CaloriesGraph>();
            var jsonResultGetAll = await resultGetAll.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CaloriesGraph>>(jsonResultGetAll);
        }
    }
}

