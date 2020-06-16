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
    public class AirspaceTests
    {
        [Test]
        public void TestIntersectionWithParksAndRestrictedCategories()
        {
            var request = new StaticAirspaceRouteData<IVerifiable>
            {
                Path =  new GreatArcRouteData(4)
                {
                    Waypoints = new List<ServiceCartographicWithTime>
                    {
                        new ServiceCartographicWithTime(34.65853034, -76.64861499, 30.48, "2017-09-12T18:00:00"),
                        new ServiceCartographicWithTime(34.71899801, -76.64239038, 30.48, "2017-09-12T18:01:55"),
                        new ServiceCartographicWithTime(34.84704720, -77.12257484, 30.48, "2017-09-12T18:13:44"),
                        new ServiceCartographicWithTime(34.52781345, -77.44714396, 30.48, "2017-09-12T18:26:20")
                    },
                    OutputSettings = new OutputSettings
                    {
                        Step = 60,
                        TimeFormat = TimeRepresentation.Epoch,
                        CoordinateFormat = new CoordinateType
                        {
                            Coord = CoordinateRepresentation.LLA
                        }
                    }
                },
                IncludePath = true,
                UseHorizontalProximity = false,
                UseVerticalProximity = false,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory>
                    {
                        AirspaceCategory.Parks,
                        AirspaceCategory.Restricted
                    }
                }
            };

            var results = AirspaceServices.GetAirspaceCrossingsForARoute<GreatArcRouteData>(request).Result;
            Assert.That(results.AirspacesAccessed.Count,Is.EqualTo(9));
            Assert.That(results.UnrecognizedAirspaceIds.Count,Is.EqualTo(0));
        }
    }
}
