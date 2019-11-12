using System;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services.Terrain;

namespace OneSky.Services.Tests.Basic.Terrain
{
    [TestFixture]
    public class TerrainTests
    {       
        [Test]
        public void TestTerrainAlongPointToPointRoute()
        {
            var request = new PointToPointRouteData(2);
            
            request.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 1910,
                Latitude = 39.0,
                Longitude = -104.77
            };
            request.Waypoints[0].Time = new DateTime(2018,10,30,0,0,0);

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 1910,
                Latitude = 38.794,
                Longitude = -105.217755
            };
            request.Waypoints[1].Time = new DateTime(2018,10,30,1,0,0);
            request.OutputSettings.Step = 20;      

            var result = TerrainServices.GetTerrainHeightsAlongARoute<PointToPointRouteData>(request).Result;

            Assert.That(result != null);
            Assert.That(result.Count == 181);
            Assert.AreEqual(2091.64136f,result[0].TerrainHeightFromMeanSeaLevel);
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
            request.Waypoints[0].Time = new DateTime(2014,02,10,10,30,0);

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100.0,
                Latitude = 42.64541,
                Longitude = -61.11172
            };
            request.Waypoints[1].Time = new DateTime(2014,02,10,18,30,20);
            request.OutputSettings.Step = 3600;      
            request.OutputSettings.CoordinateFormat.Coord = CoordinateRepresentation.XYZ;

            var result = TerrainServices.GetTerrainHeightsAlongARoute<GreatArcRouteData>(request).Result;

            Assert.That(result != null);
            Assert.That(result.Count == 10);
            Assert.AreEqual(2286.85181f,result[0].TerrainHeightFromMeanSeaLevel);
            Assert.AreEqual(-16.9748859f,result[0].MeanSeaLevelHeightFromWgs84);
            Assert.AreEqual(2269.87671f,result[0].TerrainHeightFromWgs84);
        }

         [Test]
        public void TestTerrainAtASite()
        {
            var result = TerrainServices.GetTerrainHeightsAtASite(39.0,-104.77).Result;

            Assert.That(result != null);
            Assert.AreEqual(2091.6413192307896,result.TerrainHeightFromMeanSeaLevel);
            Assert.AreEqual(-17.063999999999989,result.MeanSeaLevelHeightFromWgs84);
            Assert.AreEqual(2074.5773192307897,result.TerrainHeightFromWgs84);
        }
    }
}
