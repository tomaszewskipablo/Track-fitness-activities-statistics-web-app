using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportApp.Server
{
    public class HRCalculation
    {
        bool IsMale;
        double Weight;
        int Age;

        public HRCalculation(bool isMale, double weight, DateTime dateOfBirth)
        {
            IsMale = isMale;
            Weight = weight;

            Age = DateTime.Today.Year - dateOfBirth.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (dateOfBirth.Date > DateTime.Today.AddYears(-Age)) Age--;
        }

        public double HRCalories(double HeartRate, double time)
        {
            if (IsMale)
                return (-55.0969 + (0.6309 * HeartRate) + (0.1988 * Weight) + (0.2017 * Age)) / 4.184 / 60 * time;
            else
                return (-20.4022 + (0.4472 * HeartRate) - (0.1263 * Weight) + (0.074 * Age)) / 4.184 / 60 * time;
        }
    }
}
