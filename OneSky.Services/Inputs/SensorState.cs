using Newtonsoft.Json;

namespace OneSky.Services.Inputs
{
    /// <summary>
    /// The pointing state of a sensor on a vehicle.
    /// </summary>
    public class SensorState
    {
        /// <summary>
        /// Creates a default SensorState. 
        /// </summary>
        public SensorState()
        {
            Orientation = new ServiceUnitQuaternion();
            Name = string.Empty;
        }
        /// <summary>
        /// Creates a specific SensorState.
        /// </summary>
        /// <param name="name">The name of the sensor.</param>
        /// <param name="orientation">The orientation of the sensor.</param>
        public SensorState(string name, ServiceUnitQuaternion orientation)
        {
            Name = name;
            Orientation = orientation;
        }
        /// <summary>
        /// The quaternion orientation of the sensor.
        /// </summary>
        public ServiceUnitQuaternion Orientation { get; set; }
        /// <summary>
        /// The name of the sensor. Default is an empty string.
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
