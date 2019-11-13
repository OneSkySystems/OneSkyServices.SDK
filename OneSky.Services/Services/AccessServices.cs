using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Util;

namespace OneSky.Services.Services.Access
{
    /// <summary>
    /// Access methods.  See the service documentation for 
    /// notes on the different route types: https://saas.agi.com/V1/Documentation/Access.
    /// </summary>
    public class AccessServices
    {
        //todo add SensorFOR service here
        public static async Task<R> GetSatellitePasses<R>(SatelliteAccessPassData<IVerifiable> accessData)
        {
            string relativeUri = string.Empty;

            accessData.Verify();

            switch (accessData.FromObjectPath)
            {
                case PointToPointRouteData _:
                    relativeUri = ServiceUris.AccessSatellitePassesPointToPointUri;
                    break;
                case SiteData _:
                    relativeUri = ServiceUris.AccessSatellitePassesSiteUri;
                    break;
                case Sgp4RouteData _:
                    relativeUri = ServiceUris.AccessSatellitePassesSgp4Uri;
                    break;
                case SimpleFlightRouteData _:
                    relativeUri = ServiceUris.AccessSatellitePassesSimpleFlightUri;
                    break;
                case TolRouteData _:
                    relativeUri = ServiceUris.AccessSatellitePassesTolUri;
                    break;
                case RasterRouteData _:
                    relativeUri = ServiceUris.AccessSatellitePassesRasterUri;
                    break;
                case GreatArcRouteData _:
                    relativeUri = ServiceUris.AccessSatellitePassesGreatArcUri;
                    break;
                case CatalogRouteData _:
                    relativeUri = ServiceUris.AccessSatellitePassesCatalogObjectUri;
                    break;
            }

            if (string.IsNullOrEmpty(relativeUri))
            {
                throw new ArgumentOutOfRangeException("accessData", accessData.FromObjectPath,
                            accessData.FromObjectPath + " is not a valid type for Satellite Passes");
            }

            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<SatelliteAccessPassData<IVerifiable>, R>(uri, accessData);
        }
    }

}