using Common.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportApp.Server.MetCalc
{
    public class MetCalculation
    {
        List<Met> MetTable;
        MetVelocity metVelocity = new MetVelocity();
        double HarrisBenedictBmr;
        double Weight;
        double CorrectedMet;

        public MetCalculation(double harrisBenedictBmr, double weight, List<Met> metTable)
        {
            HarrisBenedictBmr = harrisBenedictBmr;
            Weight = weight;
            MetTable = metTable;
            MetTable.Sort((t1, t2) => t1.Value.CompareTo(t2.Value));
        }
        public double MetCalories(double velocity, double time)
        {
            double CorrectedMetcoefficent = HarrisBenedictBmr / 7.2 / Weight;
            double CorrectedMet; // CorrectedMet = MET * 3.5/CorrectedMetcoefficent

            double Met = metVelocity.MetBasedOnVelocity(MetTable, velocity);

            CorrectedMet = Met * 3.5 / CorrectedMetcoefficent;
            double Calories = CorrectedMet * Weight * time / 3600; // kcal = CorectedMET * kg * h

            return Calories;
        }
    }
}
