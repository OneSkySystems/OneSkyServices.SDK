using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Terrain;
using OneSky.Services.Services.Terrain;
using System;
using System.Collections.Generic;

namespace OneSky.Services.Tests.Terrain
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
            request.Waypoints[0].Time = new DateTime(2018,10,30,6,0,0, DateTimeKind.Utc).ToString();

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 1910,
                Latitude = 38.794,
                Longitude = -105.217755
            };
            request.Waypoints[1].Time = new DateTime(2018,10,30,7,0,0, DateTimeKind.Utc).ToString();
            request.OutputSettings.Step = 20;      
            request.OutputSettings.TimeFormat = TimeRepresentation.UTC;

            var result = TerrainServices.GetTerrainHeightsAlongARoute<PointToPointRouteData>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<TerrainHeightAtLocationResponse>>(TestHelper.TerrainBasicP2P);
            result.Should().BeEquivalentTo(expectedResult);
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
            request.Waypoints[0].Time = new DateTime(2014,02,10,17,30,0, DateTimeKind.Utc).ToString();

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100.0,
                Latitude = 42.64541,
                Longitude = -61.11172
            };
            request.Waypoints[1].Time = new DateTime(2014,02,11,1,30,20, DateTimeKind.Utc).ToString();
            request.OutputSettings.Step = 3600;      
            request.OutputSettings.CoordinateFormat.Coord = CoordinateRepresentation.XYZ;
            request.OutputSettings.TimeFormat = TimeRepresentation.UTC;

            var result = TerrainServices.GetTerrainHeightsAlongARoute<GreatArcRouteData>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<TerrainHeightAtLocationResponse>>(TestHelper.TerrainBasicGA);
            result.Should().BeEquivalentTo(expectedResult);
        }

         [Test]
        public void TestTerrainAtASite()
        {
            var result = TerrainServices.GetTerrainHeightsAtASite(39.0,-104.77).Result;
            var expectedResult = JsonConvert.DeserializeObject<Heights>(TestHelper.TerrainBasicSite);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
