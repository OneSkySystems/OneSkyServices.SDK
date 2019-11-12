using System;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Terrain
{
    public class Heights
    {
        public double TerrainHeightFromWgs84 { get; set; }
        public double MeanSeaLevelHeightFromWgs84 { get; set; }

        public double TerrainHeightFromMeanSeaLevel
        {
            get { return TerrainHeightFromWgs84 - MeanSeaLevelHeightFromWgs84; }
        }

        public Heights(Tuple<double,double> heights)
        {
            TerrainHeightFromWgs84 = heights.Item1;
            MeanSeaLevelHeightFromWgs84 = heights.Item2;
        }

        public Heights()
        {
            
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
