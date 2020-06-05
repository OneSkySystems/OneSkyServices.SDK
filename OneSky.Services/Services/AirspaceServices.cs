using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Airspace;
using OneSky.Services.Outputs.Airspace;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Util;

namespace OneSky.Services.Services.Airspace
{
    /// <summary>
    /// Airspace methods.  See the service documentation for 
    /// notes on the different airspace call types: https://saas.onesky.xyz/V1/Documentation/Airspace.
    /// </summary>
    public class AirspaceServices
    {
        public static async Task<StaticAirspaceAccessResult<AirspaceCrossingResult<IPathResult>>> 
            GetAirspaceCrossingsForARoute(StaticAirspaceRouteData<IVerifiable> airspaceRouteData){
            var relativeUri = string.Empty;
            
            airspaceRouteData.Verify();
            
            if(airspaceRouteData.Path.GetType() == typeof(PointToPointRouteData)){
                relativeUri = ServiceUris.AccessSatellitePassesPointToPointUri;
            }
            else if(airspaceRouteData.Path.GetType() == typeof(SimpleFlightRouteData)){
                relativeUri = ServiceUris.AccessSatellitePassesSimpleFlightUri;
            }
            else if(airspaceRouteData.Path.GetType() == typeof(GreatArcRouteData)){
                relativeUri = ServiceUris.AccessSatellitePassesGreatArcUri;
            }
            
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("airspaceRouteData",airspaceRouteData.Path.GetType(), 
                            airspaceRouteData.Path.GetType()  + " is not a valid type for airspace crossings");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            
            return await 
            Networking.HttpPostCall<StaticAirspaceRouteData<IVerifiable>, 
                            StaticAirspaceAccessResult<AirspaceCrossingResult<IPathResult>>>(uri, airspaceRouteData);
        }     

        public static async Task<StaticAirspaceAccessResult<AirspaceIdentifier>> 
            GetAirspaceCrossingsForACylinder(StaticAirspacePointFlightData airspacePfData){

            var relativeUri = ServiceUris.AirspacePointFlightUri;            
            airspacePfData.Verify();                     
            var uri = Networking.GetFullUri(relativeUri);            
            return await Networking.HttpPostCall<StaticAirspacePointFlightData, 
                    StaticAirspaceAccessResult<AirspaceIdentifier>>(uri, airspacePfData);
        }      

        public static async Task<AirspaceIdResult> SelectAirspaces(AirspaceSelectionOptions seletionOptions)  {
            seletionOptions.Verify();
            var uri = Networking.GetFullUri(ServiceUris.AirspaceSelectAirspacesUri);            
            return await Networking.HttpPostCall<AirspaceSelectionOptions, AirspaceIdResult>(uri, seletionOptions);
        }

        public static async Task<AirspaceIdResult> RealTime(StaticAirspaceRealTimeData realTimeData)  {
            realTimeData.Verify();
            var uri = Networking.GetFullUri(ServiceUris.AirspaceRealTimeUri);            
            return await Networking.HttpPostCall<StaticAirspaceRealTimeData, AirspaceIdResult>(uri, realTimeData);
        }
    }

}