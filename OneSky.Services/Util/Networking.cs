using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OneSky.Services.Exceptions;

namespace OneSky.Services.Util
{
    /// <summary>
    /// Utilities to help create the service call.
    /// </summary>
    public static class Networking
    {
        private static HttpClient _client;
        public static string ApiKey { get; set; }
        public static Uri BaseUri { get; set; }

        static Networking()
        {
            Init();
        }

        /// <summary>
        /// Posts data of type T to a web service defined by address.  The web service returns data defined by R.
        /// </summary>
        /// <typeparam name="T">Type of data to post to the web service</typeparam>
        /// <typeparam name="R">Type of data returned by the web service</typeparam>
        /// <param name="address">The Uri of the web service, including all query parameters</param>
        /// <param name="postData">The post data to provide to the web service.</param>
        /// <exception cref="WebException">Thrown if response code is anything other than OK.</exception>
        /// <returns>Result from the Web service, as R.</returns>
        public static async Task<R> HttpPostCall<T, R>(Uri address, T postData){

            var postDataS = JsonConvert.SerializeObject(postData, new Newtonsoft.Json.Converters.StringEnumConverter());
            HttpContent postContent = new StringContent(postDataS,Encoding.UTF8,"application/json");

            var response = await _client.PostAsync(address, postContent);
            if (!response.IsSuccessStatusCode)
            {
                GetErrorMessageandThrow(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            R result;

            if (typeof(R) != typeof(string))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<R>(jsonResponse);
                }
                catch
                {
                    throw new ArgumentOutOfRangeException($"Unable to convert web response to type: {typeof(R)}");
                }
            }
            else
            {
                result = (R)Convert.ChangeType(jsonResponse, typeof(R));
            }

            return result;
        }

        public static async Task<R> HttpGetCall<R>(Uri address){

            var response = await _client.GetAsync(address);
            if (!response.IsSuccessStatusCode)
                GetErrorMessageandThrow(response.Content.ReadAsStringAsync().Result, response.StatusCode);


            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            R result;

            try{
                result = JsonConvert.DeserializeObject<R>(jsonResponse);
            }catch{
                throw new ArgumentOutOfRangeException($"Unable to convert web response to type: {typeof(R)}");
            }
            return result;
        }

        public static async Task<string> HttpGetCall(Uri address){
            
            var response = await _client.GetAsync(address);
            if (!response.IsSuccessStatusCode) 
                GetErrorMessageandThrow(response.Content.ReadAsStringAsync().Result, response.StatusCode);

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Initializes the apikey and base url from the config file.abstract  If there are issues with
        ///  the config file, a TypeInitializationException will be thrown, with the InnerException
        ///  describing the problem.
        /// </summary>
         public static void Init()
        {

            var efm = new ExeConfigurationFileMap { ExeConfigFilename = "OneSky.Services.config" };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(efm, ConfigurationUserLevel.None);
            var asc = (AppSettingsSection)configuration.GetSection("appSettings");
            if (asc.Settings.Count == 0)
            {
                throw new ConfigurationErrorsException("The configuration file is missing or empty.");
            }
            ApiKey = asc.Settings["ApiKey"]?.Value;
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new ConfigurationErrorsException("The ApiKey is not defined in the configuration file.");
            }
            var url = asc.Settings["BaseUrl"]?.Value;
            if (string.IsNullOrEmpty(url))
            {
                throw new ConfigurationErrorsException("The BaseUrl is not defined in the configuration file.");
            }
            BaseUri = new Uri(url);

            if (_client != null) return;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static Uri GetFullUri(string relativeUri) => new Uri(BaseUri, relativeUri + "?u=" + ApiKey);

        public static Uri AppendDateToUri(Uri existingUri, DateTime? date){
            if(date.HasValue){
                UriBuilder urib = new UriBuilder(existingUri);
                var dateQuery = $"&date={date.Value.Date:yyyy-MM-dd}";
                urib.Query = urib.Query.Substring(1) + dateQuery;
                return urib.Uri;
            }            
            throw new ArgumentNullException(nameof(date),"The date must be supplied");
        }
        public static Uri AppendDateTimeAndPrnToUri(Uri existingUri,
                                                    DateTime? fromDate,
                                                    DateTime? toDate,
                                                    int? prn){
            UriBuilder urib = new UriBuilder(existingUri);
            if(fromDate.HasValue && toDate.HasValue){
                var fromQuery = $"&from={fromDate.Value:yyyy-MM-ddTHH:mm:ss}";
                var toQuery = $"&to={toDate.Value:yyyy-MM-ddTHH:mm:ss}";
                urib.Query = urib.Query.Substring(1) + fromQuery + toQuery;
            }
            if(prn.HasValue){
                var prnQuery = $"&prn={prn.Value}";
                urib.Query = urib.Query.Substring(1) + prnQuery;
            }                            
            return urib.Uri;
        }

        internal static void GetErrorMessageandThrow(string errorResponse, HttpStatusCode status)
        {
            AnalyticalServicesException asEx = new AnalyticalServicesException(9999, $"Unknown service error response: {errorResponse}"); ;
            if (!string.IsNullOrEmpty(errorResponse))
            {
                try
                {
                    var errorResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorResponse);
                    asEx = new AnalyticalServicesException(
                        int.Parse(errorResult["ErrorId"]),
                        errorResult["Message"],
                        status)
                    {
                        HelpLink = errorResult["HelpUrl"]
                    };
                }
                catch
                {
                    throw asEx;
                }
            }
            throw asEx;
        }
    }
}
