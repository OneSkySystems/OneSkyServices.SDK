using System;
using System.Collections.Generic;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    /// <summary>
    /// Calculated Accuracy Data
    /// </summary>
    public class AccuracyData
    {
        public Dictionary<NavigationAccuracyType, List<double>> Series { get; }
        public List<DateTime> Dates { get; }
        public List<ServiceCartographic> Positions { get; }
        public List<int> NumberOfSatellites { get; }

        public AccuracyData()
        {
            NumberOfSatellites = new List<int>();
            Series = new Dictionary<NavigationAccuracyType, List<double>>();
            Dates = new List<DateTime>();
            Positions = new List<ServiceCartographic>();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
