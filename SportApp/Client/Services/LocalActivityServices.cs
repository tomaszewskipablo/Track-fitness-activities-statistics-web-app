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

        public async void ProcessActivity(Activity activity)
        {
            string json = JsonConvert.SerializeObject(activity);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _http.PostAsync("Activity/Process", httpContent);
        }
        public async Task<List<double>> GetCalories(int trainingSession)
        {
            var resultGetAll = await _http.GetAsync("Activity/Calories?trainingSession=" + trainingSession);
            if (!resultGetAll.IsSuccessStatusCode)
                return new List<double>();
            var jsonResultGetAll = await resultGetAll.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<double>>(jsonResultGetAll);
        }
    }
}

