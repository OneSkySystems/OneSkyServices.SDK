using System;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs.Routing;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Access
{
    /// <summary>
    /// Sensor field of regard access data
    /// </summary>
    /// <typeparam name="T">the path for the "To" object</typeparam>
    public class SensorForAccessData<T>: IVerifiable
        where T: IVerifiable
    {
        public static double MaximumSensorRangeDefault = 100000;
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public T ToObjectPath { get; set; }
        public CatalogRouteData FromObjectCatalogPath { get; set; }
        public string SensorNameContains { get; set; }
        public bool Sunlit { get; set; }
        public double MaximumSensorRange { get; set; }
        public bool IncludePath { get; set; }

        public SensorForAccessData()
        {
            Sunlit = false;
            MaximumSensorRange = MaximumSensorRangeDefault;
        }

        public void Verify()
        {
            if (ToObjectPath == null)
            {
                throw new AnalyticalServicesException(24000, "ToObjectPath must be set.");
            }

            if (FromObjectCatalogPath == null)
            {
                throw new AnalyticalServicesException(24000, "FromObjectCatalogPath must be set.");
            }

            if (MaximumSensorRange <= 0)
            {
                throw new AnalyticalServicesException(23600, "MaximumSensorRange must be greater than 0.");
            }

            if (Start > Stop)
            {
                throw new AnalyticalServicesException(21800, 
                    "SensorForAccess Start time must be earlier that SensorFORAccess Stop time.");
            }

            if (ToObjectPath != null)
                ToObjectPath.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "ToObjectPath must not be null.");
            }

            if (FromObjectCatalogPath != null)
                FromObjectCatalogPath.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "FromObjectCatalogPath must not be null.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}