﻿using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class TrainingData
    {
        public int Id { get; set; }
        public int? TrainingSessionId { get; set; }
        public DateTime Time { get; set; }
        public double? LatitudeDegrees { get; set; }
        public double? LongitudeDegrees { get; set; }
        public double? AltitudeMeters { get; set; }
        public double? DistanceMeters { get; set; }
        public double? Velocitykmh { get; set; }
        public double? ElevationMeters { get; set; }
        public int? HeartRateBpm { get; set; }

        public virtual TrainingSession TrainingSession { get; set; }
    }
}
