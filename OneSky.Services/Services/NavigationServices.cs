using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Navigation;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Navigation;
using OneSky.Services.Util;

namespace OneSky.Services.Services
{
    /// <summary>
    /// Navigation methods.  See the service documentation for 
    /// notes on the different navigation services: https://saas.onesky.xyz/V1/Documentation/Navigation.
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
        public static async Task<string> GetSofData(){
            var uri = Networking.GetFullUri(ServiceUris.NavigationSofDataUri);
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
        public static async Task<IEnumerable<ServiceSatelliteOutage>> GetGpsOutages(DateTime? fromTime = null, DateTime? toTime = null, int? prn = null){
            if(fromTime == null && toTime == null && prn == null){
                throw new ArgumentNullException("fromTime",
                                                "You must specify either a from and to date or a prn number.");
            }
            var uri = Networking.GetFullUri(ServiceUris.NavigationGpsOutagesUri);
            //AS-146 will address why this conversion doesn't work
            return await Networking.HttpGetCall<List<ServiceSatelliteOutage>>(Networking.AppendDateTimeAndPrnToUri(uri,fromTime,toTime,prn));
        }
    /// <summary>
    /// Returns Gps Outages times for a time interval or a prn, or a prn within a time interval.
    /// </summary>
    /// <param name="fromTime">The start time to look for outages.</param>
    /// <param name="toTime">The end time to look for outages.</param>
    /// <param name="prn">The prn whose outages are desired.</param>
    /// <remarks>Note that either a from/to time or a prn (or both) must be supplied. Also, this is a temporary method that should be deprecated when AS-146 is fixed.</remarks>
    /// <returns>A string containing Gps outages.</returns>
    public static async Task<string> GetGpsOutagesString(DateTime? fromTime = null, DateTime? toTime = null, int? prn = null)
    {
        if (fromTime == null && toTime == null && prn == null)
        {
            throw new ArgumentNullException("fromTime",
                "You must specify either a from and to date or a prn number.");
        }
        var uri = Networking.GetFullUri(ServiceUris.NavigationGpsOutagesUri);
        return await Networking.HttpGetCall(Networking.AppendDateTimeAndPrnToUri(uri, fromTime, toTime, prn));
    }

        /// <summary>
        /// Gets predicted Gps errors along a route
        /// </summary>
        /// <param name="predictionDataForRoute">Data for the prediction</param>
        /// <returns>RouteNavigationErrorResults</returns>         
        public static async Task<RouteNavigationErrorResults> GetPredictedNavigationErrorsOnARoute(
                                        NavigationPredictionData<IVerifiable> predictionDataForRoute)
        {            
            var relativeUri = string.Empty;
            
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
            var relativeUri = string.Empty;
            
            assessmentDataForRoute.Verify();
            
            if(assessmentDataForRoute.Path.GetType() == typeof(PointToPointRouteData)){
                relativeUri = ServiceUris.NavigationAssessedPointToPointUri;
            }
            else if(assessmentDataForRoute.Path.GetType() == typeof(SimpleFlightRouteData)){
                relativeUri = ServiceUris.NavigationAssessedSimpleFlightUri;
            }
            else if(assessmentDataForRoute.Path.GetType() == typeof(TolRouteData)){
                relativeUri = ServiceUris.NavigationAssessedTolUri;
            }
            else if(assessmentDataForRoute.Path.GetType() == typeof(RasterRouteData)){
                relativeUri = ServiceUris.NavigationAssessedRasterUri;
            }
            else if(assessmentDataForRoute.Path.GetType() == typeof(GreatArcRouteData)){
                relativeUri = ServiceUris.NavigationAssessedGreatArcUri;
            }

            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("accessData",assessmentDataForRoute.Path.GetType(), 
                            assessmentDataForRoute.Path.GetType() + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationAssessmentData<IVerifiable>,
                                                 RouteNavigationErrorResults>(uri, assessmentDataForRoute);
        }  

        public static async Task<RouteDopResults> GetDopOnARoute(NavigationData<IVerifiable> dopDataForRoute)
        {            
            var relativeUri = string.Empty;
            
            dopDataForRoute.Verify();
            
            if(dopDataForRoute.Path.GetType() == typeof(PointToPointRouteData)){
                relativeUri = ServiceUris.NavigationDopPointToPointUri;
            }
            else if(dopDataForRoute.Path.GetType() == typeof(SimpleFlightRouteData)){
                relativeUri = ServiceUris.NavigationDopSimpleFlightUri;
            }
            else if(dopDataForRoute.Path.GetType() == typeof(TolRouteData)){
                relativeUri = ServiceUris.NavigationDopTolUri;
            }
            else if(dopDataForRoute.Path.GetType() == typeof(RasterRouteData)){
                relativeUri = ServiceUris.NavigationDopRasterUri;
            }
            else if(dopDataForRoute.Path.GetType() == typeof(GreatArcRouteData)){
                relativeUri = ServiceUris.NavigationDopGreatArcUri;
            }

            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("accessData",dopDataForRoute.Path.GetType(), 
                            dopDataForRoute.Path.GetType() + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationData<IVerifiable>, RouteDopResults>(uri, dopDataForRoute);
        }  

        public static async Task<SiteNavigationErrorResults> GetPredictedNavigationErrorsAtASite(
                                        NavigationPredictionData<IVerifiable> predictionDataForSite)
        {            
            var relativeUri = string.Empty;
            
            predictionDataForSite.Verify();
            
            if(predictionDataForSite.Path.GetType() == typeof(SiteData)){
                relativeUri = ServiceUris.NavigationPredictedSiteUri;
            }
            
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("predictionDataForSite",predictionDataForSite.Path.GetType(), 
                            predictionDataForSite.Path.GetType() + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationPredictionData<IVerifiable>,
                                                 SiteNavigationErrorResults>(uri, predictionDataForSite);
        } 

        public static async Task<SiteNavigationErrorResults> GetAssessedNavigationErrorsAtASite(
                                        NavigationAssessmentData<IVerifiable> assessedDataForSite)
        {            
            var relativeUri = string.Empty;
            
            assessedDataForSite.Verify();
            
            if(assessedDataForSite.Path.GetType() == typeof(SiteData)){
                relativeUri = ServiceUris.NavigationAssessedSiteUri;
            }
            
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("assessedDataForSite",assessedDataForSite.Path.GetType(), 
                            assessedDataForSite.Path.GetType() + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationAssessmentData<IVerifiable>,
                                                 SiteNavigationErrorResults>(uri, assessedDataForSite);
        } 

        public static async Task<SiteDopResults> GetDopAtASite(
                                        NavigationData<IVerifiable> dopDataForSite)
        {            
            var relativeUri = string.Empty;
            
            dopDataForSite.Verify();
            
            if(dopDataForSite.Path.GetType() == typeof(SiteData)){
                relativeUri = ServiceUris.NavigationDopSiteUri;
            }
            
            if(string.IsNullOrEmpty(relativeUri)){
                throw new ArgumentOutOfRangeException("dopDataForSite",dopDataForSite.Path.GetType(), 
                            dopDataForSite.Path.GetType() + " is not a valid type for Navigation paths");
            }
            
            var uri = Networking.GetFullUri(relativeUri);
            return await Networking.HttpPostCall<NavigationData<IVerifiable>,
                                                 SiteDopResults>(uri, dopDataForSite);
        } 
    }
}