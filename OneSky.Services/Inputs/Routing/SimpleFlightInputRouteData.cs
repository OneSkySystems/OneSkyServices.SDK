using System;
using System.Collections.Generic;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Routing
{
    /// <summary>
    /// Simple flight routes are defined by a few waypoints, with the vehicle making smooth turns around
    /// each waypoint, all at a constant altitude.
    /// </summary>
    public class SimpleFlightRouteData : IVerifiable
    {
        /// <summary>
        /// The start time of the route.
        /// </summary>
        public DateTimeOffset Start { get; set; }
        /// <summary>
        /// The set of waypoints the route will follow
        /// </summary>
        public List<ServiceCartographic2D> Waypoints { get; set; }
        /// <summary>
        /// The radius of the turn around a waypoint, in meters.
        /// </summary>
        public double TurningRadius { get; set; }
        /// <summary>
        /// The constant speed at which the vehicle travels along the route, in meters/second.
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// The constant altitude the vehicle flies at along the route, in meters.
        /// </summary>
        public double Altitude { get; set; }
        /// <summary>
        /// Set this to true if <see cref="Altitude"/> is referenced to Mean Sea Level.
        /// Set this to false if <see cref="Altitude"/> is referenced to WGS-84.
        /// </summary>
        public bool MeanSeaLevel { get; set; }
        /// <summary>
        /// The <see cref="OutputSettings"/> the route will be propagated with.
        /// </summary>
        public OutputSettings OutputSettings { get; set; }

        public SimpleFlightRouteData()
        {
            TurningRadius = 200;
            Speed = 65;        
            Altitude = 1000;
            MeanSeaLevel = true;
            OutputSettings = new OutputSettings();
            Waypoints = new List<ServiceCartographic2D>();
        }

        public void Verify()
        {
            OutputSettings.Verify();

            if (Waypoints == null)
            {
                throw new AnalyticalServicesException(24000, "Waypoints must be supplied to this service.");
            }

            if (Waypoints.Count < 2)
            {
                throw new AnalyticalServicesException(23600, "There must be at least two waypoints to define a route.");
            }

            Waypoints.ForEach(n => n.Verify());

            if (Speed <= 0)
            {
                throw new AnalyticalServicesException(23600, "Speed must be greater than 0.");
            }
            if (Altitude <= 0)
            {
               throw new AnalyticalServicesException(23600, "Altitude must be greater than 0.");
            }
            if (Start == DateTime.MinValue)
            {
                throw new AnalyticalServicesException(23600, "Start must be set to a valid value.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    
}
