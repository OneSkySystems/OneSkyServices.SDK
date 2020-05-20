using System;
using System.Net;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services.Terrain;

namespace OneSky.Services.Tests.Terrain
{
    [TestFixture]
    public class TerrainValidation
    {
        [Test]
        public void TestBadLatitude()
        {
            void ErrorFunction()
            {
                var s = TerrainServices.GetTerrainHeightsAtASite(91, -105.043498).Result;
            }
            var ex = Assert.Throws<AggregateException>(ErrorFunction);
            Assert.That(ex.Message.Contains("BadRequest"));
            void ErrorFunction2()
            {
                var s = TerrainServices.GetTerrainHeightsAtASite(-91, -105.043498).Result;
            }
            ex = Assert.Throws<AggregateException>(ErrorFunction2);
            Assert.That(ex.Message.Contains("BadRequest"));
        }

        [Test]
        public void TestBadLongitude()
        {
            void ErrorFunction()
            {
                var s = TerrainServices.GetTerrainHeightsAtASite(80, -361).Result;
            }
            var ex = Assert.Throws<AggregateException>(ErrorFunction);
            Assert.That(ex.Message.Contains("BadRequest"));

            void ErrorFunction2()
            {
                var s = TerrainServices.GetTerrainHeightsAtASite(80, 361).Result;
            }
            ex = Assert.Throws<AggregateException>(ErrorFunction2);
            Assert.That(ex.Message.Contains("BadRequest"));
        }

        [Test]
        public void TestTooManyPoints()
        {
            var request = new PointToPointRouteData(2);

            request.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 20000,
                Latitude = 39.07096,
                Longitude = -104.78509
            };
            request.Waypoints[0].Time = new DateTime(2014, 02, 10, 10, 30, 0).ToString();

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100,
                Latitude = 42.64541,
                Longitude = -61.11172
            };
            request.Waypoints[1].Time = new DateTime(2014, 02, 10, 18, 30, 20).ToString();
            request.OutputSettings.Step = 60; // too many results for a terrain calculation

            void ErrorFunction()
            {
                var result = TerrainServices.GetTerrainHeightsAlongARoute<PointToPointRouteData>(request).Result;
            }
            var ex = Assert.Throws<AggregateException>(ErrorFunction);
            Assert.That(ex.Message.Contains("BadRequest"));
        }
    }
}




