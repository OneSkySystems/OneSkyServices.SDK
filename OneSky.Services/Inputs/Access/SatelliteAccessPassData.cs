using System;
using System.Collections.Generic;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Access
{
    public class SatelliteAccessPassData<T>: IVerifiable
        where T: IVerifiable
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public T FromObjectPath { get; set; }
        public List<int> SSCs { get; set; }
        public bool FromObjectDark { get; set; }
        public bool ToObjectLit { get; set; }
        public bool UseMinElevation { get; set; }
        public double FromObjectMinElevation { get; set; }
        public bool LineOfSight { get; set; }
        public bool IncludePathData { get; set; }
        public bool IncludePathCzml { get; set; }
        public string SatelliteOrbitColor { get; set; }
        public string PassLinkColor { get; set; }

        public SatelliteAccessPassData()
        {
            FromObjectDark = true;
            ToObjectLit = true;
            UseMinElevation = true;
            FromObjectMinElevation = 10.0;
            LineOfSight = true;
            IncludePathData = false;
            IncludePathCzml = false;
            SatelliteOrbitColor = "AliceBlue";
            PassLinkColor = "Magenta";
            SSCs = new List<int>();
        }

        public void Verify()
        {
            if (FromObjectPath != null)
                FromObjectPath.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "FromObjectPath must not be null.");
            }

            if (SSCs.Count == 0)
            {
                throw new AnalyticalServicesException(24201, "SSCs must contain at least one SSC number.");
            }

            if (FromObjectPath == null)
            {
                throw new AnalyticalServicesException(24000, "FromObjectPath must be set.");
            }


            if (FromObjectMinElevation < -90 || FromObjectMinElevation > 90)
            {
                throw new AnalyticalServicesException(23600, 
                    "FromObjectMinElevation must be between -90 and 90 degrees inclusive.");
            }

            if (Start > Stop)
            {
                throw new AnalyticalServicesException(21800, 
                    "SatelliteAccessPassData Start time must be earlier that SatelliteAccessPassData Stop time.");
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}