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

            if (accessData.FromObjectPath is PointToPointRouteData)
            {
                relativeUri = ServiceUris.AccessSatellitePassesPointToPointUri;
            }
            else if (accessData.FromObjectPath is SiteData)
            {
                relativeUri = ServiceUris.AccessSatellitePassesSiteUri;
            }
            else if (accessData.FromObjectPath is Sgp4RouteData)
            {
                relativeUri = ServiceUris.AccessSatellitePassesSgp4Uri;
            }
            else if (accessData.FromObjectPath is SimpleFlightRouteData)
            {
                relativeUri = ServiceUris.AccessSatellitePassesSimpleFlightUri;
            }
            else if (accessData.FromObjectPath is TolRouteData)
            {
                relativeUri = ServiceUris.AccessSatellitePassesTolUri;
            }
            else if (accessData.FromObjectPath is RasterRouteData)
            {
                relativeUri = ServiceUris.AccessSatellitePassesRasterUri;
            }
            else if (accessData.FromObjectPath is GreatArcRouteData)
            {
                relativeUri = ServiceUris.AccessSatellitePassesGreatArcUri;
            }
            else if (accessData.FromObjectPath is CatalogRouteData)
            {
                relativeUri = ServiceUris.AccessSatellitePassesCatalogObjectUri;
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