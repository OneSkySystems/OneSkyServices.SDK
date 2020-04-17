using System;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Inputs.Weather;
using OneSky.Services.Services.Weather;

namespace OneSky.Services.Tests.Basic.Weather
{
    [TestFixture]
    public class WeatherTests
    {
        [Test]
        public void TestWeatherAtASite()
        {
            var request = new WeatherData<SiteData>
            {
                Path = new SiteData {Location = {Latitude = 39.0, Longitude = -104.77, Altitude = 1910}},
                AnalysisStart = DateTime.Now.AddDays(-2),
                AnalysisStop = DateTime.Now.AddDays(-2).AddHours(2.5)
            };

            var weather = WeatherServices.GetWeatherAtASite(request).Result;
            Assert.That(weather != null);
            Assert.That(weather.Count == 6);           
        }

        [Test]
        public void TestWeatherAlongARoute()
        {
            var request = new WeatherData<PointToPointRouteData>();
            var route = new PointToPointRouteData(2);
            
            route.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 2000,
                Latitude = 39.07096,
                Longitude = -104.78509
            };
            route.Waypoints[0].Time = DateTimeOffset.Now.AddDays(-2);
            route.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 1800,
                Latitude = 39.743635,
                Longitude = -104.607925
            };
            route.Waypoints[1].Time = DateTimeOffset.Now.AddDays(-2).AddHours(2.5);
            route.OutputSettings.Step = 900;
            route.IncludeWaypointsInRoute = false;
            
            request.Path = route;
            
            var weather = WeatherServices.GetWeatherAlongARoute(request).Result;
            Assert.That(weather != null);
            // should be 6 Wx reports,  one for each half hour along the route.
            Assert.That(weather.Count == 6);
        }

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
            route.Waypoints[0].Time = DateTimeOffset.Now.AddDays(-2);
            route.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 1800,
                Latitude = 39.743635,
                Longitude = -104.607925
            };
            route.Waypoints[1].Time = DateTimeOffset.Now.AddDays(-2).AddHours(2.5);
            route.OutputSettings.Step = 900;       
            
            request.Path = route;
            request.Provider = WeatherProviderType.AviationDotGov;
            
            var weather = WeatherServices.GetWeatherAlongARoute(request).Result;
            Assert.That(weather != null);
            Assert.That(weather.Count == 7);
        }

        [Test]
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
            
            route.Waypoints[0].Time = DateTimeOffset.Now.AddDays(-2);
            route.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100,
                Latitude = 1.291218,
                Longitude = 103.887249
            };
            route.Waypoints[1].Time = DateTimeOffset.Now.AddDays(-2).AddHours(2.5);
            route.OutputSettings.Step = 900;       
            
            request.Path = route;
            request.Provider = WeatherProviderType.AviationDotGov;
            
            var weather = WeatherServices.GetWeatherAlongARoute(request).Result;
            Assert.That(weather != null);
            Assert.That(weather.Count == 7);
        }
    }
}