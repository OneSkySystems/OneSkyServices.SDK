using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Lighting
{
    public class LightingInfo
    {
        /// <summary>
        /// Start time for Astronomical twilight 
        /// </summary>
        public DateTimeOffset AstronomicalTwilightAmStart { get; set; }
        /// <summary>
        /// Stop time for Astronomical twilight, occurs prior to sunrise
        /// </summary>
        public DateTimeOffset AstronomicalTwilightAmStop { get; set; }
        /// <summary>
        /// Start time for Astronomical twilight 
        /// </summary>
        public DateTimeOffset AstronomicalTwilightPmStart { get; set; }
        /// <summary>
        /// Stop time for Astronomical twilight, occurs prior to sunrise
        /// </summary>
        public DateTimeOffset AstronomicalTwilightPmStop { get; set; }
        /// <summary>
        /// start time for nautical twilight
        /// </summary>
        public DateTimeOffset NauticalTwilightAmStart { get; set; }
        /// <summary>
        /// stop time for nautical twilight
        /// </summary>
        public DateTimeOffset NauticalTwilightAmStop { get; set; }
        /// <summary>
        /// start time for nautical twilight
        /// </summary>
        public DateTimeOffset NauticalTwilightPmStart { get; set; }
        /// <summary>
        /// stop time for nautical twilight
        /// </summary>
        public DateTimeOffset NauticalTwilightPmStop { get; set; }
        /// <summary>
        /// start time for civil twilight
        /// </summary>
        public DateTimeOffset CivilTwilightAmStart { get; set; }
        /// <summary>
        /// stop time for civil twilight
        /// </summary>
        public DateTimeOffset CivilTwilightAmStop { get; set; }
        /// <summary>
        /// start time for civil twilight
        /// </summary>
        public DateTimeOffset CivilTwilightPmStart { get; set; }
        /// <summary>
        /// stop time for civil twilight
        /// </summary>
        public DateTimeOffset CivilTwilightPmStop { get; set; }
        /// <summary>
        /// The time that the upper edge of the sun just crests the local horizontal plane
        /// </summary>
        public DateTimeOffset Sunrise { get; set; }
        /// <summary>
        /// The time that the upper edge of the sun just dips below the local horizontal plane
        /// </summary>
        public DateTimeOffset Sunset { get; set; }
        /// <summary>
        /// When true, indicates that the sun does not rise at this location and all times in this class are undefined.
        /// </summary>
        public bool ContinuouslyBelowHorizon { get; set; }
        /// <summary>
        /// When true, indicates that the sun does not set at this location and all times in this class are undefined.
        /// </summary>
        public bool ContinuouslyAboveHorizon { get; set; }
        /// <summary>
        /// When true, indicates that the sun does rise at this location on this day, when false, 
        /// it does not rise at this location on this day.
        /// </summary>
        public bool IsRiseDefined { get; set; }
        /// <summary>
        /// When true, indicates that the sun does set at this location on this day, when false, 
        /// it does not set at this location on this day.
        /// </summary>
        public bool IsSetDefined { get; set; }

        public LightingInfo()
        {
            ContinuouslyBelowHorizon = false;
            ContinuouslyAboveHorizon = false;
            IsRiseDefined = true;
            IsSetDefined = true;
        }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
