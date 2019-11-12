using System;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs.Routing;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Communications
{
    public class ReceiverData:IVerifiable
    {
        /// <summary>
        /// The location at or along which the receiver travels.
        /// </summary>
        public IVerifiable Path { get; set; }
        /// <summary>
        /// The type of path this receiver uses.
        /// </summary>
        public RouteTypes PathRouteType { get; set; }
        /// <summary>
        /// The target frequency used as the center frequency for the bandwidth, in hertz. Default is 14.5 Ghz.
        /// </summary>
        public double TargetFrequency { get; set; }
        /// <summary>
        /// The full bandwidth this receiver uses, centered on TargetFrequency, in hertz. Default is 20 Mhz.
        /// </summary>
        public double Bandwidth { get; set; }
        /// <summary>
        /// The amplifier gain this receiver uses, in linear units. Default is 100.
        /// </summary>
        public double AmplifierGain { get; set; }
        /// <summary>
        /// The NoiseFactor for this receiver.  A Noise Factor of 1 represents no noise. 
        /// A noise factor of 2 represents noise from the ambient temperature divided by the reference temperature. 
        /// The relationship between noise factor and temperature is: 
        /// NoiseFactor = 1 + NoiseTemperature/ReferenceTemperature. 
        /// Default is 2.
        /// </summary>
        public double NoiseFactor { get; set; }
        /// <summary>
        /// The temperature value used to determine the noise temperature, 
        /// in conjunction with the NoiseFactor, in Celsius. Default is 16.85 Celsius.
        /// </summary>
        public double ReferenceTemperature { get; set; }

        public ReceiverData()
        {
            TargetFrequency = 14.5e9;
            Bandwidth = 2e7; // 20 Mhz
            AmplifierGain = 100;
            NoiseFactor = 2;
            ReferenceTemperature = 16.85;
        }

        public void Verify()
        {
            if (Path != null)
                Path.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "Path must not be null.");
            }

            if (Bandwidth <= 0)
            {
                throw new AnalyticalServicesException(23600, "Bandwidth must be greater than 0.");
            }

            if (NoiseFactor < 1)
            {
                throw new AnalyticalServicesException(23600, "NoiseFactor must be greater than or equal to 1.");
            }

            if (ReferenceTemperature < -273.15)
            {
                throw new AnalyticalServicesException(23600, 
                    "ReferenceTemperature must be greater than absolute zero.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
