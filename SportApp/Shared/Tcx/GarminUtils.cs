using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

//Copyright (c) 2008 http://peterkellner.net


//Permission is hereby granted, free of charge, to any person
//obtaining a copy of this software and associated documentation
//files (the "Software"), to deal in the Software without
//restriction, including without limitation the rights to use,
//copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the
//Software is furnished to do so, subject to the following
//conditions:

//The above copyright notice and this permission notice shall be
//included in all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//OTHER DEALINGS IN THE SOFTWARE.


/// <summary>
/// Summary description for GarminUtils
/// </summary>
public class GarminUtils
{
    public static Activity ConvertTCS(Stream stream)
    {
        XElement root = XElement.Load(stream);
        XNamespace ns1 = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2";

        IEnumerable<Activity> activities = from activityElement in root.Descendants(ns1 + "Activities")
                                           select new Activity
                                                      {
                                                          Laps =
                                                              (from lapElement in
                                                                   activityElement.Descendants(ns1 + "Lap")
                                                               select new Lap
                                                                          {
                                                                              TotalTimeSeconds =
                                                                                  lapElement.Element(ns1 +
                                                                                                     "TotalTimeSeconds") !=
                                                                                  null
                                                                                      ? Convert.ToDouble(
                                                                                            (string)
                                                                                            lapElement.Element(ns1 +
                                                                                                               "TotalTimeSeconds")
                                                                                                .Value)
                                                                                      : 0.00,
                                                                              DistanceMeters =
                                                                                  lapElement.Element(ns1 +
                                                                                                     "DistanceMeters") !=
                                                                                  null
                                                                                      ? Convert.ToDouble(
                                                                                            (string)
                                                                                            lapElement.Element(ns1 +
                                                                                                               "DistanceMeters")
                                                                                                .Value)
                                                                                      : 0.00,
                                                                              MaximumSpeed =
                                                                                  lapElement.Element(ns1 +
                                                                                                     "MaximumSpeed") !=
                                                                                  null
                                                                                      ? Convert.ToDouble(
                                                                                            (string)
                                                                                            lapElement.Element(ns1 +
                                                                                                               "MaximumSpeed")
                                                                                                .Value)
                                                                                      : 0.00,
                                                                              Calories =
                                                                                  lapElement.Element(ns1 + "Calories") !=
                                                                                  null
                                                                                      ? Convert.ToInt16(
                                                                                            (string)
                                                                                            lapElement.Element(ns1 +
                                                                                                               "Calories")
                                                                                                .Value)
                                                                                      : 0,
                                                                              AverageHeartRateBpm =
                                                                                  lapElement.Element(ns1 +
                                                                                                     "AverageHeartRateBpm") !=
                                                                                  null
                                                                                      ? Convert.ToInt16(
                                                                                            (string)
                                                                                            lapElement.Element(ns1 +
                                                                                                               "AverageHeartRateBpm")
                                                                                                .Value)
                                                                                      : 0,
                                                                              MaximumHeartRateBpm =
                                                                                  lapElement.Element(ns1 +
                                                                                                     "MaximumHeartRateBpm") !=
                                                                                  null
                                                                                      ? Convert.ToInt16(
                                                                                            (string)
                                                                                            lapElement.Element(ns1 +
                                                                                                               "MaximumHeartRateBpm")
                                                                                                .Value)
                                                                                      : 0,
                                                                              Intensity =
                                                                                  lapElement.Element(ns1 + "Intensity") !=
                                                                                  null
                                                                                      ? lapElement.Element(ns1 +
                                                                                                           "Intensity").
                                                                                            Value
                                                                                      : "",
                                                                              Cadence =
                                                                                  lapElement.Element(ns1 + "Cadence") !=
                                                                                  null
                                                                                      ? Convert.ToInt16(
                                                                                            (string)
                                                                                            lapElement.Element(ns1 +
                                                                                                               "Cadence")
                                                                                                .Value)
                                                                                      : 0,
                                                                              TriggerMethod =
                                                                                  lapElement.Element(ns1 +
                                                                                                     "TriggerMethod") !=
                                                                                  null
                                                                                      ? lapElement.Element(ns1 +
                                                                                                           "TriggerMethod")
                                                                                            .Value
                                                                                      : "",
                                                                              Notes =
                                                                                  lapElement.Element(ns1 + "Notes") !=
                                                                                  null
                                                                                      ? lapElement.Element(ns1 + "Notes")
                                                                                            .Value
                                                                                      : "",
                                                                              Tracks =
                                                                                  (from trackElement in
                                                                                       lapElement.Descendants(ns1 +
                                                                                                              "Track")
                                                                                   select new Track
                                                                                              {
                                                                                                  TrackPoints =
                                                                                                      (from
                                                                                                           trackPointElement
                                                                                                           in
                                                                                                           trackElement.
                                                                                                           Descendants(
                                                                                                           ns1 +
                                                                                                           "Trackpoint")
                                                                                                       select
                                                                                                           new TrackPoint
                                                                                                               {
                                                                                                                   Timex
                                                                                                                       =
                                                                                                                       trackPointElement
                                                                                                                           .
                                                                                                                           Element
                                                                                                                           (ns1 +
                                                                                                                            "Time") !=
                                                                                                                       null
                                                                                                                           ? Convert
                                                                                                                                 .
                                                                                                                                 ToString
                                                                                                                                 ((
                                                                                                                                  string
                                                                                                                                  )
                                                                                                                                  trackPointElement
                                                                                                                                      .
                                                                                                                                      Element
                                                                                                                                      (ns1 +
                                                                                                                                       "Time")
                                                                                                                                      .
                                                                                                                                      Value)
                                                                                                                           : "",
                                                                                                                   AltitudeMeters
                                                                                                                       =
                                                                                                                       trackPointElement
                                                                                                                           .
                                                                                                                           Element
                                                                                                                           (ns1 +
                                                                                                                            "AltitudeMeters") !=
                                                                                                                       null
                                                                                                                           ? Convert
                                                                                                                                 .
                                                                                                                                 ToDouble
                                                                                                                                 ((
                                                                                                                                  string
                                                                                                                                  )
                                                                                                                                  trackPointElement
                                                                                                                                      .
                                                                                                                                      Element
                                                                                                                                      (ns1 +
                                                                                                                                       "AltitudeMeters")
                                                                                                                                      .
                                                                                                                                      Value)
                                                                                                                           : 0.0,
                                                                                                                   DistanceMeters
                                                                                                                       =
                                                                                                                       trackPointElement
                                                                                                                           .
                                                                                                                           Element
                                                                                                                           (ns1 +
                                                                                                                            "DistanceMeters") !=
                                                                                                                       null
                                                                                                                           ? Convert
                                                                                                                                 .
                                                                                                                                 ToDouble
                                                                                                                                 ((
                                                                                                                                  string
                                                                                                                                  )
                                                                                                                                  trackPointElement
                                                                                                                                      .
                                                                                                                                      Element
                                                                                                                                      (ns1 +
                                                                                                                                       "DistanceMeters")
                                                                                                                                      .
                                                                                                                                      Value)
                                                                                                                           : 0.0,
                                                                                                                   HeartRateBpm
                                                                                                                       =
                                                                                                                       trackPointElement
                                                                                                                           .
                                                                                                                           Element
                                                                                                                           (ns1 +
                                                                                                                            "HeartRateBpm") !=
                                                                                                                       null
                                                                                                                           ? Convert
                                                                                                                                 .
                                                                                                                                 ToInt16
                                                                                                                                 ((
                                                                                                                                  string
                                                                                                                                  )
                                                                                                                                  trackPointElement
                                                                                                                                      .
                                                                                                                                      Element
                                                                                                                                      (ns1 +
                                                                                                                                       "HeartRateBpm")
                                                                                                                                      .
                                                                                                                                      Value)
                                                                                                                           : 0,
                                                                                                                   Cadence
                                                                                                                       =
                                                                                                                       trackPointElement
                                                                                                                           .
                                                                                                                           Element
                                                                                                                           (ns1 +
                                                                                                                            "Cadence") !=
                                                                                                                       null
                                                                                                                           ? Convert
                                                                                                                                 .
                                                                                                                                 ToInt16
                                                                                                                                 ((
                                                                                                                                  string
                                                                                                                                  )
                                                                                                                                  trackPointElement
                                                                                                                                      .
                                                                                                                                      Element
                                                                                                                                      (ns1 +
                                                                                                                                       "Cadence")
                                                                                                                                      .
                                                                                                                                      Value)
                                                                                                                           : 0,
                                                                                                                   SensorState
                                                                                                                       =
                                                                                                                       trackPointElement
                                                                                                                           .
                                                                                                                           Element
                                                                                                                           (ns1 +
                                                                                                                            "SensorState") !=
                                                                                                                       null
                                                                                                                           ? trackPointElement
                                                                                                                                 .
                                                                                                                                 Element
                                                                                                                                 (ns1 +
                                                                                                                                  "SensorState")
                                                                                                                                 .
                                                                                                                                 Value
                                                                                                                           : "",
                                                                                                                   Positionx
                                                                                                                       =
                                                                                                                       ((from
                                                                                                                             positionElement
                                                                                                                             in
                                                                                                                             trackPointElement
                                                                                                                             .
                                                                                                                             Descendants
                                                                                                                             (ns1 +
                                                                                                                              "Position")
                                                                                                                         select
                                                                                                                             new Position
                                                                                                                                 {
                                                                                                                                     LatitudeDegrees
                                                                                                                                         =
                                                                                                                                         Convert
                                                                                                                                         .
                                                                                                                                         ToDouble
                                                                                                                                         ((
                                                                                                                                          string
                                                                                                                                          )
                                                                                                                                          positionElement
                                                                                                                                              .
                                                                                                                                              Element
                                                                                                                                              (ns1 +
                                                                                                                                               "LatitudeDegrees")
                                                                                                                                              .
                                                                                                                                              Value),
                                                                                                                                     LongitudeDegrees
                                                                                                                                         =
                                                                                                                                         Convert
                                                                                                                                         .
                                                                                                                                         ToDouble
                                                                                                                                         ((
                                                                                                                                          string
                                                                                                                                          )
                                                                                                                                          positionElement
                                                                                                                                              .
                                                                                                                                              Element
                                                                                                                                              (ns1 +
                                                                                                                                               "LongitudeDegrees")
                                                                                                                                              .
                                                                                                                                              Value)
                                                                                                                                 })
                                                                                                                       .
                                                                                                                       ToList
                                                                                                                       ())
                                                                                                               }).ToList
                                                                                                      ()
                                                                                              }).ToList()
                                                                          }).ToList()
                                                      };

        return activities.SingleOrDefault();
    }
}