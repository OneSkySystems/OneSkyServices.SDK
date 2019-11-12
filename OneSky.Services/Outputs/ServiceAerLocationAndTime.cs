using Newtonsoft.Json;

namespace OneSky.Services.Outputs
{
    public class ServiceAerLocationAndTime
    {
        public ServiceAerLocationAndTime()
        {

        }
        public ServiceAerLocationAndTime(double azimuth, double elevation, double range, string time)
        {
            Azimuth = azimuth;
            Elevation = elevation;
            Range = range;
            Time = time;
        }
        public double Azimuth { get; set; }
        public double Elevation { get; set; }
        public double Range { get; set; }
        public string Time { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
