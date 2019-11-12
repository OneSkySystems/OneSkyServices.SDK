using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs
{
    public class ServiceCartographicWithTime : IVerifiable, IPathResult
    {
        public ServiceCartographic Position { get; set; }
        public List<SensorState> SensorStates { get; set; }
        public DateTimeOffset Time { get; set; }

        public ServiceCartographicWithTime(double latitude, double longitude, double altitude, DateTimeOffset time)
        {
            Position = new ServiceCartographic(latitude, longitude, altitude);
            SensorStates = new List<SensorState>();
            Time = time;
        }

        public ServiceCartographicWithTime(double latitude, double longitude, double altitude,
                                           List<SensorState> sensors, DateTimeOffset time)
        {
            Position = new ServiceCartographic(latitude, longitude, altitude);
            if (sensors == null)
            {
                SensorStates = new List<SensorState>();
            }
            else
            {
                SensorStates = sensors;
            }
            Time = time;
        }

        public ServiceCartographicWithTime(ServiceCartographic position, DateTimeOffset time)
        {
            Position = position;
            SensorStates = new List<SensorState>();
            Time = time;
        }

        public ServiceCartographicWithTime(ServiceCartographic position, List<SensorState> sensors, DateTimeOffset time)
        {
            Position = position;
            if (sensors == null)
            {
                SensorStates = new List<SensorState>();
            }
            else
            {
                SensorStates = sensors;
            }
            Time = time;
        }

        public void Verify() => Position.Verify();

        public ServiceCartographicWithTime()
        {
            Position = new ServiceCartographic();
            SensorStates = new List<SensorState>();
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
