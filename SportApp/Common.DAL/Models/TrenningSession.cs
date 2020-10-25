using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class TrenningSession
    {
        public TrenningSession()
        {
            TrenningData = new HashSet<TrenningData>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime StartingTime { get; set; }
        public int Duration { get; set; }
        public int? Distance { get; set; }
        public int? AverageHeartRateBpm { get; set; }
        public int Calories { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<TrenningData> TrenningData { get; set; }
    }
}
