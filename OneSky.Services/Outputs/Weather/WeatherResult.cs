using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Weather
{
    public class WeatherResult
    {
        /// <summary>
        /// The time for this weather report
        /// </summary>
        public DateTimeOffset ResultTime { get; set; }
        /// <summary>
        /// In millibars
        /// </summary>
        public float AtmosphericPressure { get; set; }
        /// <summary>
        /// The lowest base of the clouds, in feet
        /// </summary>
        public string CloudCeiling { get; set; }       
        /// <summary>
        /// Percentage of cloud cover [0-1]
        /// </summary>
        public float CloudCover { get; set; }        
        /// <summary>
        /// Degrees Fahrenheit
        /// </summary>
        public float DewPoint { get; set; }        
        /// <summary>
        /// In percent [0-1]
        /// </summary>
        public float RelativeHumidity { get; set; }        
        /// <summary>
        /// Text description that can be used to pick a weather icon
        /// </summary>
        public string Icon { get; set; }        
        /// <summary>
        /// Rate of liquid precipitation in inches per hour
        /// </summary>
        public float PrecipitationRate { get; set; }       
        /// <summary>
        /// The type of precipitation. Will be either "Rain", "Snow" or "Sleet" or null
        /// </summary>
        public string PrecipitationType { get; set; }        
        /// <summary>
        /// The chance of precipitation, percentage [0-1]
        /// </summary>
        public float PrecipitationProbability { get; set; }        
        /// <summary>
        /// In degrees Fahrenheit
        /// </summary>
        public float Temperature { get; set; }
        /// <summary>
        /// A short summary of the weather conditions
        /// </summary>
        public string TextSummary { get; set; }
        /// <summary>
        /// In miles
        /// </summary>
        public float Visibility { get; set; }
        /// <summary>
        /// In miles per hour
        /// </summary>
        public float WindGust { get; set; }
        /// <summary>
        /// In miles per hour
        /// </summary>
        public float WindSpeed { get; set; }
        /// <summary>
        /// Direction the wind comes from, in degrees from North (0), clockwise.
        /// </summary>
        public float WindDirection { get; set; }
        /// <summary>
        /// Any notes about the weather report
        /// </summary>
        public string Notes { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
