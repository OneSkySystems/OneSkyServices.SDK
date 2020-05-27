using System;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Access;
using OneSky.Services.Outputs.Lighting;
using OneSky.Services.Services.Overflight;
using OneSky.Services.Services.Terrain;
using System.Collections.Generic;

namespace OneSky.Services.Tests.Overflight
{
    [TestFixture]
    public class RegionalOverflightDocExamples
    {
        [Test]
        public void IssForThirtyMinutes()
        {
            var overflightInput = new OverflightAccessData<IVerifiable> {IncludePath = true};
            var path = new Sgp4RouteData
            {
                Start = DateTimeOffset.Parse("2014-02-10T00:00:00Z"),
                Stop = DateTimeOffset.Parse("2014-02-10T00:30:00Z"),
                SSC = 25544,
                OutputSettings =
                {
                    Step = 300,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat = {Coord = CoordinateRepresentation.XYZ, Frame = FrameRepresentation.Fixed}
                }
            };
            overflightInput.Path = path;

            var result = OverflightServices.GetRegionalOverflight<ServiceCartesianWithTime>(overflightInput).Result;

            var expected = JsonConvert.DeserializeObject<List<OverflightAccessResult<ServiceCartesianWithTime>>>(TestHelper.RegionalOverflightDoc);
            result.Should().BeEquivalentTo(expected);
        }
    }
}