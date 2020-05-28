using System;
using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs.Navigation
{
    /// <summary>
    /// Data required to perform a navigation prediction calculation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NavigationPredictionData<T> : NavigationData<T>
        where T: IVerifiable
    {
        /// <summary>
        /// True if the results are to be scaled to PercentConfidence before returning.  If false, the results are a statistical RMS for the particular data type. Defaults to true.
        /// </summary>
        public bool ScaleToConfidence { get; set; }
        /// <summary>
        /// Statistical confidence level between 0 and 100 inclusive, which the navigation error results are scaled to, if ScaleToConfidence is true. Defaults to 95.
        /// </summary>
        public int PercentConfidence { get; set; }
        /// <summary>
        /// Path (file path, Http, Ftp, etc.) for the Prediction Support File (PSF) if a specific PSF is to be used. If this property is set no other PSF will be used.
        /// </summary>
        public string PsfLocation { get; set; }

  
        public NavigationPredictionData ()
	    {
            ScaleToConfidence = true;
            PercentConfidence = 95;
            UseBestAvailableData = true;
            PsfLocation = string.Empty;
	    }

        /// <summary>
        /// Verifies that the properties have valid values
        /// </summary>
        public override void Verify()
        {
            base.Verify();

            if (PercentConfidence < 0 || PercentConfidence > 100)
            {
                throw new AnalyticalServicesException(23600, "PercentConfidence must be between 0 and 100 inclusive.");
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
