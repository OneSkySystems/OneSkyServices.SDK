using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Exceptions;
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
            request.Waypoints[0].Time = "2018-10-30T06:00:00Z";

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100,
                Latitude = 42.64541,
                Longitude = -61.11172
            };
            request.Waypoints[1].Time = "2018-10-30T07:00:00Z";
            request.OutputSettings.Step = 1800;      
            request.OutputSettings.TimeFormat = TimeRepresentation.UTC;

            var result = TerrainServices.GetTerrainHeightsAlongARoute(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<TerrainHeightAtLocationResponse>>(TestHelper.TerrainP2PRoute);
            result.Should().BeEquivalentTo(expectedResult, options => options
               .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
               .WhenTypeIs<double>()
               .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
               .When(info => info.SelectedMemberPath == "Time")
           );
        }

        [Test]
        public void TestTerrainAlongPointToPointRouteTooManyPoints()
        {

            var request = new PointToPointRouteData(2);

            request.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 20000,
                Latitude = 39.07096,
                Longitude = -104.78509
            };
            request.Waypoints[0].Time = "2018-10-30T10:30:00Z";

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100,
                Latitude = 42.64541,
                Longitude = -61.11172
            };
            request.Waypoints[1].Time = "2018-10-30T18:30:00Z";
            request.OutputSettings.Step = 60;
            request.OutputSettings.TimeFormat = TimeRepresentation.UTC;

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => TerrainServices.GetTerrainHeightsAlongARoute(request));
            Assert.That(exc.ErrorId, Is.EqualTo(20350));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestBadLatitude()
        {

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => TerrainServices.GetTerrainHeightsAtASite(91, -105.043498));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);

            exc = Assert.CatchAsync<AnalyticalServicesException>(() => TerrainServices.GetTerrainHeightsAtASite(-91, -105.043498));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestBadLongitude()
        {
            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => TerrainServices.GetTerrainHeightsAtASite(80, -361));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);

            exc = Assert.CatchAsync<AnalyticalServicesException>(() => TerrainServices.GetTerrainHeightsAtASite(80, 361));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }
    }
}
