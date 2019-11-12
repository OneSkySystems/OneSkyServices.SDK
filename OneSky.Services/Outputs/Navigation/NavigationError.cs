using System;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    /// <summary>
    /// Navigation error results for a single location
    /// </summary>
    public class NavigationError : INavigationError
    {
        public DateTime Time { get; set; }
        public double HorizontalError { get; set; }
        public double VerticalError { get; set; }
        public double TimeError { get; set; }
        public double PositionError { get; set; }
        public double XError { get; set; }
        public double YError { get; set; }
        public int NumberOfSatellites { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }    
}