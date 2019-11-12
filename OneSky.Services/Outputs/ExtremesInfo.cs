using System;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs
{
    public class ExtremesInfo : BasicExtremesInfo<double>
    {
        public ServiceCartographic LocationOfMax { get; set; }
        public ServiceCartographic LocationOfMin { get; set; }
        public DateTime TimeOfMax { get; set; }
        public DateTime TimeOfMin { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
