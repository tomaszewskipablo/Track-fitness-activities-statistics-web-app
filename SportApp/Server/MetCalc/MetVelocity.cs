using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DAL.Models;

namespace SportApp.Server.MetCalc
{
    public class MetVelocity
    {
        public double MetBasedOnVelocity(List<Met> metTable, double velocity)
        {
            if (metTable.Count == 1)
                return metTable[0].Value;

            if (velocity < 0.5)
                return 2;

            int j = 0;
            while (metTable[j].Speedkmh < velocity)
            {
                j++;
                if (j == metTable.Count)
                    return metTable[metTable.Count-1].Value;
            }         
            if (j == 0)    // jesli predkosc mniejsza niz minimalna przewidziana w tabeli
                return metTable[0].Value;
            if(j == metTable.Count) // jesli predkosc wieksza
                return metTable[metTable.Count - 1].Value;

            double lowerSpeed = (double)metTable[j-1].Speedkmh;
            double fasterSpeed = (double)metTable[j].Speedkmh;

            double lowerMet = (double)metTable[j-1].Value;
            double fasterMet = (double)metTable[j].Value;

            double a = (fasterMet - lowerMet) / (fasterSpeed - lowerSpeed);
            double met = a * velocity + fasterMet - a * fasterSpeed;

            return met;
        }
    }
}
