using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportApp.Shared.ViewModel;

namespace SportApp.Client.MetCalculation
{
    public class HarrisBenedictBMR
    {
        public bool IsMan { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }
        public double BMR { get; set; }

        public HarrisBenedictBMR(bool _isMan, int _height, int _weight, DateTime _dateOfBirth)
        {
            IsMan = _isMan;
            Height = _height;
            Weight = _weight;
            Age = DateTime.Today.Year - _dateOfBirth.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (_dateOfBirth.Date > DateTime.Today.AddYears(-Age)) Age--;
        }
        
        public double CalculateBMR()
        {
            if (IsMan)
            {
                BMR = 66.4730 + 5.0033 * Height + 13.7516 * Weight - 6.7550 * Age;
            }
            else
            {
                BMR = 655.0955 + 1.8496 * Height + 9.5634 * Weight - 4.6756 * Age;
            }

            return BMR;
        }
    }
}
