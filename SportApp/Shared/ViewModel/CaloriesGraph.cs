using System;
using System.Collections.Generic;
using System.Text;

namespace SportApp.Shared.ViewModel
{
    public class CaloriesGraph
    {
        public CaloriesGraph(double calories, double caloriesHR, double time)
        {
            Calories = calories;
            CaloriesHR = caloriesHR;
            Time = time;
        }

        public double Calories { get; set; }
        public double CaloriesHR { get; set; }
        public double Time { get; set; }
    }
}
