using FluentAssertions;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Visualization;
using OneSky.Services.Services.Czml;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OneSky.Services.Inputs.Airspace;
using OneSky.Services.Outputs.Airspace;


namespace OneSky.Services.Tests.Visualization
{
    [TestFixture]
    public class VisualizationDocExamples
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
            result.Should().Contain(expectedResult, "generated id in CZML will be different, so we can only check some of the data" );
            result.Should().Contain("\"color\":{\"rgba\":[0,255,0,255]", "checking the line color is green for the orbit");
        }
        // Airspace Viz tests needed here

        [Test]
        public void TestAirspaceVis()
        {
            // Note that this test is slightly different from the documented example.
            // It was modified to reduce the amount of data returned from the service, allowing for easier testing.
            var A1 = new AirspaceCzmlData()
            {
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory>
                    {
                        AirspaceCategory.Parks
                    },
                    RegionCenter = new ServiceCartographic
                    {
                        Latitude = 37.13328,
                        Longitude = -76.41104,
                        Altitude = 0
                    },
                    UseRegionalAirspaceQuery = true,
                    RegionRadius = 25000
                },
                Color = "Green",
                OutlineColor = "LimeGreen",
                DisplayStart = DateTime.Parse("2017-01-01T01:00:00Z"),
                DisplayStop = DateTime.Parse("2017-01-01T06:59:59Z")
            };
            var A2 = new AirspaceCzmlData()
            {
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory>
                    {
                        AirspaceCategory.Airport
                    },
                    RegionCenter = new ServiceCartographic
                    {
                        Latitude = 37.13328,
                        Longitude = -76.41104,
                        Altitude = 0
                    },
                    UseRegionalAirspaceQuery = true,
                    RegionRadius = 8000
                },
                Color = "Gray",
                Outline = false,
                DisplayStart = DateTime.Parse("2017-01-01T06:00:00Z"),
                DisplayStop = DateTime.Parse("2017-01-01T11:59:59Z")
            };
            var request = new List<AirspaceCzmlData>
            {
                A1,
                A2
            };
            var expectedResult = JsonConvert.DeserializeObject<AirspaceCzml>(TestHelper.VisualizationAirspaceDocExample);
            var result = CzmlServices.GetAirspaceCzml(request).Result;
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
