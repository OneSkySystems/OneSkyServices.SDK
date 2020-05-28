
using System;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs.Routing;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Navigation
{
    /// <summary>
    /// Data required to perform a navigation assessment calculation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NavigationAssessmentData<T> : NavigationData<T>
        where T: IVerifiable
       
    {
        /// <summary>
        /// Path (file path, Http, Ftp, etc.) for the Performance Assessment File (PAF) if a specific PAF is to be used. If this property is set no other PAF will be used.
        /// </summary>
        public string PafLocation { get; set; }
        /// <summary>
        /// Allows for analyses to be performed when the evaluation times go beyond the Paf Data availability times
        /// </summary>
        public bool ExtrapolatePafData { get; set; }


        public NavigationAssessmentData() :base()
	    {
            PafLocation = string.Empty;
	        ExtrapolatePafData = false;
	    }

        public override void Verify()
        {
            base.Verify();

            if (!(Path is ISiteInput)) return;
            // This duration check only works for sites - length of a generic path is not available.
            // The service will through this same exception if the path is too long
            var dur = AnalysisStop - AnalysisStart;
            if (!ExtrapolatePafData && dur.TotalHours >= 23.75) // one day's worth of paf data
            {
                throw new AnalyticalServicesException(21250, "Analysis time is greater than available Paf data.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
