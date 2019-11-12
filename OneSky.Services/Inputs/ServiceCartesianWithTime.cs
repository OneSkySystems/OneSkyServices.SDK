using System;
using System.Collections.Generic;
using OneSky.Services.Inputs;
using Newtonsoft.Json;

namespace OneSky.Services.Inputs
{
    public class ServiceCartesianWithTime : IPathResult
    {
        public ServiceCartesian Position { get; set; }
        public List<SensorState> SensorStates { get; set; }
        public DateTimeOffset Time { get; set; }

        public ServiceCartesianWithTime()
        {
            Position = new ServiceCartesian();
            SensorStates = new List<SensorState>();
        }

        public ServiceCartesianWithTime(ServiceCartesian position, DateTimeOffset time)
        {
            Position = position;
            SensorStates = new List<SensorState>();
            Time = time;
        }

        public ServiceCartesianWithTime(ServiceCartesian position, List<SensorState> sensors, DateTimeOffset time)
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

        public ServiceCartesianWithTime(double posX, double posY, double posZ, DateTimeOffset time)
        {
            Position = new ServiceCartesian(posX, posY, posZ);
            SensorStates = new List<SensorState>();
            Time = time;
        }

        public ServiceCartesianWithTime(double posX, double posY, double posZ, List<SensorState> sensors,
        DateTimeOffset time)
        {
            Position = new ServiceCartesian(posX, posY, posZ);
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

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}