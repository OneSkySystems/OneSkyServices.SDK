using System;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Communications
{
    public class LinkBudgetResultAll
    {
        public double TransmitterAntennaGainInLinkDirection { get; set; }
        public double ReceiverAntennaGainInLinkDirection { get; set; }
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

        public LinkBudgetResultAll(double transmitterAntennaGainInLinkDirection,
                                   double receiverAntennaGainInLinkDirection,
                                   double bitErrorRate,
                                   double carrierToInterference,
                                   double carrierToNoise,
                                   double carrierToNoiseDensity,
                                   double carrierToNoisePlusInterference,
                                   double effectiveIsotropicRadiatedPower,
                                   double energyPerBitToNoiseDensity,
                                   double powerAtReceiverOutput,
                                   double propagationLoss,
                                   double receivedIsotropicPower,
                                   double receivedPowerFluxDensity,
                                   DateTime time)
        {
            TransmitterAntennaGainInLinkDirection = transmitterAntennaGainInLinkDirection;
            ReceiverAntennaGainInLinkDirection = receiverAntennaGainInLinkDirection;
            BitErrorRate = bitErrorRate;
            CarrierToInterference = carrierToInterference;
            CarrierToNoise = carrierToNoise;
            CarrierToNoiseDensity = carrierToNoiseDensity;
            CarrierToNoisePlusInterference = carrierToNoisePlusInterference;
            EffectiveIsotropicRadiatedPower = effectiveIsotropicRadiatedPower;
            EnergyPerBitToNoiseDensity = energyPerBitToNoiseDensity;
            PowerAtReceiverOutput = powerAtReceiverOutput;
            PropagationLoss = propagationLoss;
            ReceivedIsotropicPower = receivedIsotropicPower;
            ReceivedPowerFluxDensity = receivedPowerFluxDensity;
            Time = time;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}