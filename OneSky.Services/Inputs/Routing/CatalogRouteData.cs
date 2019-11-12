using System;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Routing
{
    /// <summary>
    /// A route defined by the ephemeris associated with a STK Data Federate (SDF) catalog object. 
    /// The returned route depends on how the route is defined in the catalog object.
    /// </summary>
    public class CatalogRouteData : IVerifiable
    {
        /// <summary>
        /// The start date and time for the route.
        /// </summary>
        public DateTimeOffset Start {get; set;}
        /// <summary>
        /// The stop date and time for the route.
        /// </summary>
        public DateTimeOffset Stop { get; set; }
        /// <summary>
        /// A URI that points to the json object definition on the STK Data Federate (SDF). 
        /// See <see href="https://saas.agi.com/V1/Documentation/Routing#SDFURIs">Finding SDF Object URIs</see>
        /// to get the URI for the object you're interested in.
        /// </summary>
        public string URI { get; set; }
        /// <summary>
        /// The OutputSettings for the route.
        /// </summary>
        public OutputSettings OutputSettings { get; set; }

        public CatalogRouteData()
        {
            OutputSettings = new OutputSettings();
        }

        /// <summary>
        /// Verifies the <see cref="OutputSettings"/>, the start time is prior to the stop time and that the URI
        /// is not null or empty.
        /// </summary>
        public void Verify()
        {
            OutputSettings.Verify();

            if (Start > Stop)
            {
                throw new AnalyticalServicesException(21800, 
                "CatalogRoute Start time must be earlier that CatalogRoute Stop time.");
            }
               
            if (string.IsNullOrEmpty(URI))
            {
                throw new AnalyticalServicesException(24000, "CatalogRoute URI must be supplied to this service.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}