using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Airspace
{
    public class AirspaceCzml
    {
        public string Czml { get; set; }
        public List<string> UnrecognizedIds { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
