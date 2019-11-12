using System.Collections.Generic;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Outputs.Access
{
    public class SensorOrientationData
    {
        public SensorOrientationData()
        {
            SensorOrientations = new List<ServiceUnitQuaternion>();
        }

        public SensorOrientationData(string name, List<ServiceUnitQuaternion> sensorOrientations)
        {
            SensorName = name;
            SensorOrientations = new List<ServiceUnitQuaternion>(sensorOrientations);
        }

        public List<ServiceUnitQuaternion> SensorOrientations { get; set; }
        public string SensorName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
