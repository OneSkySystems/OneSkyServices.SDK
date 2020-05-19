using System;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Routing
{
    /// <summary>
    /// A raster search grid route.
    /// </summary>
    public class RasterRouteData :IVerifiable
    {
        /// <summary>
        /// The time to start traveling from the beginning of the route.
        /// </summary>
        public DateTimeOffset Start {get; set;}        
        /// <summary>
        /// The center of the region to search.
        /// </summary>
        public ServiceCartographic2D CenterPoint { get; set; }
        /// <summary>
        /// The heading of the route (and the vehicle) as it begins the route. This defines the heading
        /// of all raster lines in the search grid.
        /// </summary>
        public double SearchHeading { get; set; }
        /// <summary>
        /// The distance of each raster line, centered on <see cref="CenterPoint"/>, in meters.
        /// Defaults to 0.
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// The width of the search area, in meters.
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// The radius of the turns between raster lines, in meters. Defaults to 200.
        /// </summary>
        public double TurningRadius { get; set; }
        /// <summary>
        /// Speed of the vehicle in meters/second. Defaults to 65.
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// The conatant altitude of the raster search pattern route. Defaults to 1000.
        /// </summary>
        public double Altitude {get; set;}
        /// <summary>
        /// Set to true if the <see cref="Altitude" /> is referenced to Mean Sea Level, false
        /// if reference to WGS-84. Defaults to true.
        /// </summary>
        public bool MeanSeaLevel {get; set;}
        /// <summary>
        /// The output settings for this route.
        /// </summary>
        public OutputSettings OutputSettings { get; set; }

        public RasterRouteData()
        {
            SearchHeading = 0;
            TurningRadius = 200;
            Speed = 65;
            Altitude = 1000;
            MeanSeaLevel = true;
            OutputSettings = new OutputSettings();
        }

        public void Verify()
        {
            if (CenterPoint == null)
            {
                throw new AnalyticalServicesException(24000, "CenterPoint must be supplied to the service.");
            }

            CenterPoint.Verify();

            if (SearchHeading < 0 || SearchHeading > 360)
            {
                throw new AnalyticalServicesException(23600, "SearchHeading must be in the interval: [0-360].");
            }

            if (Length <= 0)
            {
                throw new AnalyticalServicesException(23600, "Length must be greater than 0.");
            }
            if (Width <= 0)
            {
                throw new AnalyticalServicesException(23600, "Width must be greater than 0.");
            }
            if (Speed <= 0)
            {
                throw new AnalyticalServicesException(23600, "Speed must be greater than 0.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
