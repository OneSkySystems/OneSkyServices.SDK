using System;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Routing
{
    public class TolRouteData : IVerifiable
    {
        public ServiceCartographic TakeOffPoint { get; set; }
        public ServiceCartographic LandingPoint { get; set; }
        public DateTime Start { get; set; }
        public double Speed { get; set; }
        public double Altitude { get; set; }
        public bool MeanSeaLevel { get; set; }
        public OutputSettings OutputSettings { get; set; }

        public TolRouteData()
        {
            Speed = 65;
            Altitude = 1000;
            MeanSeaLevel = true;
            OutputSettings = new OutputSettings();
        }

        public void Verify()
        {
            if (TakeOffPoint == null)
            {
                throw new AnalyticalServicesException(24000, "TakeOffPoint must be set in this service.");
            }

            if (LandingPoint == null)
            {
                throw new AnalyticalServicesException(24000, "LandingPoint must be set in this service.");
            }

            TakeOffPoint.Verify();
            LandingPoint.Verify();
            OutputSettings.Verify();

            if (Speed <= 0)
            {
                throw new AnalyticalServicesException(23600, "Speed must be greater than 0.");
            }
            if (Altitude <= 0)
            {
                throw new AnalyticalServicesException(23600, "Route altitude must be greater than 0.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
