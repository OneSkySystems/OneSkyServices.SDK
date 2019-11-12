using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Access
{
    public class SatellitePassResults<T>
        where T : IPathResult
    {
        public List<PassResult<T>> Passes { get; set; }
        public string CzmlForPasses { get; set; }

        public SatellitePassResults()
        {
            Passes = new List<PassResult<T>>();
        }
    }

    /// <summary>
    /// Returns all AccessResult data, and min and max range and elevation data, and brightness magnitude.
    /// </summary>
    public class PassResult<T> : AccessResult<T>
        where T : IPathResult
    {
        public PassResult()
        {
            MaximumElevationData = new ServiceAerLocationAndTime();
            AccessBeginData = new ServiceAerLocationAndTime();
            AccessEndData = new ServiceAerLocationAndTime();

        }
        public int SSC { get; set; }
        public ServiceAerLocationAndTime MaximumElevationData { get; set; }
        public ServiceAerLocationAndTime AccessBeginData { get; set; }
        public ServiceAerLocationAndTime AccessEndData { get; set; }
        public double MaxMagnitude { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
            //return string.Format("Max Elevation: {0:F1}, Max Magnitude: {1:F2}, Start Azimuth: {2:F1}, Stop Azimuth: {3:F1}", MaximumElevationData.Elevation, MaxMagnitude, AccessBeginData.Azimuth, AccessEndData.Azimuth);
        }
    }
}
