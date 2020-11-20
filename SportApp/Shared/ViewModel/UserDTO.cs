using System;
using System.Collections.Generic;
using System.Text;

namespace SportApp.Shared.ViewModel
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsMan { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public UserDTO() { }
    }
}
