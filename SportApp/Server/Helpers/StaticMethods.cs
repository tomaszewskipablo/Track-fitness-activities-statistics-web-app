using SportApp.Server.MetCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportApp.Server.Helpers
{
    public class StaticMethods
    {
        public static List<TrackPoint> LapsToTrackPoints(Activity activity)
        {
            List<TrackPoint> Training = new List<TrackPoint>();

            foreach (var lap in activity.Laps)
            {
                foreach (var track in lap.Tracks)
                {
                    foreach (var trackPoint in track.TrackPoints)
                    {
                        Training.Add(trackPoint);
                    }
                }
            }
            return Training;
        }
        public static double CountAverage(List<TrackPoint> list, int start, int end)
        {
            List<TrackPoint> Training = new List<TrackPoint>();

            double sum = 0;

            int i;
            for (i = start; i < end; i++)
            {
                sum += list[i].HeartRateBpm;
            }

            i -= start;
            return sum/i;
        }

    }
}
