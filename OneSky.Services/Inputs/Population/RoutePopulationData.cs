using OneSky.Services.Exceptions;

namespace OneSky.Services.Inputs.Population
{
    public class RoutePopulationData<T>: IVerifiable 
        where T: IVerifiable
    {
        /// <summary>
        /// Whether the request is for density or count data
        /// </summary>
        public PopulationDataType PopulationType { get; set; }
        /// <summary>
        /// The location at or along which population results are desired.
        /// </summary>
        public T Path { get; set; }
        /// <summary>
        /// When true, the path through the population grid will be included in the result, with individual pop values.
        /// This will have the same value for Point flights and the point Flight service
        /// </summary>
        public bool IncludePath { get; set; }

        public RoutePopulationData()
        {
            IncludePath = false;
        }

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
