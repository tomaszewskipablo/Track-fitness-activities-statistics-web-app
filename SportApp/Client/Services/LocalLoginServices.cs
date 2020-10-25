using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SportApp.Shared.Services;
using SportApp.Shared.ViewModel;

namespace SportApp.Client.Services
{

    public class LocalLoginServices : ILoginServices
    {
        private readonly HttpClient _http;
        public LocalLoginServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserDTO>> GetUser(int id)
        {
            var resultGetAll = await _http.GetAsync("Login?id="+id);
            if (!resultGetAll.IsSuccessStatusCode)
                return new List<UserDTO>();
            var jsonResultGetAll = await resultGetAll.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserDTO>>(jsonResultGetAll);
        }

    }
}

