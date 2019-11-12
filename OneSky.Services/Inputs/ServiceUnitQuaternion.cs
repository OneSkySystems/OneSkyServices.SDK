using Newtonsoft.Json;

namespace OneSky.Services.Inputs
{
    /// <summary>
    /// A unitQuaternion describing a rotation
    /// </summary>
    public class ServiceUnitQuaternion
    {
        /// <summary>
        /// Default UnitQuaternion. W = 1.0, X, Y and Z = 0.0;
        /// </summary>
        public ServiceUnitQuaternion()
        {
            // identity UnitQuaternion
            W = 1.0;
            X = Y = Z = 0.0;
        }
        /// <summary>
        /// Creates a specific UnitQuaternion.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public ServiceUnitQuaternion(double w, double x, double y, double z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        /// The W parameter for the UnitQuaternion
        /// </summary>
        public double W { get; set; }
        /// <summary>
        /// The X parameter for the UnitQuaternion
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The Y parameter for the UnitQuaternion
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// The Z parameter for the UnitQuaternion
        /// </summary>
        public double Z { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
