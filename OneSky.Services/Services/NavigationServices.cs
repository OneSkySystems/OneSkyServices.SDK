using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Navigation;
using OneSky.Services.Outputs.Navigation;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Util;

namespace OneSky.Services.Services.Navigation
{
    /// <summary>
    /// Navigation methods.  See the service documentation for 
    /// notes on the different navigation services: https://saas.agi.com/V1/Documentation/Navigation.
    /// </summary>
    public class NavigationServices
    {
        public static async Task<string> GetPsfData(DateTime? timeOfPsf = null){
            var uri = Networking.GetFullUri(ServiceUris.NavigationPsfDataUri);
            uri = timeOfPsf == null ? uri : Networking.AppendDateToUri(uri,timeOfPsf);
            return await Networking.HttpGetCall(uri);
        } 
        public static async Task<string> GetPafData(DateTime? timeOfPaf = null){
            var uri = Networking.GetFullUri(ServiceUris.NavigationPafDataUri);
            uri = timeOfPaf == null ? uri : Networking.AppendDateToUri(uri,timeOfPaf);
            return await Networking.HttpGetCall(uri);
        }  
        public static async Task<string> GetSofData(DateTime? timeOfSof = null){
            var uri = Networking.GetFullUri(ServiceUris.NavigationSofDataUri);
            uri = timeOfSof == null ? uri : Networking.AppendDateToUri(uri,timeOfSof);
            return await Networking.HttpGetCall(uri);
        } 
        public static async Task<string> GetAlmanacData(DateTime? timeOfAlmanac = null){
            var uri = Networking.GetFullUri(ServiceUris.NavigationAlmanacDataUri);
            uri = timeOfAlmanac == null ? uri : Networking.AppendDateToUri(uri,timeOfAlmanac);
            return await Networking.HttpGetCall(uri);
        }   

    /// <summary>
    /// Returns Gps Outages times for a time interval or a prn, or a prn within a time interval.
    /// </summary>
    /// <param name="fromTime">The start time to look for outages.</param>
    /// <param name="toTime">The end time to look for outages.</param>
    /// <param name="prn">The prn whose outages are desired.</param>
    /// <remarks>Note that either a from/to time or a prn (or both) must be supplied</remarks>
    /// <returns>A string containing Gps outages.</returns>
        public static async Task<string> GetGpsOutages(DateTime? fromTime = null, 
                                                       DateTime? toTime = null,
                                                       int? prn = null){
            if(fromTime == null && toTime == null && prn == null){
                throw new ArgumentNullException("fromTime",
                                                "You must specify either a from and to date or a prn number.");
            }
            var uri = Networking.GetFullUri(ServiceUris.NavigationGpsOutagesUri);
            return await Networking.HttpGetCall(Networking.AppendDateTimeAndPrnToUri(uri,fromTime,toTime,prn));
        }       

        /// <summary>
        /// Gets predicted Gps errors along a route
        /// </summary>
        /// <param name="predictionDataForRoute">Data for the prediction</param>
        /// <returns>RouteNavigationErrorResults</returns>         
        public static async Task<RouteNavigationErrorResults> GetPredictedNavigationErrorsOnARoute(
                                        NavigationPredictionData<IVerifiable> predictionDataForRoute)
        {            
            string relativeUri = string.Empty;
            
            predictionDataForRoute.Verify();

            if(predictionDataForRoute.Path.GetType() == typeof(PointToPointRouteData)){
                relativeUri = ServiceUris.NavigationPredictedPointToPointUri;
            }
            else if(predictionDataForRoute.Path.GetType() == typeof(SimpleFlightRouteData)){
                relativeUri = ServiceUris.NavigationPredictedSimpleFlightUri;
            }
            else if(predictionDataForRoute.Path.GetType() == typeof(TolRouteData)){
                relativeUri = ServiceUris.NavigationPredictedTolUri;
            }
            else if(predictionDataForRoute.Path.GetType() == typeof(RasterRouteData)){
                relativeUri = ServiceUris.NavigationPredictedRasterUri;
            }
            else if(predictionDataForRoute.Path.GetType() == typeof(GreatArcRouteData)){
                relativeUri = ServiceUris.NavigationPredictedGreatArcUri;
            }

            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("accessData",(Type)predictionDataForRoute.Path, 
                            (Type)predictionDataForRoute.Path  + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationPredictionData<IVerifiable>,
                                                 RouteNavigationErrorResults>(uri, predictionDataForRoute);
        } 

        public static async Task<RouteNavigationErrorResults> GetAssessedNavigationErrorsOnARoute(
                                        NavigationAssessmentData<IVerifiable> assessmentDataForRoute)
        {            
            string relativeUri = string.Empty;
            
            assessmentDataForRoute.Verify();
            
            if((Type)assessmentDataForRoute.Path == typeof(PointToPointRouteData)){
                relativeUri = ServiceUris.NavigationAssessedPointToPointUri;
            }
            else if((Type)assessmentDataForRoute.Path == typeof(SimpleFlightRouteData)){
                relativeUri = ServiceUris.NavigationAssessedSimpleFlightUri;
            }
            else if((Type)assessmentDataForRoute.Path == typeof(TolRouteData)){
                relativeUri = ServiceUris.NavigationAssessedTolUri;
            }
            else if((Type)assessmentDataForRoute.Path == typeof(RasterRouteData)){
                relativeUri = ServiceUris.NavigationAssessedRasterUri;
            }
            else if((Type)assessmentDataForRoute.Path == typeof(GreatArcRouteData)){
                relativeUri = ServiceUris.NavigationAssessedGreatArcUri;
            }

            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("accessData",(Type)assessmentDataForRoute.Path, 
                            (Type)assessmentDataForRoute.Path  + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationAssessmentData<IVerifiable>,
                                                 RouteNavigationErrorResults>(uri, assessmentDataForRoute);
        }  

        public static async Task<RouteDopResults> GetDopOnARoute(NavigationData<IVerifiable> dopDataForRoute)
        {            
            string relativeUri = string.Empty;
            
            dopDataForRoute.Verify();
            
            if((Type)dopDataForRoute.Path == typeof(PointToPointRouteData)){
                relativeUri = ServiceUris.NavigationDopPointToPointUri;
            }
            else if((Type)dopDataForRoute.Path == typeof(SimpleFlightRouteData)){
                relativeUri = ServiceUris.NavigationDopSimpleFlightUri;
            }
            else if((Type)dopDataForRoute.Path == typeof(TolRouteData)){
                relativeUri = ServiceUris.NavigationDopTolUri;
            }
            else if((Type)dopDataForRoute.Path == typeof(RasterRouteData)){
                relativeUri = ServiceUris.NavigationDopRasterUri;
            }
            else if((Type)dopDataForRoute.Path == typeof(GreatArcRouteData)){
                relativeUri = ServiceUris.NavigationDopGreatArcUri;
            }

            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("accessData",(Type)dopDataForRoute.Path, 
                            (Type)dopDataForRoute.Path  + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationData<IVerifiable>, RouteDopResults>(uri, dopDataForRoute);
        }  

        public static async Task<SiteNavigationErrorResults> GetPredictedNavigationErrorsAtASite(
                                        NavigationPredictionData<IVerifiable> predictionDataForSite)
        {            
            string relativeUri = string.Empty;
            
            predictionDataForSite.Verify();
            
            if((Type)predictionDataForSite.Path == typeof(SiteData)){
                relativeUri = ServiceUris.NavigationPredictedSiteUri;
            }
            
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("predictionDataForSite",(Type)predictionDataForSite.Path, 
                            (Type)predictionDataForSite.Path  + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationPredictionData<IVerifiable>,
                                                 SiteNavigationErrorResults>(uri, predictionDataForSite);
        } 

        public static async Task<SiteNavigationErrorResults> GetAssessedNavigationErrorsAtASite(
                                        NavigationPredictionData<IVerifiable> assessedDataForSite)
        {            
            string relativeUri = string.Empty;
            
            assessedDataForSite.Verify();
            
            if((Type)assessedDataForSite.Path == typeof(SiteData)){
                relativeUri = ServiceUris.NavigationAssessedSiteUri;
            }
            
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("assessedDataForSite",(Type)assessedDataForSite.Path, 
                            (Type)assessedDataForSite.Path  + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationPredictionData<IVerifiable>,
                                                 SiteNavigationErrorResults>(uri, assessedDataForSite);
        } 

        public static async Task<SiteDopResults> GetDopAtASite(
                                        NavigationData<IVerifiable> dopDataForSite)
        {            
            string relativeUri = string.Empty;
            
            dopDataForSite.Verify();
            
            if((Type)dopDataForSite.Path == typeof(SiteData)){
                relativeUri = ServiceUris.NavigationDopSiteUri;
            }
            
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("dopDataForSite",(Type)dopDataForSite.Path, 
                            (Type)dopDataForSite.Path  + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationData<IVerifiable>,
                                                 SiteDopResults>(uri, dopDataForSite);
        } 
    }
}