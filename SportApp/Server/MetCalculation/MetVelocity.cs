using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DAL.Models;

namespace SportApp.Server.MetCalculation
{
    public class MetVelocity
    {
        public double MetBasedOnVelocity(List<Met> metTable, double velocity)
        {
            // sortuj first
            int j = 0;
            while (metTable[j].MaxSpeedkmh < velocity)
            {
                j++;
                if (j == metTable.Count)
                    return metTable[metTable.Count-1].Value;
            }         
            if (j == 0)    // jesli predkosc mniejsza niz minimalna przewidziana w tabeli
                return metTable[0].Value;
            

            double lowerSpeed = (double)metTable[j-1].MaxSpeedkmh;
            double fasterSpeed = (double)metTable[j].MaxSpeedkmh;

            double lowerMet = (double)metTable[j-1].Value;
            double fasterMet = (double)metTable[j].Value;

            double a = (fasterMet - lowerMet) / (fasterSpeed - lowerSpeed);
            double met = a * velocity + fasterMet - a * fasterSpeed;

            return met;
        }
    }
}
