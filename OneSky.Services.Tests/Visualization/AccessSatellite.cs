using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Inputs.Visualization;
using OneSky.Services.Services.Czml;
using OneSky.Services.Services.Czml;

namespace OneSky.Services.Tests.Visualization
{
    [TestFixture]
    public class AccessSatellite
    {
        [Test]
        public void TestSgp4Vis()
        {
            var request = new Sgp4CzmlRouteData
            {
                SSC = 25544,
                PathColor = "Green",
                SatelliteName = "ISS",
                Start = DateTimeOffset.Parse("2020-04-15T13:02:55Z"),
                Stop = DateTimeOffset.Parse("2020-04-15T14:02:55Z"),
                OutputSettings = new OutputSettings
                {
                    Step = 300,
                    TimeFormat = TimeRepresentation.Epoch,
                    CoordinateFormat = new CoordinateType()
                    {
                        Coord = CoordinateRepresentation.XYZ,
                        Frame = FrameRepresentation.Inertial
                    }
                }
            };
            var expectedResult = TestHelper.VisualizationSgp4DocExample;
            var result = CzmlServices.GetSgp4Czml(request).Result;
            result.Should().Contain(expectedResult, "generated id will be different, so we can only check most of the CZML" );
            result.Should().Contain("\"color\":{\"rgba\":[0,255,0,255]", "the color green for the orbit");

        }
    }
}
