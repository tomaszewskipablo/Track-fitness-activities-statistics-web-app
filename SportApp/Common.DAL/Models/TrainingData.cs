using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class TrenningData
    {
        public int Id { get; set; }
        public int? TrenningSessionId { get; set; }
        public DateTime Time { get; set; }
        public double? LatitudeDegrees { get; set; }
        public double? LongitudeDegrees { get; set; }
        public double? AltitudeMeters { get; set; }
        public double? DistanceMeters { get; set; }
        public double? Velocitykmh { get; set; }
        public double? ElevationMeters { get; set; }
        public int? HeartRateBpm { get; set; }

        public virtual TrenningSession TrenningSession { get; set; }
    }
}
