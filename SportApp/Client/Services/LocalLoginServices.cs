using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SportApp.Shared.Authenticate;
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

        public async Task<UserDTO> GetUser(int id)
        {
            var resultGetAll = await _http.GetAsync("Login?id="+id);
            if (!resultGetAll.IsSuccessStatusCode)
                return new UserDTO();
            var jsonResultGetAll = await resultGetAll.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDTO>(jsonResultGetAll);
        }

        public async Task<bool> Exist(AuthenticateRequest authenticateRequest)
        {
            var response = await _http.PostAsJsonAsync<AuthenticateRequest>("/Login/exist", authenticateRequest);
            if (!response.IsSuccessStatusCode)
                return false;
            var jsonResultGetAll = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(jsonResultGetAll);
        }

    }
}

