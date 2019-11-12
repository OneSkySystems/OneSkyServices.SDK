using System;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    /// <summary>
    /// Navigation error results for a single location
    /// </summary>
    public class NavigationDop
    {
        public DateTime Time { get; set; }
        public double Hdop { get; set; }
        public double Vdop { get; set; }
        public double Tdop { get; set; }
        public double Pdop { get; set; }
        public double Xdop { get; set; }
        public double Ydop { get; set; }
        public int NumberOfSatellites { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}