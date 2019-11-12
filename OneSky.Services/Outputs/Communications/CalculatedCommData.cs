using System;
using System.Collections.Generic;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Communications
{
    /// <summary>
    /// Calculated Communications data
    /// </summary>
    public class CalculatedCommData
    {
        public Dictionary<LinkBudgetType, List<double>> Series { get; }
        public List<DateTime> Dates { get; }
        public List<ServiceCartographic> Positions { get; }

        public CalculatedCommData()
        {
            Series = new Dictionary<LinkBudgetType, List<double>>();
            Dates = new List<DateTime>();
            Positions = new List<ServiceCartographic>();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
