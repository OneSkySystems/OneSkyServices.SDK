using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Navigation
{
    public class NavigationExtremes
    {
        public NavigationExtremes(Dictionary<NavigationAccuracyType, ExtremesInfo> extremes)
        {
            TotalExtremes = extremes[NavigationAccuracyType.PositionTotal];
            HorizontalExtremes = extremes[NavigationAccuracyType.XYTotal];
            VerticalExtremes = extremes[NavigationAccuracyType.ZTotal];
            TimeExtremes = extremes[NavigationAccuracyType.TimeTotal];
            XExtremes = extremes[NavigationAccuracyType.XTotal];
            YExtremes = extremes[NavigationAccuracyType.YTotal];
        }
        
        public NavigationExtremes(Dictionary<DilutionOfPrecisionType, ExtremesInfo> extremes)
        {
            TotalExtremes = extremes[DilutionOfPrecisionType.Position];
            HorizontalExtremes = extremes[DilutionOfPrecisionType.XY];
            VerticalExtremes = extremes[DilutionOfPrecisionType.Z];
            TimeExtremes = extremes[DilutionOfPrecisionType.Time];
            XExtremes = extremes[DilutionOfPrecisionType.X];
            YExtremes = extremes[DilutionOfPrecisionType.Y];
        }

        public NavigationExtremes()
        {
        }

        public ExtremesInfo VerticalExtremes { get; set; }
        public ExtremesInfo HorizontalExtremes { get; set; }
        public ExtremesInfo TimeExtremes { get; set; }
        public ExtremesInfo TotalExtremes { get; set; }
        public ExtremesInfo XExtremes { get; set; }
        public ExtremesInfo YExtremes { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
