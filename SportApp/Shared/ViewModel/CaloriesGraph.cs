using System;
using System.Collections.Generic;
using System.Text;

namespace SportApp.Shared.ViewModel
{
    public class CaloriesGraph
    {
        public CaloriesGraph(double calories, double time)
        {
            Calories = calories;
            Time = time;
        }

        public double Calories { get; set; }
        public double Time { get; set; }
    }
}
