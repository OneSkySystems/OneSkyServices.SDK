using System;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Lighting
{
    /// <summary>
    /// Defines an azimuth/elevation pair at a specific time for solar lighting output
    /// </summary>
    public class SolarAngles
    {
        /// <summary>
        /// The time of the Az El
        /// </summary>
        public DateTimeOffset Time { get; set; }
        /// <summary>
        /// The azimuth in degrees, measured eastward from north along the local horizontal plane
        /// </summary>
        public double Azimuth { get; set; }
        /// <summary>
        /// The elevation angle in degrees, measured positive away from the central body, from the local horizontal plane
        /// </summary>
        public double Elevation { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
