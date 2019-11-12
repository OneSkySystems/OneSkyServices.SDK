using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs
{
    public class ServiceCartographic : IVerifiable
    {
        public ServiceCartographic()
        {

        }
        public ServiceCartographic(double latitude, double longitude, double altitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        /// <summary>
        /// Latitude measured negative south, in degrees
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude measured negative west, in degrees.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Altitude in meters
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// Verifies cartographic coordinates
        /// </summary>
        /// <exception cref="AnalyticalServicesException">When Longitude or Latitude values are outside 
        /// their domain, in degrees.</exception>
        public void Verify()
        {
            if (Latitude < -90 || Latitude > 90)
            {
                throw new AnalyticalServicesException(23600, "Latitude must be between -90 deg and 90 deg.");
            }

            if (Longitude < -360 || Longitude > 360)
            {
                throw new AnalyticalServicesException(23600, "Longitude must be between -360 deg and 360 deg.");
            }
        }

        public override string ToString(){
            return JsonConvert.SerializeObject(this);
        }
        
    }
}
