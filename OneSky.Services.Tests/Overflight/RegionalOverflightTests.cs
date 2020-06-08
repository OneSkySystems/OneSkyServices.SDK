using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Access;
using OneSky.Services.Services.Overflight;

namespace OneSky.Services.Tests.Overflight
{
    [TestFixture]
    public class RegionalOverflightTests
    {
        [Test]
        public void PointToPointOverflightAllCountries()
        {
            var overflightInput = new OverflightAccessData<IVerifiable> { IncludePath = true };
            var path = new PointToPointRouteData()
            {
                Waypoints = new List<ServiceCartographicWithTime>
                {
                    new ServiceCartographicWithTime(new ServiceCartographic(39.07096,-104.78509, 20000.0),"2020-06-03T10:30:00"),
                    new ServiceCartographicWithTime(new ServiceCartographic(42.64541,-61.11172, 100.0),"2020-06-03T18:30:20"),
                },
                OutputSettings =
                {
                    Step = 900,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat = {Coord = CoordinateRepresentation.LLA}
                }
            };
            overflightInput.Path = path;
            overflightInput.IncludePath = false;

            var result = OverflightServices.GetRegionalOverflight<ServiceCartesianWithTime>(overflightInput).Result;

            var expected = JsonConvert.DeserializeObject<List<OverflightAccessResult<ServiceCartesianWithTime>>>(TestHelper.RegionalOverflightP2P);
            result.Should().BeEquivalentTo(expected,options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void PointToPointOverUserDefinedRegion()
        {
            var overflightInput = new OverflightAccessData<IVerifiable> 
            {
                IncludePath = false,
                Path = new PointToPointRouteData()
                {
                    Waypoints = new List<ServiceCartographicWithTime>
                    {
                        new ServiceCartographicWithTime(new ServiceCartographic(39.926634, -111.133952, 200.0),"2020-06-03T10:30:00"),
                        new ServiceCartographicWithTime(new ServiceCartographic(39.905344, -111.091664, 100.0),"2020-06-03T10:55:20"),
                    },
                    OutputSettings =
                    {
                        Step = 60,
                        TimeFormat = TimeRepresentation.UTC,
                        CoordinateFormat = {Coord = CoordinateRepresentation.LLA}
                    }
                }                
            };
            overflightInput.Regions.Add(new NamedRegion
            {
                Name = "Soldier Point",
                Positions = new List<ServiceCartographic>
                {
                    new ServiceCartographic(39.909438,-111.132076,2000),
                    new ServiceCartographic(39.930431,-111.105812,2000),
                    new ServiceCartographic(39.923741,-111.084189,2000),
                    new ServiceCartographic(39.900152,-111.116667,2000),
                    new ServiceCartographic(39.909438,-111.132076,2000),
                }
            });

            var result = OverflightServices.GetRegionalOverflight<ServiceCartographicWithTime>(overflightInput).Result;

            var expected = JsonConvert.DeserializeObject<List<OverflightAccessResult<ServiceCartographicWithTime>>>(TestHelper.RegionalOverflightUserDefinedP2P);
            result.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void GreatArcOverflightAllCountries()
        {
            var overflightInput = new OverflightAccessData<IVerifiable> { IncludePath = true };
            var path = new GreatArcRouteData(2)
            {
                Waypoints = new List<ServiceCartographicWithTime>
                {
                    new ServiceCartographicWithTime(new ServiceCartographic(39.07096,-104.78509, 20000.0),"2020-06-03T10:30:00"),
                    new ServiceCartographicWithTime(new ServiceCartographic(42.64541,-61.11172, 100.0),"2020-06-03T18:30:20"),
                },
                OutputSettings =
                {
                    Step = 900,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat = {Coord = CoordinateRepresentation.LLA}
                }
            };
            overflightInput.Path = path;
            overflightInput.IncludePath = false;

            var result = OverflightServices.GetRegionalOverflight<ServiceCartesianWithTime>(overflightInput).Result;

            var expected = JsonConvert.DeserializeObject<List<OverflightAccessResult<ServiceCartesianWithTime>>>(TestHelper.RegionalOverflightGreatArc);
            result.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }
    }
}
