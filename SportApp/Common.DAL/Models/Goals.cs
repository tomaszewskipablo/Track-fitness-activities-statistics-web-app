using System;
using System.Collections.Generic;

namespace Common.DAL.Models
{
    public partial class Goals
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int Calories { get; set; }
        public bool Gain { get; set; }

        public virtual Users User { get; set; }
    }
}
