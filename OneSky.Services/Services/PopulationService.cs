using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Inputs.Population;
using OneSky.Services.Outputs.Population;
using OneSky.Services.Util;

namespace OneSky.Services.Services.Lighting
{
    /// <summary>
    /// Population service methods.  See the service documentation for 
    /// notes on the different population service types: https://saas.agi.com/V1/Documentation/Population.
    /// </summary>
    public class PopulationServices
    {
        public static async Task<PopulationResults> GetPopulationAtASite(PointPopulationData sitePopulationData){
       
            var uri = Networking.GetFullUri(ServiceUris.PopulationSiteUri);
            return await Networking.HttpPostCall<PointPopulationData,PopulationResults>(uri, sitePopulationData);
        }  

        public static async Task<PopulationResults> GetPopulationAlongARoute(
                                                                RoutePopulationData<IVerifiable> routePopulationData){
            routePopulationData.Verify();
            var uri = Networking.GetFullUri(ServiceUris.PopulationPointToPointUri);
            return await Networking.HttpPostCall<RoutePopulationData<IVerifiable>,
                                                                        PopulationResults>(uri, routePopulationData);
        }  
    }
}