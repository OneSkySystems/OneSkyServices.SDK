using OneSky.Services.Exceptions;
using OneSky.Services.Inputs.Routing;

namespace OneSky.Services.Inputs.Population
{
    public class PointPopulationData
    {
        /// <summary>
        /// Whether the request is for density or count data
        /// </summary>
        public PopulationDataType PopulationType { get; set; }
        /// <summary>
        /// The location at or along which population results are desired.
        /// </summary>
        public SiteData Path { get; set; }

        /// <summary>
        /// The radius of the cylinder the point flight is flying in
        /// </summary>
        public double PointFlightRadius { get; set; }

        public void Verify()
        {
            if (Path != null)
                Path.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "Path must not be null.");
            }
        }
    }
}
