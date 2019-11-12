using System;
using System.Collections.Generic;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Navigation
{
    public class RegionLocationData : IVerifiable
    {
        /// <summary>
        /// List of boundary points defining the region.  Must include at least two points.
        /// </summary>
        public List<ServiceCartographic> RegionBoundary { get; set; }
        /// <summary>
        /// The grid point separation inside the region used for calculation, in degrees.
        /// </summary>
        public double GridIncrement { get; set; }
        /// <summary>
        /// True if this is to be a global calculation, false otherwise.  If false, RegionBoundary must have at least two boundary points defined.
        /// </summary>
        public bool IsGlobal { get; set; }
        /// <summary>
        /// The type of result to return from the calculation.
        /// </summary>
        public NavigationResultType ResultCategory { get; set; }
        /// <summary>
        /// Output Settings for the results
        /// </summary>
        public OutputSettings OutputSettings { get; set; }

        public RegionLocationData()
        {
            IsGlobal = false;
            RegionBoundary = new List<ServiceCartographic>();
            ResultCategory = NavigationResultType.Total;
            OutputSettings = new OutputSettings();
        }

        public void Verify()
        {
            if (!IsGlobal && RegionBoundary.Count < 2)
            {
                throw new AnalyticalServicesException(23600, "RegionBoundary must have at least two boundary points");
            }
            RegionBoundary.ForEach(bp => bp.Verify());

            if (GridIncrement <= 0)
            {
                throw new AnalyticalServicesException(23600, "GridIncrement must be greater than 0.");

                // todo eventually check for this value;
                //(GridArea)^2/(PointArea)^2 * runDuration/timestep = constant.             
            }
            OutputSettings.Verify();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }    
}
                                                     
