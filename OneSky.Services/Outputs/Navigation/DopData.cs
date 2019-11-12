using System;
using System.Collections.Generic;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    /// <summary>
    /// Calculated DOP data
    /// </summary>
    public class DopData
    {
        public Dictionary<DilutionOfPrecisionType, List<double>> Series { get; }
        public List<DateTime> Dates { get; }
        public List<ServiceCartographic> Positions { get; }
        public List<int> NumberOfSatellites { get; }

        public DopData()
        {
            Series = new Dictionary<DilutionOfPrecisionType, List<double>>();
            Dates = new List<DateTime>();
            Positions = new List<ServiceCartographic>();
            NumberOfSatellites = new List<int>();
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
