using System;
using System.Collections.Generic;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs.Routing;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Navigation
{
    public class NavigationData<T> : IReceiverParameters
        where T: IVerifiable
    {
        /// <summary>
        /// The location at or along which navigation results are desired.
        /// </summary>
        public T Path { get; set; }
        /// <summary>
        /// The start time for the navigation analysis
        /// </summary>
        public DateTime AnalysisStart { get; set; }
        /// <summary>
        /// The stop time for the navigation analysis.
        /// </summary>
        public DateTime AnalysisStop { get; set; }
        /// <summary>
        /// The number of channels able to receive a GPS signal in the GPS receiver used in this calculation. Defaults to 12.
        /// </summary>
        public int NumberOfChannels { get; set; }
        /// <summary>
        /// The elevation angle below which no GPS satellites will be used to in the position solution. (Degrees) Defaults to 5.
        /// </summary>
        public double MinimumElevationAngle { get; set; }
        /// <summary>
        /// Use the Earth as a line-of-sight obstruction to accessing satellites.  Optional, defaults to true.
        /// </summary>
        public bool EarthLineOfSight { get; set; }
        /// <summary>
        /// The amount of noise, expressed in meters, the Gps receiver contributes to the position error. Defaults to 0.8.
        /// </summary>
        public double ReceiverNoiseError { get; set; }
        /// <summary>
        /// When true, a Best-N algorithm is used to calculate which GPS satellites are used in the solution, where N is the NumberOfChannels.  When false, an All-In-View algorithm is used.  Defaults to false.
        /// </summary>
        public bool BestN { get; set; }
        /// <summary>
        /// Path (file path, Http, Ftp, etc.) for the SEM almanac if a specific almanac is to be used.  If this property is set, no other SEM almanac will be used.
        /// </summary>
        public string SemAlmanacLocation { get; set; }
        /// <summary>
        /// Path (file path, Http, Ftp, etc.) for the Satellite Outage File (SOF) if a specific SOF is to be used. If this property is set, no other SOF will be used.
        /// </summary>
        public string SofLocation { get; set; }
        /// <summary>
        /// When true, the best available data will be retrieved from the internet if no data is available for the date requested. Defaults to true.
        /// </summary>
        public bool UseBestAvailableData { get; set; }
        /// <summary>
        /// A list of supported GNSS constellations to use. If empty, only Gps will be used.
        /// </summary>
        public List<NavigationConstellationType> Constellations { get; set; }
        
        public NavigationData ()
	    {
            NumberOfChannels = 12;
            MinimumElevationAngle = 5.0;
	        EarthLineOfSight = true;
            ReceiverNoiseError = 0.8;
            BestN = false;
            SemAlmanacLocation = string.Empty;
            SofLocation = string.Empty;
	        UseBestAvailableData = true;
            Constellations = new List<NavigationConstellationType>();
        }

        public virtual void Verify()
        {
            if(Path == null)
                throw new AnalyticalServicesException(22200, "Path must not be null.");
            
            Path.Verify();

            if (Path is ISiteInput)
            {
                // verify times are present and in order
                if (AnalysisStart >= AnalysisStop)
                {
                    throw new AnalyticalServicesException(21800, "AnalysisStart and AnalysisStop must be provided and AnalysisStart must be before AnalysisStop.");
                }
            }

            if (NumberOfChannels < 4)
            {
                throw new AnalyticalServicesException(23600, "NumberOfChannels must be 4 or greater.");
            }

            if (MinimumElevationAngle < -90 || MinimumElevationAngle > 90)
            {
                throw new AnalyticalServicesException(23600, "MinimumElevationAngle must be between -90 deg and 90 deg inclusive.");
            }

            if (ReceiverNoiseError < 0)
            {
                throw new AnalyticalServicesException(23600, "ReceiverNoiseError must be greater than or equal to 0.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }    
}
