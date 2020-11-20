using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class Sport
    {
        public Sport()
        {
            Met = new HashSet<Met>();
            TrenningSession = new HashSet<TrenningSession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsVelocity { get; set; }

        public virtual ICollection<Met> Met { get; set; }
        public virtual ICollection<TrenningSession> TrenningSession { get; set; }
    }
}
