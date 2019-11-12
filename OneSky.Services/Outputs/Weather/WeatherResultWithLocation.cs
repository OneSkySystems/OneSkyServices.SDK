using System;
using System.Collections.Generic;
using System.Globalization;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Weather
{
    public class WeatherResultWithLocation : WeatherResult
    {
       public ServiceCartographic Location {get; set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
