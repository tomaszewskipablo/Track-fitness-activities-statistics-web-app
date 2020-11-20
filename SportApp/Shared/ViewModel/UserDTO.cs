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
        public int Heightcm { get; set; }
        public int Weightkg { get; set; }
        public double HarrisBenedictBmr { get; set; }
        public UserDTO() { }
    }
}
