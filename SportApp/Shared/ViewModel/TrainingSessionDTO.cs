﻿using System;
using System.Collections.Generic;

namespace SportApp.Shared.ViewModel
{
    public partial class TrainingSessionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SportId { get; set; }
        public string SportName { get; set; }
        public DateTime StartingTime { get; set; }
        public double DistanceMeters { get; set; }
        public double AverageVelocitykmh { get; set; }
        public double CaloriesMet { get; set; }
        public double CaloriesHR { get; set; }
        public double DurationSeconds { get; set; }
    }
}
