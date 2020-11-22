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

    public class LocalSportServices : ISportServices
    {
        private readonly HttpClient _http;
        public LocalSportServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<SportDTOCombobox[]> GetSports()
        {
            var resultGetAll = await _http.GetAsync("Sport");
            if (!resultGetAll.IsSuccessStatusCode)
                return new SportDTOCombobox[0];
            var jsonResultGetAll = await resultGetAll.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SportDTOCombobox[]>(jsonResultGetAll);
        }
    }
}

