using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Airspace
{
    public class AirspaceCrossingResult<TPath> : AirspaceIdentifier
    {
        public TPath Entry { get; set; }
        public TPath Exit { get; set; }
        public List<TPath> Path { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
