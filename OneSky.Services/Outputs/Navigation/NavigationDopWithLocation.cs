using System;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{    
    /// <summary>
    /// This is used for conditions where the location changes with time
    /// </summary>
    public class NavigationDopWithLocation : NavigationDop
    {
        public ServiceCartographic Location { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}