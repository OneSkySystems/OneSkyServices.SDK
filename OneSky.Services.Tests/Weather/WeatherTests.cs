using System;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Inputs.Weather;
using OneSky.Services.Services.Weather;

namespace OneSky.Services.Tests.Weather
{
    [TestFixture]
    public class WeatherTests
    {
        [Test]
        public void TestWeatherAlongARouteWithAviationDotGov()
        {
            var request = new WeatherData<PointToPointRouteData>();
            var route = new PointToPointRouteData(2);
            
            route.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 2000,
                Latitude = 39.07096,
                Longitude = -104.78509
            };
            route.Waypoints[0].Time = DateTimeOffset.Now.AddDays(-2).ToString();
            route.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 1800,
                Latitude = 39.743635,
                Longitude = -104.607925
            };
            route.Waypoints[1].Time = DateTimeOffset.Now.AddDays(-2).AddHours(2.5).ToString();
            route.OutputSettings.Step = 900;       
            
            request.Path = route;
            request.Provider = WeatherProviderType.AviationDotGov;
            
            var weather = WeatherServices.GetWeatherAlongARoute(request).Result;
            Assert.That(weather != null);
            Assert.That(weather.Count == 6);
        }

        [Test]
        [DebugExplicit] // AS-149 will fix a services error when this data is unavailable
        public void TestWeatherAlongARouteWithSingapore()
        {
            var request = new WeatherData<PointToPointRouteData>();
            var route = new PointToPointRouteData(2);
            
            route.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 100,
                Latitude = 1.353811,
                Longitude = 103.659532
            };
            
            route.Waypoints[0].Time = DateTimeOffset.Now.AddDays(-2).ToString();
            route.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100,
                Latitude = 1.291218,
                Longitude = 103.887249
            };
            route.Waypoints[1].Time = DateTimeOffset.Now.AddDays(-2).AddHours(2.5).ToString();
            route.OutputSettings.Step = 900;       
            
            request.Path = route;
            request.Provider = WeatherProviderType.Singapore;
            
            var weather = WeatherServices.GetWeatherAlongARoute(request).Result;
            Assert.That(weather != null);
            Assert.That(weather.Count == 6);
        }
    }
}