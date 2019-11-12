namespace OneSky.Services.Outputs.Communications
{
    public enum LinkBudgetType
    {
        TransmitterAntennaGainInLinkDirection,
        ReceiverAntennaGainInLinkDirection,
        BitErrorRate,
        CarrierToInterference,
        CarrierToNoise,
        CarrierToNoisePlusInterference,
        CarrierToNoiseDensity,
        EffectiveIsotropicRadiatedPower,
        EnergyPerBitToNoiseDensity,
        PowerAtReceiverOutput,
        PropagationLoss,
        ReceivedIsotropicPower,
        ReceivedPowerFluxDensity
    }
}
