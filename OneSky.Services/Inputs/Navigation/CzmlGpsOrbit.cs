using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Navigation
{
    public class CzmlGpsOrbit : IVerifiable
    {
        /// <summary>
        /// The start time for Gps orbit(s)
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// The stop time for Gps orbit(s)
        /// </summary>
        public DateTime Stop { get; set; }
        /// <summary>
        /// List of Prns to generate orbits for. (Optional, if empty, all Gps orbits will be generated)
        /// </summary>
        public List<int> Prns { get; set; }
        /// <summary>
        /// List of colors to apply to each Prn. (Optional, if empty, Gps orbits will be colored by block type)
        /// </summary>
        public List<Color> PrnColors { get; set; }
        /// <summary>
        /// Specify whether to highlight Gps satellite orbits when they are set unhealthy. Optional, default is true.
        /// </summary>
        public bool HighlightOutages { get; set; }
        /// <summary>
        /// The color of the Gps orbit when the satellite is unhealthy. (Default is Red);
        /// </summary>
        public Color OutageHighlightColor { get; set; }
        /// <summary>
        /// Specify whether the orbit paths are displayed using Inertial (true) or Fixed (false) coordinate. Optional, default is true.
        /// </summary>
        public bool Inertial { get; set; }
        /// <summary>
        /// Path (file path, Http, Ftp, etc.) for the SEM almanac if a specific almanac is to be used.  Optional. If this property is set, no other SEM almanac will be used.  If it is not set, the best almanac for the <paramref name="Start"/> date will be used.
        /// </summary>
        public string SemAlmanacLocation { get; set; }
        /// <summary>
        /// Gets or Sets the path (file path, Http, Ftp, etc.) for the GpsData file from AGI.  Optional. If this property is not set, the latest GpsData.txt file will be used.
        /// </summary>
        public string GpsDataFileLocation { get; set; }
        /// <summary>
        /// Path (file path, Http, Ftp, etc.) for the Satellite Outage File (SOF) if a specific SOF is to be used. Optional. If this property is set, no other SOF will be used. If is not set, the latest SOF will be used to determine Gps Outages
        /// </summary>
        public string SofLocation { get; set; }
        /// <summary>
        /// When true, the best available data will be retrieved from the internet if no data is available for the date requested. Defaults to true.
        /// </summary>
        public bool UseBestAvailableData { get; set; }

        public CzmlGpsOrbit()
	    {
            Prns = new List<int>();
            PrnColors = new List<Color>();
            HighlightOutages = true;
            Inertial = true;
            OutageHighlightColor = Color.Red;
            SemAlmanacLocation = string.Empty;
            SofLocation = string.Empty;
	        UseBestAvailableData = true;
	    }

        public void Verify()
        {
            if (Prns.Count == PrnColors.Count) return;
            throw new AnalyticalServicesException(25100, 
                "The Prns and PrnColors lists must contain the same number of elements");
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
