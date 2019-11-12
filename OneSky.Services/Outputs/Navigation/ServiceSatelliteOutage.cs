using System;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    /// <summary>
    /// Defines a Satellite Outage for a GPS satellite
    /// </summary>
    public class ServiceSatelliteOutage
    {
        /// <summary>
        /// start time of the outage
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// Stop time of the outage
        /// </summary>
        public DateTime Stop { get; set; }
        /// <summary>
        /// The number of the NANU defining the outage
        /// </summary>
        public string NanuNumber { get; set; }
        /// <summary>
        /// Link to the Nanu file
        /// </summary>
        public string NanuLink { get; set; }
        /// <summary>
        /// The type of Nanu for this outage
        /// </summary>
        public string NanuType { get; set; }
        /// <summary>
        /// The Satellite Vehicle Number of the GPS satellite that is out.
        /// </summary>
        public int Svn { get; set; }
        /// <summary>
        /// The Pseudo random number of the satellite that is out.
        /// </summary>
        public int Prn { get; set; }
        /// <summary>
        /// The duration of the outage, in seconds.
        /// </summary>
        public double Duration { get; set; }
        /// <summary>
        /// The type of outage, historical, current or predicted.
        /// </summary>
        public string OutageType { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
