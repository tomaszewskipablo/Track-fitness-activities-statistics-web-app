using System;
using System.Collections.Generic;

namespace Common.DAL.Models { 
    public partial class Met
    {
        public int Id { get; set; }
        public int? SportId { get; set; }
        public double? Speedkmh { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
