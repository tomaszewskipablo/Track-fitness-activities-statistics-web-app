﻿using System;
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
    }
}