using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class TrainingSession
    {
        public TrainingSession()
        {
            TrainingData = new HashSet<TrainingData>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? SportId { get; set; }
        public DateTime StartingTime { get; set; }
        public double DurationSeconds { get; set; } = 0;
        public double? DistanceMeters { get; set; } = 0;
        public double? AverageVelocitykmh { get; set; } = 0;
        public int? AverageHeartRateBpm { get; set; }
        public double? ElevationMeters { get; set; }
        public double Calories { get; set; } = 0;

        public virtual Sport Sport { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<TrainingData> TrainingData { get; set; }
    }
}
