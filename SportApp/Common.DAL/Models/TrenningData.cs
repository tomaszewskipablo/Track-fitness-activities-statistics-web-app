using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class TrenningData
    {
        public int Id { get; set; }
        public int? TrenningSessionId { get; set; }
        public DateTime Time { get; set; }
        public int? LatitudeDegrees { get; set; }
        public int? LongitudeDegrees { get; set; }
        public int? AltitudeMeters { get; set; }
        public int? DistanceMeters { get; set; }
        public int? HeartRateBpm { get; set; }

        public virtual TrenningSession TrenningSession { get; set; }
    }
}
