
using System;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Communications
{
    public class LinkBudgetResultInterference
    {
        public double BitErrorRate { get; set; }
        public double CarrierToInterference { get; set; }
        public double CarrierToNoise { get; set; }
        public double CarrierToNoiseDensity { get; set; }
        public double CarrierToNoisePlusInterference { get; set; }
        public double EffectiveIsotropicRadiatedPower { get; set; }
        public double EnergyPerBitToNoiseDensity { get; set; }
        public double PowerAtReceiverOutput { get; set; }
        public double PropagationLoss { get; set; }
        public double ReceivedIsotropicPower { get; set; }
        public double ReceivedPowerFluxDensity { get; set; }
        public DateTime Time { get; set; }
        public LinkBudgetResultInterference(LinkBudgetResultAll fullResults)
        {
            BitErrorRate = fullResults.BitErrorRate;
            CarrierToInterference = fullResults.CarrierToInterference;
            CarrierToNoise = fullResults.CarrierToNoise;
            CarrierToNoiseDensity = fullResults.CarrierToNoiseDensity;
            CarrierToNoisePlusInterference = fullResults.CarrierToNoisePlusInterference;
            EffectiveIsotropicRadiatedPower = fullResults.EffectiveIsotropicRadiatedPower;
            EnergyPerBitToNoiseDensity = fullResults.EnergyPerBitToNoiseDensity;
            PowerAtReceiverOutput = fullResults.PowerAtReceiverOutput;
            PropagationLoss = fullResults.PropagationLoss;
            ReceivedIsotropicPower = fullResults.ReceivedIsotropicPower;
            ReceivedPowerFluxDensity = fullResults.ReceivedPowerFluxDensity;
            Time = fullResults.Time;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
