using System.Collections.Generic;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    public class SiteNavigationErrorResults : NavigationExtremes
    {
        public SiteNavigationErrorResults()
        {

        }
        public SiteNavigationErrorResults(AccuracyData libraryAccuracyResults,
                                         Dictionary<NavigationAccuracyType, ExtremesInfo> extremes,
                                         BasicExtremesInfo<int> numberOfSatelliteExtremes)
            :base(extremes)
        {
            Location = libraryAccuracyResults.Positions[0];

            List<NavigationError> navPredictions = new List<NavigationError>();
            NumberOfSatellitesExtremes = numberOfSatelliteExtremes;

            for (int i = 0; i < libraryAccuracyResults.Dates.Count; i++)
            {
                NavigationError ne = new NavigationError();
                ne.Time = libraryAccuracyResults.Dates[i];
                ne.PositionError = libraryAccuracyResults.Series[NavigationAccuracyType.PositionTotal][i];
                ne.HorizontalError = libraryAccuracyResults.Series[NavigationAccuracyType.XYTotal][i];
                ne.VerticalError = libraryAccuracyResults.Series[NavigationAccuracyType.ZTotal][i];
                ne.TimeError = libraryAccuracyResults.Series[NavigationAccuracyType.TimeTotal][i];
                ne.XError = libraryAccuracyResults.Series[NavigationAccuracyType.XTotal][i];
                ne.YError = libraryAccuracyResults.Series[NavigationAccuracyType.YTotal][i];
                ne.NumberOfSatellites = libraryAccuracyResults.NumberOfSatellites[i];
                navPredictions.Add(ne);
            }
            Errors = navPredictions.ToArray();
        }
       
        public NavigationError[] Errors { get; set; }
        public BasicExtremesInfo<int> NumberOfSatellitesExtremes { get; set; }
        public ServiceCartographic Location { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}