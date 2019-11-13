using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Outputs.Access;
using OneSky.Services.Util;


namespace OneSky.Services.Services.Overflight
{
    /// <summary>
    /// Overflight service methods.  See the service documentation for 
    /// notes on the different overflight service types: https://saas.agi.com/V1/Documentation/Overflight.
    /// </summary>
    public class OverflightServices
    {
        public static async Task<List<OverflightAccessResult<T>>> GetRegionalOverflight<T>(
                                                                    OverflightAccessData<IVerifiable> overflightData) where T:IPathResult{
            var relativeUri = string.Empty;
            
            overflightData.Verify();

            switch (overflightData.Path)
            {
                case PointToPointRouteData _:
                    relativeUri = ServiceUris.OverflightPointToPointUri;
                    break;
                case Sgp4RouteData _:
                    relativeUri = ServiceUris.OverflightSgp4Uri;
                    break;
                case SimpleFlightRouteData _:
                    relativeUri = ServiceUris.OverflightSimpleFlightUri;
                    break;
                case GreatArcRouteData _:
                    relativeUri = ServiceUris.OverflightGreatArcUri;
                    break;
                case CatalogRouteData _:
                    relativeUri = ServiceUris.OverflightCatalogObjectUri;
                    break;
            }

            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException(nameof(overflightData),overflightData.Path, 
                            overflightData.Path  + " is not a valid type for Overflight Access");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<OverflightAccessData<IVerifiable>, List<OverflightAccessResult<T>>>(uri, overflightData);
        }        
    }

}