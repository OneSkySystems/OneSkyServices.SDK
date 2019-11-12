using System;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Communications
{
    public class LinkBudgetResultAllWithLocation : LinkBudgetResultAll
    {
        public ServiceCartographic Location { get; set; }
        public LinkBudgetResultAllWithLocation(double transmitterAntennaGainInLinkDirection, 
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
                                               DateTime time,
                                               ServiceCartographic location)
            : base(transmitterAntennaGainInLinkDirection,
                   receiverAntennaGainInLinkDirection,
                   bitErrorRate,
                   carrierToInterference,
                   carrierToNoise,
                   carrierToNoiseDensity,
                   carrierToNoisePlusInterference,
                   effectiveIsotropicRadiatedPower,
                   energyPerBitToNoiseDensity,
                   powerAtReceiverOutput,
                   propagationLoss,
                   receivedIsotropicPower,
                   receivedPowerFluxDensity,
                   time)
        {
            Location = location;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}