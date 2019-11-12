using System;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs.Routing;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Communications
{
    public class TransmitterData :  IVerifiable
    {
        /// <summary>
        /// The location at or along which the transmitter resides.
        /// </summary>
        public IVerifiable Path { get; set; }
        /// <summary>
        /// The type of path this transmitter uses.
        /// </summary>
        public RouteTypes PathRouteType { get; set; }
        /// <summary>
        /// The frequency this transmitter broadcasts on, in hertz. Default is 14.5 Ghz.
        /// </summary>
        public double Frequency { get; set; }
        /// <summary>
        /// The power this transmitter broadcasts with, in watts.  
        /// The power is radiated isotropically, this value corresponds to to its EIRP. 
        /// Default is 1000 watts.
        /// </summary>
        public double Power { get; set; }
        /// <summary>
        /// The rate at which the data is modulated onto the digital carrier, using BPSK modulation, in bits per second.
        /// Default is 16 Mbps.
        /// </summary>
        public double DataRate { get; set; }

        public TransmitterData()
        {
            DataRate = 16e6;
            Frequency = 14.5e9;
            Power = 1000;
        }

        public void Verify()
        {
            if (Path != null)
                Path.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "Path must not be null.");
            }

            if (Frequency <= 0)
            {
                throw new AnalyticalServicesException(23600, "Frequency must be greater than 0.");
            }

            if (Power <= 0)
            {
                throw new AnalyticalServicesException(23600, "Power must be greater than or equal to 0.");
            }

            if (DataRate <= 0)
            {
                throw new AnalyticalServicesException(23600, "DataRate must be greater than or equal to 0.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
