using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Routing
{
    /// <summary>
    /// A route that defines an orbital path, but also include parameters for visualization in CZML.
    /// </summary>
    public class Sgp4CzmlRouteData : Sgp4RouteData
    {
        /// <summary>
        /// The amount of time that the orbit will be shown in front of the orbital vehicle's position, in seconds.
        /// The default is 2700.
        /// </summary>
        public double LeadTime { get; set; }
        /// <summary>
        /// The amount of time that the orbit will be shown behind the orbital vehicle's position, in seconds.
        /// The default is 2700.
        /// </summary>
        public double TrailTime { get; set; }
        /// <summary>
        /// The name that will appear on the vehicle's label in the CZML.
        /// </summary>
        public string SatelliteName { get; set; }
        /// <summary>
        /// The color of the orbital path. The default is red.
        /// </summary>
        public string PathColor { get; set; }
        
        public Sgp4CzmlRouteData()
        {
            LeadTime = 2700;
            TrailTime = 2700;
            PathColor = "red";
            OutputSettings = new OutputSettings();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}