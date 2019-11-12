using System.Collections.Generic;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace OneSky.Services.Outputs.Navigation
{
    public class SiteDopResults : NavigationExtremes
    {
        public SiteDopResults()
        {

        }
        public SiteDopResults(DopData libraryAccuracyResults,
                              Dictionary<DilutionOfPrecisionType, ExtremesInfo> extremes,
                              BasicExtremesInfo<int> numberOfSatelliteExtremes )
            :base(extremes)
        {
            Location = libraryAccuracyResults.Positions[0];
            NumberOfSatellitesExtremes = numberOfSatelliteExtremes;

            List<NavigationDop> navDops = new List<NavigationDop>();
            for (int i = 0; i < libraryAccuracyResults.Dates.Count; i++)
            {
                NavigationDop nd = new NavigationDop();
                nd.Time = libraryAccuracyResults.Dates[i];
                nd.Pdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.Position][i];
                nd.Hdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.XY][i];
                nd.Vdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.Z][i];
                nd.Tdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.Time][i];
                nd.Xdop = libraryAccuracyResults.Series[DilutionOfPrecisionType.X][i];
                nd.Ydop = libraryAccuracyResults.Series[DilutionOfPrecisionType.Y][i];
                nd.NumberOfSatellites = libraryAccuracyResults.NumberOfSatellites[i];
                navDops.Add(nd);
            }
            Dops = navDops.ToArray();
        }

        public NavigationDop[] Dops { get; set; }
        public BasicExtremesInfo<int> NumberOfSatellitesExtremes { get; set; }
        public ServiceCartographic Location { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}