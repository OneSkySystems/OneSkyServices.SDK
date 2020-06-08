using System;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Airspace
{
    public class StaticAirspaceData : IVerifiable
    {
        /// <summary>
        /// When true, will return results that apply a horizontal proximity boundary around the airspace, 
        /// using the <see cref="HorizontalProximityThreshold"/> parameter.  When false, the results reflect actual airspace incursions.
        /// </summary>
        public bool UseHorizontalProximity { get; set; }

        private double _horizontalProximityThreshold;
        /// <summary>
        /// A horizontal proximity violation occurs when the path is this close or closer to an airspace in a horizontal direction. 
        /// Optional, must be greater than or equal to 0 in meters. Default is 2000.
        /// </summary>
        public double HorizontalProximityThreshold
        {
            get => _horizontalProximityThreshold * 1.0 / 1.05;
            set => _horizontalProximityThreshold = value * 1.05;
            // adding 5 % to account for the fact the GDAL does not return *some* intersected airspaces.
            //TODO Update this when the distance calculation is updated to a better solution
        }

        /// <summary>
        /// When true, will return results that apply a vertical proximity boundary on the airspace,
        /// using the <see cref="VerticalProximityThreshold"/> parameter.  When false, the results reflect actual airspace incursions.
        /// </summary>
        public bool UseVerticalProximity { get; set; }

        /// <summary>
        /// A vertical proximity violation occurs when the path is this close or closer to an airspace in the vertical direction. 
        /// Optional, must be greater than or equal to 0 in meters. Default is 30.
        /// </summary>
        public double VerticalProximityThreshold { get; set; }
        /// <summary>
        /// Options used to select which Airspaces will be used
        /// </summary>
        public AirspaceSelectionOptions AirspaceOptions { get; set; }

        public StaticAirspaceData()
        {
            HorizontalProximityThreshold = 2000;
            VerticalProximityThreshold = 30;
            AirspaceOptions = new AirspaceSelectionOptions();
        }

        public void Verify()
        {
            AirspaceOptions.Verify();
            if (UseHorizontalProximity && HorizontalProximityThreshold < 0)
            {
                throw new AnalyticalServicesException(23600, "HorizontalProximityThreshold must be greater than or equal to 0.");
            }

            if (UseVerticalProximity && VerticalProximityThreshold < 0)
            {
                throw new AnalyticalServicesException(23600, "VerticalProximityThreshold must be greater than or equal to 0.");
            }   
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
