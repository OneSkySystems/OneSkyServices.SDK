using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services;

namespace OneSky.Services.Tests.Routing
{
    [TestFixture]
    public class RoutingDocExamples
    {       
        [Test]
        public void TestSgp4Route()
        {
            var request = new Sgp4RouteData
            {
                Start = new DateTimeOffset(2014, 2, 20, 3, 0, 0, TimeSpan.Zero),
                Stop = new DateTimeOffset(2014, 2, 20, 4, 0, 0, TimeSpan.Zero),
                SSC = 25544,
                OutputSettings =
                {
                    Step = 300,
                    TimeFormat = TimeRepresentation.Epoch,
                    CoordinateFormat =
                    {
                        Coord = CoordinateRepresentation.XYZ, Frame = FrameRepresentation.Inertial
                    }
                }
            };

            var result = RouteServices.GetRoute<Sgp4RouteData,ServiceCartesianWithTime>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartesianWithTime>>(TestHelper.RoutingSgp4DocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
            );
        }

        [Test]
        public void TestPointToPointRoute()
        {
            var request = new PointToPointRouteData(2);
            
            request.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 2000,
                Latitude = 39.07096,
                Longitude = -104.78509
            };
            request.Waypoints[0].Time = "2014-03-25T18:30:00";
            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 2010,
                Latitude = 39.06308,
                Longitude = -104.78500
            };
            request.Waypoints[1].Time = "2014-03-25T18:30:20";
            request.OutputSettings.Step = 5;
            request.OutputSettings.TimeFormat = TimeRepresentation.Epoch;
            request.OutputSettings.CoordinateFormat.Coord = CoordinateRepresentation.LLA;       

            var result = RouteServices.GetRoute<PointToPointRouteData,ServiceCartographicWithTime>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartographicWithTime>>(TestHelper.RoutingP2PDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
            );
        }
        
        [Test]
        public void TestSimpleFlightRoute()
        {
            var request = new SimpleFlightRouteData
            {
                Start = new DateTimeOffset(2014, 1, 18, 8, 34, 56, TimeSpan.Zero),
                TurningRadius = 200,
                Speed = 500,
                Altitude = 5000,
                MeanSeaLevel = true,
                OutputSettings =
                {
                    Step = 3600,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat =
                    {
                        Coord = CoordinateRepresentation.LLA
                    }
                }
            };
            request.Waypoints.Add(new ServiceCartographic2D(-32.6,22.0));
            request.Waypoints.Add(new ServiceCartographic2D(30.0,30.16));
            
            var result = RouteServices.GetRoute<SimpleFlightRouteData,ServiceCartographicWithTime>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartographicWithTime>>(TestHelper.RoutingSimpleFlightDocExample);
            result.Should().BeEquivalentTo(expectedResult,options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestGreatArcRoute()
        {
            var request = new GreatArcRouteData(2);
            
            request.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 2000,
                Latitude = 39.07096,
                Longitude = -104.78509
            };
            request.Waypoints[0].Time = "2014-03-25T18:30:00";
            request.Waypoints[1].Position = new ServiceCartographic
            {
                Altitude = 2010,
                Latitude = 39.06308,
                Longitude = -104.78500
            };
            request.Waypoints[1].Time = "2014-03-25T18:30:20";
            request.OutputSettings.Step = 5;
            request.OutputSettings.TimeFormat = TimeRepresentation.Epoch;
            request.OutputSettings.CoordinateFormat.Coord = CoordinateRepresentation.LLA;       

            var result = RouteServices.GetRoute<GreatArcRouteData,ServiceCartographicWithTime>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartographicWithTime>>(TestHelper.RoutingGreatArcDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
            );
        }

        [Test]
        public void TestTakeoffAndLandingRoute()
        {
            var request = new TolRouteData
            {
                Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56, TimeSpan.Zero),
                TakeOffPoint = new ServiceCartographic(31.0, -122.0, 15.0),
                LandingPoint = new ServiceCartographic(40.0, -77.0, 180.0),
                Speed = 400,
                Altitude = 9144,
                MeanSeaLevel = true,
                OutputSettings = new OutputSettings
                {
                    Step = 3600,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat =
                    {
                        Coord = CoordinateRepresentation.LLA
                    }
                }
            };
            
            var result = RouteServices.GetRoute<TolRouteData,ServiceCartographicWithTime>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartographicWithTime>>(TestHelper.RoutingTolDocExample);
            result.Should().BeEquivalentTo(expectedResult,options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestRasterSearchRoute()
        {
            var request = new RasterRouteData()
            {
                Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56,TimeSpan.Zero),
                CenterPoint = new ServiceCartographic2D(31.0, -122.0),
                Length = 20000,
                Width = 30000,
                SearchHeading = 45,
                TurningRadius = 1000,
                Speed = 110,
                Altitude = 2000,
                MeanSeaLevel = true,
                OutputSettings = new OutputSettings
                {
                    Step = 300,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat =
                    {
                        Coord = CoordinateRepresentation.LLA
                    }
                }
            };
            
            var result = RouteServices.GetRoute<RasterRouteData,ServiceCartographicWithTime>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartographicWithTime>>(TestHelper.RoutingRasterSearchDocExample);
            result.Should().BeEquivalentTo(expectedResult,options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }
    }
}