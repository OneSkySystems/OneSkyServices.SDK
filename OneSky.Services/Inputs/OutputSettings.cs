using OneSky.Services.Exceptions;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs
{
    public class OutputSettings : IVerifiable
    {
        /// <summary>
        /// Step size of the propagated output data, in seconds. Defaults to 60.
        /// </summary>
        public int Step { get; set; }
        /// <summary>
        /// The Time Format for the output.  Defaults to UTC.
        /// </summary>
        public TimeRepresentation TimeFormat { get; set; }
        /// <summary>
        /// The format of the coordinates in the output.
        /// </summary>
        public CoordinateType CoordinateFormat { get; set; }

        public OutputSettings()
        {
            Step = 60;
            TimeFormat = TimeRepresentation.Epoch;
            CoordinateFormat = new CoordinateType();
        }
        /// <summary>
        /// Verifies the OutputSettings.
        /// </summary>
        /// <exception cref="AnalyticalServicesException">Thrown when <see cref="CoordinateFormat.Frame"/> is 
        /// <see cref="FrameRepresentation.Inertial"/> and <see cref="CoordinateFormat.Coord"/> is
        /// <see cref="CoordinateRepresentation.LLA"/>.  LLA Coordinates cannot be represented 
        /// in the inertial reference frame. This exception is also thrown if <see cref="Step"/>
        /// is less than or equal to zero.</exception>
        public void Verify()
        {
            if (Step <= 0)
            {
                throw new AnalyticalServicesException(23600, "Step must be greater than 0.");
            }

            if (CoordinateFormat.Frame == FrameRepresentation.Inertial &&
                CoordinateFormat.Coord == CoordinateRepresentation.LLA)
            {
                throw new AnalyticalServicesException(25200, 
                    "Cartographic coordinates cannot be returned in an inertial frame.");
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

    public class CoordinateType
    {
        /// <summary>
        /// The Coordinate type for the output. Defaults to <see cref="CoordinateRepresentation.LLA"/>
        /// </summary>
        public CoordinateRepresentation Coord { get; set; }
        /// <summary>
        /// The reference frame for the <see cref="CoordinateRepresentation.XYZ"/> coordinates.  
        ///Defaults to <see cref="CoordinateRepresentation.LLA"/>.
        /// </summary>
        public FrameRepresentation Frame { get; set; }

        public CoordinateType()
        {
            Coord = CoordinateRepresentation.LLA;
            Frame = FrameRepresentation.Fixed;
        }
    }
    /// <summary>
    /// Coordinate representation in the output.  LLA is Latitude, Longitude and altitude.  XYZ is Cartesian.
    /// </summary>
    public enum CoordinateRepresentation
    {
        LLA,
        XYZ
    }
    /// <summary>
    /// The reference frame for the <see cref="CoordinateRepresentation.XYZ"/> coordinates.
    /// </summary>
    public enum FrameRepresentation
    {
        Fixed,
        Inertial
    }
    /// <summary>
    /// The format of the time in the output.
    /// Epoch times start at 0, and increment by the Step value for each returned point.
    /// UTC represents each propagated point's time in Universal Time Coordinated, in ISO 8601 format.
    /// </summary>
    public enum TimeRepresentation
    {
        Epoch,
        UTC
    }
}
