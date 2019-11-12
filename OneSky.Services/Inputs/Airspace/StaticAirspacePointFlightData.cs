using System;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Airspace
{
    public class StaticAirspacePointFlightData : StaticAirspaceData,IVerifiable
    {
        public ServiceCartographic Center { get; set; }
        public double Radius { get; set; }
        /// <summary>
        /// The maximum altitude for this point flight
        /// </summary>
        public double MaxAltitude { get; set; }
        /// <summary>
        /// The minimum altitude for this point flight
        /// </summary>
        public double MinAltitude { get; set; }
        /// <summary>
        /// If true, the altitudes are referenced to Mean Sea Level, otherwise, the altitudes are referenced to WGS-84.  Default is true.
        /// </summary>
        public bool MeanSeaLevel { get; set; }
        

        public StaticAirspacePointFlightData()
        {
            MeanSeaLevel = true;
            Radius = 100; // meters
            MaxAltitude = 3000; //meters above msl
            MinAltitude = 0; // MSL
            AirspaceOptions.RegionRadius = Radius;
            AirspaceOptions.RegionCenter = Center;
        }


        public new void Verify()
        {
            base.Verify();

            if (Center == null)
            {
                throw new AnalyticalServicesException(24000, "Center must be supplied to the service.");
            }
                
            Center.Verify();

            if (Radius <= 0 || Radius > 800)
            {
                throw new AnalyticalServicesException(23600, "Radius must be greater than 0 and less than or equal to 800 meters.");
            }

            if (MaxAltitude <= MinAltitude)
            {
               throw new AnalyticalServicesException(21900, "MaxAltitude must be greater than MinAltitude.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}