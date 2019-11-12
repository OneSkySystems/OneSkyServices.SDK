namespace OneSky.Services.Inputs.Navigation
{
    public interface IReceiverParameters
    {
        int NumberOfChannels {get; set;}
        bool BestN { get; set; }
        double ReceiverNoiseError { get; set; }
        double MinimumElevationAngle { get; set; }
    }
}
