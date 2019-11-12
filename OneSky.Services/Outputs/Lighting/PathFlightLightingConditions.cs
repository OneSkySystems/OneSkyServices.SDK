using System.Collections.Generic;

namespace OneSky.Services.Outputs.Lighting
{
    public class PathFlightLightingConditions
    {
        /// <summary>
        /// The lighting conditions at the beginning of the flight
        /// </summary>
        public LightingInfo FlightLightingInfo { get; set; }
        /// <summary>
        /// Lighting conditions at the beginning of the flight
        /// </summary>
        public string BeginningOfFlightLightingCondition { get; set; }
        /// <summary>
        /// Lighting conditions at the end of the flight
        /// </summary>
        public string EndOfFlightLightingCondition { get; set; }
       /// <summary>
        /// True if a sunrise occurred between the start and stop times, false otherwise
        /// </summary>
        public bool SunriseBetweenStartAndEnd { get; set; }
        /// <summary>
        /// true if a sunset occurred between the start and stop times
        /// </summary>
        public bool SunsetBetweenStartAndEnd { get; set; }

        // public PathFlightLightingConditions(){
        //     FlightLightingInfo = new LightingInfo();
        // }
       
    }
}
