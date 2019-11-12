namespace OneSky.Services.Inputs
{
    /// <summary>
    /// Specifies the type of coordinates to return from the service
    /// </summary>
    public class CoordinateFormat
    {
        /// <summary>
        /// Can be either "LLA" or XYZ".  LLA is the Latitude, Longitude and Altitude values and is only valid 
        /// in the "Fixed" Frame. XYZ are Cartesian values and can be represented in the Earth Fixed Frame
        /// or Earth Inertial Frame. Defaults to LLA.
        /// </summary>
        public string Coord { get; set; }
        /// <summary>
        /// Specifies the reference frame for the coordinates that are returned in XYZ format.
        ///  Values are either "Inertial" or "Fixed"  Defaults to "Fixed".
        /// </summary>
        public string Frame { get; set; }

        /// <summary>
        /// Sets the Coord value to "LLA" and Frame to "Fixed".
        /// </summary>
        public CoordinateFormat()
        {
            Coord = "LLA";
            Frame = "Fixed";
        }
    }
}
