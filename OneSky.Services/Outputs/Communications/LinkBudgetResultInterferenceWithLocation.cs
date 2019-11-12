using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Communications
{
    public class LinkBudgetResultInterferenceWithLocation : LinkBudgetResultInterference
    {
        public ServiceCartographic Location { get; set; }
        public LinkBudgetResultInterferenceWithLocation(LinkBudgetResultAll fullResults, 
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