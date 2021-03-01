using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Communications
{
    /// <summary>
    /// High-level communications service input class
    /// </summary>
    public class CommunicationData
    {
        /// <summary>
        /// The start time for the communication analysis, when the receiver is a Fixed Site only
        /// </summary>
        public DateTime AnalysisStart { get; set; }

        /// <summary>
        /// The stop time for the communication analysis, when the receiver is a Fixed Site only
        /// </summary>
        public DateTime AnalysisStop { get; set; }
        /// <summary>
        /// When true, the Terrain Integrated Rough Earth Model (TIREM) will be used to 
        /// determine attenuation of comm links due to surrounding terrain
        /// </summary>
        public bool UseTirem { get; set; }
        /// <summary>
        /// The output units for appropriate link budgets. Default is Decibels
        /// </summary>
        public OutputUnit OutputUnits { get; set; }

        /// <summary>
        /// The transmitter used in the communication analysis
        /// </summary>
        public TransmitterData Transmitter { get; set; }
        /// <summary>
        /// The interference sources used in the communication analysis
        /// </summary>
        public List<TransmitterData> InterferenceSources { get; set; }

        /// <summary>
        /// The receiver used in the communication analysis
        /// </summary>
        public ReceiverData Receiver { get; set; }

        /// <summary>
        /// Settings used to define how TIREM works.
        /// </summary>
        public TiremData TiremSettings { get; set; }

        public CommunicationData()
        {
            UseTirem = false;
            TiremSettings = new TiremData();
            InterferenceSources = new List<TransmitterData>();
            OutputUnits = OutputUnit.Decibels;
        }

        public void Verify()
        {
            Transmitter.Verify();
            Receiver.Verify();
            foreach (var interferenceSource in InterferenceSources)
            {
                interferenceSource.Verify();
            }
            TiremSettings.Verify();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, 
                    new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}
