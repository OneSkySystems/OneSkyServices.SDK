using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    public class RouteDopResults : NavigationExtremes
    {
        public RouteDopResults()
        {

        }
        public List<NavigationDopWithLocation> Dops { get; set; }
        public BasicExtremesInfo<int> NumberOfSatellitesExtremes { get; set; }
        public RouteDopResults(DopData libraryAccuracyResults, 
                                Dictionary<DilutionOfPrecisionType,
                                ExtremesInfo> extremes,
                                BasicExtremesInfo<int> numberOfSatellitesExtremes )
            : base(extremes)
        {
            Dops = new List<NavigationDopWithLocation>();
            NumberOfSatellitesExtremes = numberOfSatellitesExtremes;

            for (int i = 0; i < libraryAccuracyResults.Dates.Count; i++)
            {
                NavigationDopWithLocation ndl = new NavigationDopWithLocation();
                ndl.Hdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.XY][i];
                ndl.Pdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.Position][i];
                ndl.Vdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.Z][i];
                ndl.Tdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.Time][i];
                ndl.Xdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.X][i];
                ndl.Ydop = libraryAccuracyResults.Series[DilutionOfPrecisionType.Y][i];
                ndl.Location = libraryAccuracyResults.Positions[i];
                ndl.Time = libraryAccuracyResults.Dates[i];
                ndl.NumberOfSatellites = libraryAccuracyResults.NumberOfSatellites[i];
                Dops.Add(ndl);
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}