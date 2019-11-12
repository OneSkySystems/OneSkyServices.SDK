using Newtonsoft.Json;

namespace AgiServiceLibrary.Core.Models
{
    /// <summary>
    /// A time interval.
    /// </summary>
    public class ServiceTimeInterval
    {
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the stop time.
        /// </summary>
        public string Stop { get; set; }

        /// <summary>
        /// Gets or sets the duration of the TimeInterval, in seconds.
        /// </summary>
        public double Duration { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
