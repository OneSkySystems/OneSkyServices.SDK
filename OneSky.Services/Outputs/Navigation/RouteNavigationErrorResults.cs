using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    public class RouteNavigationErrorResults : NavigationExtremes
    {
        public RouteNavigationErrorResults()
        {

        }
        public NavigationErrorWithLocation[] Errors { get; set; }
        public BasicExtremesInfo<int> NumberOfSatellitesExtremes { get; set; }

        public RouteNavigationErrorResults(AccuracyData libraryAccuracyResults, 
                                            Dictionary<NavigationAccuracyType, ExtremesInfo> extremes,
                                            BasicExtremesInfo<int> numberOfSatellitesExtremes)
            : base(extremes)
        {
            List<NavigationErrorWithLocation> navPredictions = new List<NavigationErrorWithLocation>();
            NumberOfSatellitesExtremes = numberOfSatellitesExtremes;

            for (int i = 0; i < libraryAccuracyResults.Dates.Count; i++)
            {
                NavigationErrorWithLocation nel = new NavigationErrorWithLocation();
                nel.HorizontalError = libraryAccuracyResults.Series[NavigationAccuracyType.XYTotal][i];
                nel.PositionError = libraryAccuracyResults.Series[NavigationAccuracyType.PositionTotal][i];
                nel.VerticalError = libraryAccuracyResults.Series[NavigationAccuracyType.ZTotal][i];
                nel.TimeError = libraryAccuracyResults.Series[NavigationAccuracyType.TimeTotal][i];
                nel.XError = libraryAccuracyResults.Series[NavigationAccuracyType.XTotal][i];
                nel.YError = libraryAccuracyResults.Series[NavigationAccuracyType.YTotal][i];
                nel.Location = libraryAccuracyResults.Positions[i];
                nel.Time = libraryAccuracyResults.Dates[i];
                nel.NumberOfSatellites = libraryAccuracyResults.NumberOfSatellites[i];
                navPredictions.Add(nel);
            }
            Errors = navPredictions.ToArray();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}