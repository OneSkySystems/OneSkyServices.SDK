using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Terrain;
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
            var expectedResult = JsonConvert.DeserializeObject<Heights>(TestHelper.TerrainSiteDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
            );
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
            request.Waypoints[0].Time = "2014-02-10T10:30:00Z";

            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 100.0,
                Latitude = 42.64541,
                Longitude = -61.11172
            };
            request.Waypoints[1].Time = "2014-02-10T18:30:20Z";
            request.OutputSettings.Step = 3600;
            request.OutputSettings.TimeFormat = TimeRepresentation.UTC;

            var result = TerrainServices.GetTerrainHeightsAlongARoute(request).Result;

            var expectedResult = JsonConvert.DeserializeObject<List<TerrainHeightAtLocationResponse>>(TestHelper.TerrainRouteDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }
    }
}




