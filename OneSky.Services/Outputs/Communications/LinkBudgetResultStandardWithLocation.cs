using System;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Communications
{
    public class LinkBudgetResultStandardWithLocation : LinkBudgetResultStandard
    {
        public ServiceCartographic Location { get; set; }
        public LinkBudgetResultStandardWithLocation(LinkBudgetResultAll fullResults, 
                                                    ServiceCartographic location) : base(fullResults)
        {
            Location = location;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
