using System;
using System.ComponentModel.DataAnnotations;

namespace SportApp.Shared.Authenticate
{
    public class SignupRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool IsMan { get; set; }
        [Required]
        public int Heightcm { get; set; }
        [Required]
        public int Weightkg { get; set; }
        public double HarrisBenedictBmr { get; set; }
        public int? Hrmax { get; set; }
        public int? AverageCalories { get; set; }
    }
}
