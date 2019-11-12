
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Airspace
{
    public class StaticAirspaceRealTimeData: StaticAirspaceData, IVerifiable
    {
        /// <summary>
        /// The location at which airspace proximity results are desired.
        /// </summary>
        public ServiceCartographic Location { get; set; }
        /// <summary>
        /// When true, the altitude in Location is referenced to Mean Sea Level (MSL).  
        /// When false it's referenced to WGS-84. <i>Optional.</i> Default is true.
        /// </summary>
        public bool MeanSeaLevel { get; set; }

        public StaticAirspaceRealTimeData()
        {
            MeanSeaLevel = true;
            AirspaceOptions.RegionCenter = Location;
        }

        public new void Verify()
        {
            base.Verify();
            Location.Verify();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}