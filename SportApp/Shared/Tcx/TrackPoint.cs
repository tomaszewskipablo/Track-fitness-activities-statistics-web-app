﻿using System.Collections.Generic;

public class TrackPoint
{
    public string Timex { set; get; }
    public double AltitudeMeters { get; set; }
    public double DistanceMeters { get; set; }
    public int HeartRateBpm { get; set; }
    public int Cadence { get; set; }
    public string SensorState { get; set; }
    public List<Position> Positionx { get; set; }
}