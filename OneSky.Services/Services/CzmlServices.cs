using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Airspace;
using OneSky.Services.Outputs.Airspace;
using OneSky.Services.Inputs.Navigation;
using OneSky.Services.Outputs.Navigation;
using OneSky.Services.Inputs.Routing;

using OneSky.Services.Util;

namespace OneSky.Services.Services.Czml
{
    /// <summary>
    /// Czml methods.  See the service documentation for 
    /// notes on the different Czml call types: https://saas.agi.com/V1/Documentation/Czml.
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
       /// <param name="stop">Stop time for th eorbits</param>
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
            uri = new Uri(uri, $"&start={start.ToString("YYYY-MM-DDTHH:mm:ss")}");
            uri = new Uri(uri,$"&stop={stop.ToString("YYYY-MM-DDTHH:mm:ss")}");
            uri = new Uri(uri,$"&highlightOutages={highlightOutages}");
            uri = new Uri(uri,$"&outageColor={outageColor}");
            uri = new Uri(uri,$"&inertial={useInertial}");    
                                       
            return await Networking.HttpGetCall(uri);
        }  

        /// <summary>
        /// Gets Czml data representing global GPS acuracy for a given date
        /// </summary>
        /// <param name="date">the date the global Gps accuracy is desired</param>
        /// <param name="animated">When true, the results will animate for the 24 hour period</param>
        /// <param name="useSmallGrid">When true, the data is plotted on a smaller grid.</param>
        /// <returns>A string of Czml data</returns>
         public static async Task<string> GetGpsGlobalAccuracyCzml(DateTime date, 
                                                                   bool animated = false,
                                                                   bool useSmallGrid = false){                                
            var uri = Networking.GetFullUri(ServiceUris.VehiclePathCzmlGpsUri); 
            uri = Networking.AppendDateToUri(uri,date);

            uri = new Uri(uri,$"&animated={animated}");
            uri = new Uri(uri,$"&useSmallGrid={useSmallGrid}");    
                                       
            return await Networking.HttpGetCall(uri);
        }  
    }
}