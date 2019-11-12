
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Airspace
{
    public class StaticAirspaceRouteData<T>: StaticAirspaceData, IVerifiable
        where T: IVerifiable
    {
        /// <summary>
        /// The location at or along which airspace Proximity results are desired.
        /// </summary>
        public T Path { get; set; }
        /// <summary>
        /// When true, the path through the airspace (or it's proximate area) will be included in the result.
        /// </summary>
        public bool IncludePath { get; set; }
       

        public StaticAirspaceRouteData()
        {
            IncludePath = false;
        }

        public new void Verify()
        {
            base.Verify();

            if (Path != null)
                Path.Verify();
            else
            {
                throw new AnalyticalServicesException(22200, "Path must not be null.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}