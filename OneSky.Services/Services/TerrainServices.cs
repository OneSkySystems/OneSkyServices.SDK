using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Terrain;
using OneSky.Services.Util;

namespace OneSky.Services.Services.Terrain
{
    /// <summary>
    /// Terrain methods.  See the service documentation for 
    /// notes on the different Terrain call types: https://saas.agi.com/V1/Documentation/Terrain.
    /// </summary>
    public class TerrainServices
    {
        /// <summary>
        /// Gets terrain heights at a site to visualize.
        /// </summary>
        /// <param name="terrainData">Data defining the routes to visualize.</param>
        /// <typeparam name="T">The input route data type. Only PointToPoint and GreatArc routes are allowed.
        /// or Sgp4Data.</typeparam>
        /// <returns>A Czml string.</returns>
        public static async Task<List<TerrainHeightAtLocationResponse>> 
                                            GetTerrainHeightsAlongARoute<T>(T terrainRouteData) where T: IVerifiable{

            terrainRouteData.Verify();      

            string relativeUri = string.Empty;
            if(typeof(T) == typeof(PointToPointRouteData)){
                relativeUri = ServiceUris.TerrainHeightsPointToPointUri;
            }          
            else if(typeof(T) == typeof(GreatArcRouteData)){
                relativeUri = ServiceUris.TerrainHeightsGreatArcUri;
            }
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("terrainRouteData",typeof(T), 
                            typeof(T) + " is not a valid type for terrain route generation");
            }

            var uri = Networking.GetFullUri(relativeUri);            
            return await Networking.HttpPostCall<T,List<TerrainHeightAtLocationResponse>>(uri, terrainRouteData);
        }     

         public static async Task<Heights> GetTerrainHeightsAtASite(double latitude, double longitude){                                
            UriBuilder ub = new UriBuilder(Networking.GetFullUri(ServiceUris.TerrainHeightsSiteUri));
            var latQuery = $"&latitude={latitude}";
            var longQuery = $"&longitude={longitude}";
            ub.Query = ub.Query.Substring(1) + latQuery + longQuery;
                                       
            return await Networking.HttpGetCall<Heights>(ub.Uri);
        }  
    }
}