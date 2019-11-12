using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Communications
{
    public class TiremData
    {
        /// <summary>
        /// The conductivity of the Earth's surface.  
        /// The valid range of surface conductivity is 0.00001 to 100 Siemens/meter.  Defaults to 0.005.
        /// </summary>
        public double SurfaceConductivity { get; set; }
        /// <summary>
        /// The humidity of the Earth's surface in grams/meter^3. Valid range is 0.0 to 110.0, the default is 64.0.
        /// </summary>
        public double SurfaceHumidity { get; set; }
        /// <summary>
        /// The refractivity of the Earth's surface. The valid range is 200.0 to 450.0 N-units. default is 310.0
        /// </summary>
        public double SurfaceRefractivity { get; set; }
        /// <summary>
        /// The valid range is 1.0 to 100.0, defaults to 15.0
        /// </summary>
        public double SurfaceRelativePermittivity { get; set; }

        public TiremData()
        {
            SurfaceConductivity = 0.005;
            SurfaceHumidity = 64.0; 
            SurfaceRefractivity = 310.0;
            SurfaceRelativePermittivity = 15.0;
        }

        public void Verify()
        {
            if (SurfaceConductivity < 0.00001 || SurfaceConductivity > 100.0)
            {
                throw new AnalyticalServicesException(23600, 
                    "SurfaceConductivity must be greater than 0.00001 and less than 100.0 Siemans/meter");
            }

            if (SurfaceHumidity < 0.0 || SurfaceHumidity > 110.0)
            {
                throw new AnalyticalServicesException(23600,
                     "SurfaceHumidity must be greater than 0.0 and less than 110.0 grams/meter^3.");
            }

            if (SurfaceRefractivity < 200 || SurfaceRefractivity > 450.0)
            {
                throw new AnalyticalServicesException(23600, 
                    "SurfaceRefractivity must be greater than 200.0 and less than 450.0.");
            }

            if (SurfaceRelativePermittivity < 1.0 || SurfaceRelativePermittivity > 110.0)
            {
                throw new AnalyticalServicesException(23600, 
                    "SurfaceRelativePermittivity must be greater than 1.0 and less than 100.0.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
