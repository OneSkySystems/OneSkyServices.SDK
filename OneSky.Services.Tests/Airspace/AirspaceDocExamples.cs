using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Airspace;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Airspace;
using OneSky.Services.Services.Airspace;
using System;
using System.Collections.Generic;


namespace OneSky.Services.Tests.Airspace
{
    [TestFixture]
    public class AirspaceDocExamples
    {
        [Test]
        public void TestFindAllAirspacesWithin1Km()
        {
            var request = new AirspaceSelectionOptions
            {
                Categories = new List<AirspaceCategory>
                {
                    AirspaceCategory.ControlledAirspace,
                    AirspaceCategory.Airport,
                    AirspaceCategory.Restricted,
                    AirspaceCategory.SpecialUseArea,
                    AirspaceCategory.Parks
                },
                RegionCenter = new ServiceCartographic(38.890903, -77.036035, 0),
                UseRegionalAirspaceQuery = true,
                RegionRadius = 1000

            };
            var results = AirspaceServices.SelectAirspaces(request).Result;
            var expected = JsonConvert.DeserializeObject<AirspaceIdResult>(TestHelper.AirspaceSelectAirspacesDocExample);
            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TestAirspaceIntersectionFromRoute()
        {
            var request = new StaticAirspaceRouteData<IVerifiable>
            {
                Path = new SimpleFlightRouteData
                {
                    Start = DateTimeOffset.Parse("2016-09-18T00:00:00"),
                    Waypoints = new List<ServiceCartographic2D>
                    {
                        new ServiceCartographic2D(35.74664809,-82.68041262),
                        new ServiceCartographic2D(35.18442928,-82.42519487),
                        new ServiceCartographic2D(34.96250080,-80.26139220),
                        new ServiceCartographic2D(35.62088862,-80.13563273),
                        new ServiceCartographic2D(35.29169471,-81.70022851)
                    },
                    TurningRadius = 15,
                    Speed = 20,
                    Altitude = 100,
                    MeanSeaLevel = true,
                    OutputSettings = new OutputSettings
                    {
                        Step = 300,
                        TimeFormat = TimeRepresentation.UTC,
                        CoordinateFormat = new CoordinateType
                        {
                            Coord = CoordinateRepresentation.LLA
                        }
                    }
                },
                IncludePath = true,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory>
                    {
                        AirspaceCategory.ControlledAirspace,
                        AirspaceCategory.Restricted
                    }
                }
            };

            var result = AirspaceServices.GetAirspaceCrossingsForARoute(request).Result;
            var expected = JsonConvert.DeserializeObject<AirspaceIdResult>(TestHelper.AirspaceIntersectionFromARoute);
            result.Should().BeEquivalentTo(expected);
        }

    }
}
