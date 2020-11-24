using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class Users
    {
        public Users()
        {
            Goals = new HashSet<Goals>();
            TrainingSession = new HashSet<TrainingSession>();
        }

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
        public int? Hrmax { get; set; }
        public int? AverageCalories { get; set; }
        public double HarrisBenedictBmr { get; set; }

        public virtual ICollection<Goals> Goals { get; set; }
        public virtual ICollection<TrainingSession> TrainingSession { get; set; }
    }
}
