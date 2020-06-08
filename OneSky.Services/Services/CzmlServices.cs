using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Airspace;
using OneSky.Services.Outputs.Airspace;
using OneSky.Services.Inputs.Navigation;
using OneSky.Services.Outputs.Navigation;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Inputs.Visualization;
using OneSky.Services.Util;

namespace OneSky.Services.Services.Czml
{
    /// <summary>
    /// Czml methods.  See the service documentation for 
    /// notes on the different Czml call types: https://saas.onesky.xyz/V1/Documentation/Czml.
    /// </summary>
    public class CzmlServices
    {
        /// <summary>
        /// Gets orbits to visualize.
        /// </summary>
        /// <param name="czmlRouteData">Data defining the routes to visualize.</param>
        /// <returns>A Czml string.</returns>
        public static async Task<string> GetSgp4Czml(Sgp4CzmlRouteData czmlRouteData){
            czmlRouteData.Verify();                                   
            var uri = Networking.GetFullUri(ServiceUris.VehiclePathCzmlSgp4Uri);            
            return await Networking.HttpPostCall<Sgp4CzmlRouteData,string>(uri, czmlRouteData);
        }     

        /// <summary>
        /// Gets airspace shapes to visualize.
        /// </summary>
        /// <param name="czmlAirspaceData">Data defining the airspaces to visualize.</param>
        /// <returns>A string of Czml</returns>
        public static async Task<AirspaceCzml> GetAirspaceCzml(AirspaceCzmlData czmlAirspaceData){
            czmlAirspaceData.Verify();                                   
            var uri = Networking.GetFullUri(ServiceUris.AirspaceCzmlUri);            
            return await Networking.HttpPostCall<AirspaceCzmlData,AirspaceCzml>(uri, czmlAirspaceData);
        }  

       /// <summary>
       /// Gets Czml data representing the positions of Gps satellites in orbit
       /// </summary>
       /// <param name="czmlGpsData">Data defining the satellites to display</param>
       /// <param name="start">Start time for the orbits</param>
       /// <param name="stop">Stop time for the orbits</param>
       /// <param name="highlightOutages">When true, satellites with outages will be highlighted with a color.</param>
       /// <param name="outageColor">The color to use when highlighting a satellite orbit with an outage.</param>
       /// <param name="useInertial">When true, the orbits are shown in an inertial frame, an
       /// Earth-Fixed frame is used when false.</param>
       /// <returns>A string of Czml data.</returns>
       public static async Task<string> GetGpsOrbitsCzml(CzmlGpsOrbit czmlGpsData, DateTime start, DateTime stop, 
                                                        bool highlightOutages = true, string outageColor = "White",
                                                        bool useInertial = true){
            czmlGpsData.Verify();                                   
            var uri = Networking.GetFullUri(ServiceUris.VehiclePathCzmlGpsUri);      
            uri = new Uri(uri, $"&start={start:YYYY-MM-DDTHH:mm:ss}");
            uri = new Uri(uri,$"&stop={stop:YYYY-MM-DDTHH:mm:ss}");
            uri = new Uri(uri,$"&highlightOutages={highlightOutages}");
            uri = new Uri(uri,$"&outageColor={outageColor}");
            uri = new Uri(uri,$"&inertial={useInertial}");    
                                       
            return await Networking.HttpGetCall(uri);
        }
    }
}