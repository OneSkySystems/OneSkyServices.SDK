using System.Collections.Generic;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Airspace
{
    public class AirspaceSelectionOptions: IVerifiable
    {
        /// <summary>
        /// When true, all airspaces will be returned that have the text specified in AirspaceId as part of their Airspace Identifier.
        /// For example, if one of the AirspaceIds is "DENVER" all airspaces that have the term "DENVER" as part of their actual AirspaceId will be returned.
        /// If Search is false, no such search is performed and only airspaces with the exact AirspaceId of "DENVER" will be returned.  Default is false.
        /// </summary>
        public bool Search { get; set; }
        /// <summary>
        /// A list of AirspaceIds to check for intersections. If there are entries in this list, and Search is false, only the entries only this list will be checked
        ///  regardless of other airspace selection settings.
        /// </summary>
        public List<string> AirspaceIds { get; set; }
        /// <summary>
        /// When true, only airspaces within a circle centered on <see cref="RegionCenter"/>, with a radius of <see cref="RegionRadius"/> will be returned.
        /// </summary>
        public bool UseRegionalAirspaceQuery { get; set; }
        /// <summary>
        /// Defines the center of a region to search for airspaces.
        /// </summary>
        public ServiceCartographic RegionCenter { get; set; }
        /// <summary>
        /// Defines the radius from <see cref="RegionCenter"/> in meters. Optional, default is 5000.
        /// </summary>
        public double RegionRadius { get; set; }
        /// <summary>
        /// Categories of airspaces(s) to use. Optional.
        /// </summary>
        public List<AirspaceCategory> Categories { get; set; }
        /// <summary>
        /// When true, only airspaces that touch the surface of the Earth will be returned. 
        /// When true, this must be used in conjunction with the Search criteria or regional query criteria.
        /// Optional, default is false.
        /// </summary>
        public bool OnSurface { get; set; }

        public AirspaceSelectionOptions()
        {
            Search = false;
            UseRegionalAirspaceQuery = false;
            RegionRadius = 50000;
            OnSurface = false;
            Categories = new List<AirspaceCategory>();
            AirspaceIds = new List<string>();
        }

        public void Verify()
        {
            if (Search)
            {
                if (AirspaceIds == null || AirspaceIds.Count == 0)
                {
                    throw new AnalyticalServicesException(24250, "Airspace option 'Search' is enabled, but no AirspaceIds were provided.");
                }
            }
            if (UseRegionalAirspaceQuery)
            {
                if (RegionRadius <= 0)
                {
                    throw new AnalyticalServicesException(23600, "Airspace option 'RegionRadius' must be greater than 0.");
                }
                if (RegionCenter == null)
                {
                    throw new AnalyticalServicesException(24000, "Airspace option 'RegionCenter' must be provided when searching by location");
                }
            }   
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
