using System;

namespace OneSky.Services.Inputs.Lighting
{
    /// <summary>
    /// A Request for the Solar Lighting Service
    /// </summary>
    public class SolarLightingData<T> where T: IVerifiable
    {
        /// <summary>
        /// The path along which Solar lighting will be calculated.  The path can be either a 
        /// <see cref="Routing.PointToPointRouteData"/> or a <see cref="Routing.SiteData"/>. The start and stop times
        /// of the path dictate the analysis start and stop times when Path is a 
        /// <see cref="Routing.PointToPointRouteData"/>. If Path is set to a <see cref="Routing.SiteData"/>, both
        /// <see cref="AnalysisStart"/> and <see cref="AnalysisStop"/> must be set in this class. They are ignored otherwise.
        /// </summary>
        public T Path { get; set; }
        /// <summary>
        /// The start time for the analysis.  This is only used if the PathLocation is a <see cref="Routing.SiteData"/>.
        /// </summary>
        public DateTimeOffset AnalysisStart { get; set; }
        /// <summary>
        /// The stop time for the analysis.  This is only used if the PathLocation is a <see cref="Routing.SiteData"/>.
        /// </summary>
        public DateTimeOffset AnalysisStop { get; set; }
        /// <summary>
        /// Lighting times are output in UTC by default, for any location.
        /// Set this value to the UTC hours offset for your location to return results in local time.
        /// Defaults to 0.
        /// </summary>
        public float OutputTimeOffset { get; set; }

        public void Verify() => Path.Verify();
    }
}


