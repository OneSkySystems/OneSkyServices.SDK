using System;
using System.Collections.Generic;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Routing
{
    /// <summary>
    /// Defines a route that creates a Great Arc geodesic between Waypoints
    /// </summary>
    public class GreatArcRouteData : IVerifiable
    {
        public List<ServiceCartographicWithTime> Waypoints { get; set; }
        public OutputSettings OutputSettings { get; set; }

        public GreatArcRouteData(int numberOfPointsToAdd)
        {
            OutputSettings = new OutputSettings();
            
            Waypoints = new List<ServiceCartographicWithTime>();
            for (int i = 0; i < numberOfPointsToAdd; i++)
            {
                Waypoints.Add(new ServiceCartographicWithTime());
            }
        }

        public void Verify()
        {
            OutputSettings.Verify();

            if (Waypoints == null)
            {
                throw new AnalyticalServicesException(24000, "Waypoints must be supplied to this service.");
            }

            if(Waypoints.Count < 2)
            {
                throw new AnalyticalServicesException(23600, "There must be at least two waypoints to define a route.");
            }

            Waypoints.ForEach(n => n.Verify());
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}