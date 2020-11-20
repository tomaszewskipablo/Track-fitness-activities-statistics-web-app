using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class Sport
    {
        public Sport()
        {
            Met = new HashSet<Met>();
            TrainingSession = new HashSet<TrainingSession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsVelocity { get; set; }

        public virtual ICollection<Met> Met { get; set; }
        public virtual ICollection<TrainingSession> TrainingSession { get; set; }
    }
}
