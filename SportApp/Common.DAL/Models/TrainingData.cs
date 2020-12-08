using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class TrainingData
    {
        public int Id { get; set; }
        public int? TrainingSessionId { get; set; }
        public double? LatitudeDegrees { get; set; }
        public double? LongitudeDegrees { get; set; }
        public double? AltitudeMeters { get; set; }
        public double? DistanceMeters { get; set; }
        public double? Velocitykmh { get; set; } = 0;
        public double? ElevationMeters { get; set; }
        public int? HeartRateBpm { get; set; }
        public double CaloriesMet { get; set; }
        public double? Time { get; set; }
        public double? CaloriesHR { get; set; }

        public virtual TrainingSession TrainingSession { get; set; }
    }
}
