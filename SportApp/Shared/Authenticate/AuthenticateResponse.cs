using SportApp.Shared.ViewModel;
using System;
using System.Numerics;

namespace SportApp.Shared.Authenticate
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }

        public AuthenticateResponse() { }
    }
}
