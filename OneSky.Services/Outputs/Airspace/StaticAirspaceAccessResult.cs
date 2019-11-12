using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Airspace
{
    public class StaticAirspaceAccessResult<T> where T: AirspaceIdentifier
    {
        public List<string> UnrecognizedAirspaceIds {get; set;}
        public List<T> AirspacesAccessed { get; set; }

        public StaticAirspaceAccessResult()
        {
            UnrecognizedAirspaceIds = new List<string>();
            AirspacesAccessed = new List<T>();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}