using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class TrenningSession
    {
        public TrenningSession()
        {
            TrenningData = new HashSet<TrainingData>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? SportId { get; set; }
        public DateTime StartingTime { get; set; }
        public double DurationSeconds { get; set; }
        public double? DistanceMeters { get; set; }
        public double? AverageVelocitykmh { get; set; }
        public int? AverageHeartRateBpm { get; set; }
        public double? ElevationMeters { get; set; }
        public double? Calories { get; set; }

        public virtual Sport Sport { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<TrainingData> TrenningData { get; set; }
    }
}
