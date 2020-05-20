using System;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services.Terrain;

namespace OneSky.Services.Tests.Terrain
{
    [TestFixture]
    public class TerrainDocExamples
    {
        [Test]
        public void TestTerrainAtASite()
        {
            var result = TerrainServices.GetTerrainHeightsAtASite(38.840318, -105.043498).Result;

            Assert.That(result != null);
            Assert.AreEqual(4299.2474923799728, result.TerrainHeightFromMeanSeaLevel);
            Assert.AreEqual(-16.108077610647548, result.MeanSeaLevelHeightFromWgs84);
            Assert.AreEqual(4283.1394147693254, result.TerrainHeightFromWgs84);
        }

        [Test]
        public void TestTerrainAlongPointToPointRoute()
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
            request.OutputSettings.Step = 900;

            var result = TerrainServices.GetTerrainHeightsAlongARoute<PointToPointRouteData>(request).Result;

            Assert.That(result != null);
            Assert.That(result.Count == 34);
            Assert.AreEqual(2269.87682f, result[0].TerrainHeightFromWgs84);
            Assert.AreEqual(-16.9748859f, result[0].MeanSeaLevelHeightFromWgs84);
            Assert.AreEqual(2286.85170f, result[0].TerrainHeightFromMeanSeaLevel);
        }

        [Test]
        public void TestTerrainAlongGreatArcRoute()
        {
            var request = new GreatArcRouteData(2);

            request.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 20000.0,
                Latitude = 39.07096,
                Longitude = -104.78509
            };
            request.Waypoints[0].Time = new DateTime(2014, 02, 10, 10, 30, 0).ToString();

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100.0,
                Latitude = 42.64541,
                Longitude = -61.11172
            };
            request.Waypoints[1].Time = new DateTime(2014, 02, 10, 18, 30, 20).ToString();
            request.OutputSettings.Step = 3600;
            request.OutputSettings.CoordinateFormat.Coord = CoordinateRepresentation.XYZ;

            var result = TerrainServices.GetTerrainHeightsAlongARoute<GreatArcRouteData>(request).Result;

            Assert.That(result != null);
            Assert.That(result.Count == 10);
            Assert.AreEqual(2286.85181f, result[0].TerrainHeightFromMeanSeaLevel);
            Assert.AreEqual(-16.9748859f, result[0].MeanSeaLevelHeightFromWgs84);
            Assert.AreEqual(2269.87671f, result[0].TerrainHeightFromWgs84);
        }
    }
}




