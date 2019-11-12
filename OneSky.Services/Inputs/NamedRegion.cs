using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs
{
    public class NamedRegion
    {
        public List<ServiceCartographic> Positions { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}