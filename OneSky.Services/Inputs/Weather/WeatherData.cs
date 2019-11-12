using System;
using OneSky.Services.Exceptions;

namespace OneSky.Services.Inputs.Weather
{
    public class WeatherData<T>: IVerifiable 
        where T: IVerifiable
    {

        /// <summary>
        /// The location at or along which weather reports are desired.
        /// </summary>
        public T Path { get; set; }
        /// <summary>
        /// The start time for the weather results. This is only used if Path is a Site location.
        /// </summary>
        public DateTimeOffset AnalysisStart {get; set;}
        /// <summary>
        /// The end time for the weather results. This is only used if Path is a Site location.
        /// </summary>
        public DateTimeOffset AnalysisStop {get; set;}
        /// <summary>
        /// The provider for the weather results.  The default is WeatherProviderType.DarkSky
        /// </summary>
        public WeatherProviderType Provider {get; set;}
        public WeatherData()
        {
            Provider = WeatherProviderType.DarkSky;
        }

        public void Verify()
        {
            if (Path != null)
                Path.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "Path must not be null.");
            }
        }
    }
}
