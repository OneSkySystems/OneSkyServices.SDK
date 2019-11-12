
using System;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Communications
{
    public class LinkBudgetResultStandard
    {
        public double BitErrorRate { get; set; }
        public double CarrierToNoise { get; set; }
        public double CarrierToNoiseDensity { get; set; }
        public double EffectiveIsotropicRadiatedPower { get; set; }
        public double PowerAtReceiverOutput { get; set; }
        public double PropagationLoss { get; set; }
        public double ReceivedIsotropicPower { get; set; }
        public DateTime Time { get; set; }
       
        public LinkBudgetResultStandard(LinkBudgetResultAll fullResults)
        {
            BitErrorRate = fullResults.BitErrorRate;
            CarrierToNoise = fullResults.CarrierToNoise;
            CarrierToNoiseDensity = fullResults.CarrierToNoiseDensity;
            EffectiveIsotropicRadiatedPower = fullResults.EffectiveIsotropicRadiatedPower;
            PowerAtReceiverOutput = fullResults.PowerAtReceiverOutput;
            PropagationLoss = fullResults.PropagationLoss;
            ReceivedIsotropicPower = fullResults.ReceivedIsotropicPower;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
