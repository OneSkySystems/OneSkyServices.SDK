using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Communications
{
    public class CommunicationsExtremes
    {
        public ExtremesInfo TransmitterAntennaGainInLinkDirectionExtremes { get; set; }
        public ExtremesInfo ReceiverAntennaGainInLinkDirectionExtremes { get; set; }
        public ExtremesInfo BitErrorRateExtremes { get; set; }
        public ExtremesInfo CarrierToInterferenceExtremes { get; set; }
        public ExtremesInfo CarrierToNoiseExtremes { get; set; }
        public ExtremesInfo CarrierToNoiseDensityExtremes { get; set; }
        public ExtremesInfo CarrierToNoisePlusInterferenceExtremes { get; set; }
        public ExtremesInfo EffectiveIsotropicRadiatedPowerExtremes { get; set; }
        public ExtremesInfo EnergyPerBitToNoiseDensityExtremes { get; set; }
        public ExtremesInfo PowerAtReceiverOutputExtremes { get; set; }
        public ExtremesInfo PropagationLossExtremes { get; set; }
        public ExtremesInfo ReceivedIsotropicPowerExtremes { get; set; }
        public ExtremesInfo ReceivedPowerFluxDensityExtremes { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
