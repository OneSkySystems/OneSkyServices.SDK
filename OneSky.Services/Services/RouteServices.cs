using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Util;

namespace OneSky.Services.Services.Routing
{
    /// <summary>
    /// A propagated route, defined by a few waypoints.  See the service documentation for 
    /// notes on the different route types: https://saas.agi.com/V1/Documentation/Routing.
    /// </summary>
    public class RouteServices
    {
        /// <summary>
        /// Gets a propagated route.
        /// </summary>
        /// <param name="routeData">The data defining the route.</param>
        /// <typeparam name="T">The input route data type. Examples might be PointToPointRouteData
        /// or Sgp4Data.</typeparam>
        /// <typeparam name="R">The output type. This must be consistent with <see cref="CoordinateFormat"/></typeparam>
        /// in <see cref="OutputSettings"/>
        /// <returns>A Task containing a List of type R results. R is typically 
        /// <see cref="ServiceCartographicWithTime"/> or a similar type.
        /// </returns>
        public static async Task<List<R>> GetRoute<T,R>(T routeData) where T: IVerifiable
        {
            routeData.Verify();

            string relativeUri = string.Empty;
            if(typeof(T) == typeof(PointToPointRouteData)){
                relativeUri = ServiceUris.PointToPointRouteUri;
            }
            else if(typeof(T) == typeof(Sgp4RouteData)){
                relativeUri = ServiceUris.Sgp4RouteUri;
            }
            else if(typeof(T) == typeof(TolRouteData)){
                relativeUri = ServiceUris.TolRouteUri;
            }
            else if(typeof(T) == typeof(RasterRouteData)){
                relativeUri = ServiceUris.RasterRouteUri;
            }
            else if(typeof(T) == typeof(GreatArcRouteData)){
                relativeUri = ServiceUris.GreatArcRouteUri;
            }
            else if(typeof(T) == typeof(SimpleFlightRouteData)){
                relativeUri = ServiceUris.SimpleFlightRouteUri;
            }
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("routeData",typeof(T), 
                            typeof(T) + " is not a valid type for route generation");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<T, List<R>>(uri, routeData);
        }
    }

}