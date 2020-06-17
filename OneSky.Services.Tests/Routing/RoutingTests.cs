using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services;
using System;
using System.Collections.Generic;

namespace OneSky.Services.Tests.Routing
{
    [TestFixture]
    public class RoutingTests
    {   
        #region SGP4
        [Test]
        public void TestSgp4RouteUsingTle()
        {
            var request = new Sgp4RouteData
            {
                Start = new DateTimeOffset(2018, 11, 1, 3, 0, 0, TimeSpan.Zero),
                Stop = new DateTimeOffset(2018, 11, 1, 4, 0, 0, TimeSpan.Zero),
                TLEs = new List<string>
                {
                    "1 22871U 93066A   18308.81194535 -.00000197  00000-0  00000-0 0  99982 22871   5.1556  64.6235 0001701 208.3757 358.2742  0.99320289 91475"
                },
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
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartesianWithTime>>(TestHelper.RoutingSgp4FromTle);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
            );
        }

        [Test]
        public void TestSgp4RouteUsingBadSsc()
        {
            var request = new Sgp4RouteData
            {
                Start = new DateTimeOffset(2018, 11, 1, 3, 0, 0, TimeSpan.Zero),
                Stop = new DateTimeOffset(2018, 11, 1, 4, 0, 0, TimeSpan.Zero),
                SSC = 99999,
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

           var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<Sgp4RouteData, ServiceCartesianWithTime>(request));
           Assert.That(exc.ErrorId, Is.EqualTo(20800));
           Assert.That(exc.HelpLink, !Is.Empty);
           Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestSgp4RouteUsingBadTle()
        {
            var request = new Sgp4RouteData
            {
                Start = new DateTimeOffset(2018, 11, 1, 3, 0, 0, TimeSpan.Zero),
                Stop = new DateTimeOffset(2018, 11, 1, 4, 0, 0, TimeSpan.Zero),
                TLEs = new List<string>
                {
                    "1 22871U 93066A   18308.81194535 -.00071   5.1556  64.6235 0001701 208.3757 358.2742  0.99320289 91475"
                },
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

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<Sgp4RouteData, ServiceCartesianWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(9999));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestSgp4RouteUsingSscAndTle()
        {
            var request = new Sgp4RouteData
            {
                Start = new DateTimeOffset(2018, 11, 1, 3, 0, 0, TimeSpan.Zero),
                Stop = new DateTimeOffset(2018, 11, 1, 4, 0, 0, TimeSpan.Zero),
                SSC = 25544,
                TLEs = new List<string>
                {
                    "1 22871U 93066A   18308.81194535 -.00000197  00000-0  00000-0 0  99982 22871   5.1556  64.6235 0001701 208.3757 358.2742  0.99320289 91475"
                },
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
            // Expected result is an ephemeris generated fom the SSC, not the TLE.
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartesianWithTime>>(TestHelper.RoutingSgp4FromSsc);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
            );
        }

        [Test]
        public void TestSgp4RouteMissingStart()
        {
            var request = new Sgp4RouteData
            {
                //Start = new DateTimeOffset(2014, 2, 20, 3, 0, 0, TimeSpan.Zero),
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

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<Sgp4RouteData,ServiceCartesianWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestSgp4RouteMissingStop()
        {
            var request = new Sgp4RouteData
            {
                Start = new DateTimeOffset(2014, 2, 20, 3, 0, 0, TimeSpan.Zero),
                //Stop = new DateTimeOffset(2014, 2, 20, 4, 0, 0, TimeSpan.Zero),
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

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<Sgp4RouteData,ServiceCartesianWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestSgp4RouteMissingSscAndTle()
        {
            var request = new Sgp4RouteData
            {
                Start = new DateTimeOffset(2014, 2, 20, 3, 0, 0, TimeSpan.Zero),
                Stop = new DateTimeOffset(2014, 2, 20, 4, 0, 0, TimeSpan.Zero),
                //SSC = 25544,
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

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<Sgp4RouteData,ServiceCartesianWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(24150)); 
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestSgp4RouteOptionalOutputSettings()
        {
            var request = new Sgp4RouteData
            {
                Start = new DateTimeOffset(2014, 2, 20, 3, 0, 0, TimeSpan.Zero),
                Stop = new DateTimeOffset(2014, 2, 20, 4, 0, 0, TimeSpan.Zero),
                SSC = 25544
            };

            // https://saas.onesky.xyz/V1/Documentation/Common#outputsettings
            Assert.That(request.OutputSettings,!Is.Null);
            Assert.That(request.OutputSettings.Step,Is.EqualTo(60));
            Assert.That(request.OutputSettings.TimeFormat,Is.EqualTo(TimeRepresentation.Epoch));
            Assert.That(request.OutputSettings.CoordinateFormat.Coord,Is.EqualTo(CoordinateRepresentation.LLA));
        }

        #endregion

        #region Point To Point
        [Test]
        public void TestPointToPointRouteCartesian()
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
                Altitude = 1910,
                Latitude = 38.794,
                Longitude = -105.217755
            };
            request.Waypoints[1].Time = "2018-10-30T07:00:00Z";
            request.OutputSettings.Step = 45;
            request.OutputSettings.TimeFormat = TimeRepresentation.UTC;
            request.OutputSettings.CoordinateFormat.Coord = CoordinateRepresentation.XYZ;       

            var result = RouteServices.GetRoute<PointToPointRouteData,ServiceCartesianWithTime>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartesianWithTime>>(TestHelper.RoutingP2PCart);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void TestPointToPointRouteFromJson()
        {
            var json = "{\"Waypoints\": [{\"Position\": {\"Latitude\": 39.07096,\"Longitude\": -104.78509,\"Altitude\": 2000.0},\"Time\": \"2014-03-25T18:30:00\"},{\"Position\": {\"Latitude\": 39.06308,\"Longitude\": -104.78500,\"Altitude\": 2010.0},\"Time\": \"2014-03-25T18:30:20\"}],\"IncludeWaypointsInRoute\": true,\"OutputSettings\": {\"Step\": 5,\"TimeFormat\": \"Epoch\",\"CoordinateFormat\": {\"Coord\": \"LLA\" }}}";
            var request = new PointToPointRouteData(json);
            
            Assert.NotNull(request);
        }
        [Test]
        public void TestPointToPointRouteMissingWaypoint()
        {
            var request = new PointToPointRouteData(2);

            request.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 1910,
                Latitude = 39.0,
                Longitude = -104.77
            };
            request.Waypoints[0].Time = "2018-10-30T06:00:00Z";

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<PointToPointRouteData, ServiceCartesianWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestPointToPointRouteOptionalOutputSettings()
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
                Altitude = 1910,
                Latitude = 38.794,
                Longitude = -105.217755
            };

            request.Waypoints[1].Time = "2018-10-30T07:00:00Z";
            // https://saas.onesky.xyz/V1/Documentation/Common#outputsettings
            Assert.That(request.OutputSettings, !Is.Null);
            Assert.That(request.OutputSettings.Step, Is.EqualTo(60));
            Assert.That(request.OutputSettings.TimeFormat, Is.EqualTo(TimeRepresentation.Epoch));
            Assert.That(request.OutputSettings.CoordinateFormat.Coord, Is.EqualTo(CoordinateRepresentation.LLA));
        }
        [Test]
        public void TestPointToPointRouteOptionalIncludeWaypoints()
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
                Altitude = 1910,
                Latitude = 38.794,
                Longitude = -105.217755
            };
            request.Waypoints[1].Time = "2018-10-30T07:00:00Z";

            Assert.That(request.IncludeWaypointsInRoute, Is.True);
        }
        #endregion

        #region Simple Flight
        [Test]
        public void TestSimpleFlightLong()
        {
            var request = new SimpleFlightRouteData
            {
                Start = new DateTimeOffset(2016, 9, 18, 0, 0, 0, TimeSpan.Zero),
                TurningRadius = 15,
                Speed = 200,
                Altitude = 100,
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
            request.Waypoints.Add(new ServiceCartographic2D(35.0,-82.0));
            request.Waypoints.Add(new ServiceCartographic2D(-35.0,-150.0));
            
            var result = RouteServices.GetRoute<SimpleFlightRouteData,ServiceCartographicWithTime>(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<List<ServiceCartographicWithTime>>(TestHelper.RoutingSimpleFlightLong);
            result.Should().BeEquivalentTo(expectedResult,options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }
        [Test]
        public void TestSimpleFlightMissingStart()
        {
            var request = new SimpleFlightRouteData
            {
               // Start = new DateTimeOffset(2016, 9, 18, 0, 0, 0, TimeSpan.Zero),
                TurningRadius = 15,
                Speed = 200,
                Altitude = 100,
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
            request.Waypoints.Add(new ServiceCartographic2D(35.0, -82.0));
            request.Waypoints.Add(new ServiceCartographic2D(-35.0, -150.0));

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<SimpleFlightRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }
        [Test]
        public void TestSimpleFlightMissingWaypoint()
        {
            var request = new SimpleFlightRouteData
            {
                Start = new DateTimeOffset(2016, 9, 18, 0, 0, 0, TimeSpan.Zero),
                TurningRadius = 15,
                Speed = 200,
                Altitude = 100,
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
            request.Waypoints.Add(new ServiceCartographic2D(35.0, -82.0));
            //request.Waypoints.Add(new ServiceCartographic2D(-35.0, -150.0));

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<SimpleFlightRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }
        [Test]
        public void TestSimpleFlightOptionalTurnRadius()
        {
            var request = new SimpleFlightRouteData();
            Assert.That(request.TurningRadius, Is.EqualTo(200));
        }
        [Test]
        public void TestSimpleFlightOptionalSpeed()
        {
            var request = new SimpleFlightRouteData();
            Assert.That(request.Speed, Is.EqualTo(65));
        }
        [Test]
        public void TestSimpleFlightOptionalAltitude()
        {
            var request = new SimpleFlightRouteData();
            Assert.That(request.Altitude, Is.EqualTo(1000));
        }
        [Test]
        public void TestSimpleFlightOptionalMeanSeaLevel()
        {
            var request = new SimpleFlightRouteData();
            Assert.That(request.MeanSeaLevel, Is.EqualTo(true));
        }
        #endregion

        #region Great Arc
        [Test]
        public void TestGreatArcRouteNotEnoughWaypoints()
        {
            var request = new GreatArcRouteData(2);
            
            request.Waypoints[0].Position = new ServiceCartographic
            {
                Altitude = 2000,
                Latitude = 39.07096,
                Longitude = -104.78509
            };
            request.Waypoints[0].Time = "2014-03-25T18:30:00";
            request.OutputSettings.Step = 5;
            request.OutputSettings.TimeFormat = TimeRepresentation.Epoch;
            request.OutputSettings.CoordinateFormat.Coord = CoordinateRepresentation.LLA;       

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<GreatArcRouteData,ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }
        #endregion

        #region Takeoff and Langing
        [Test]
        public void TestTolRoute()
        {
            var request = new TolRouteData
            {
                Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56, TimeSpan.Zero),
                //TakeOffPoint = new ServiceCartographic(31.0, -122.0, 15.0),
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
            
            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<TolRouteData,ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(24000));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestTolMissingStart()
        {
            var request = new TolRouteData()
            {
                // Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56, TimeSpan.Zero),
                TakeOffPoint = new ServiceCartographic(31.0, -122.0, 15.0),
                LandingPoint = new ServiceCartographic(40.0, -77.0, 180.0),
                Speed = 200,
                Altitude = 100,
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

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<TolRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }
        [Test]
        public void TestTolMissingTakeoffPoint()
        {
            var request = new TolRouteData()
            {
                Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56, TimeSpan.Zero),
                //TakeOffPoint = new ServiceCartographic(31.0, -122.0, 15.0),
                LandingPoint = new ServiceCartographic(40.0, -77.0, 180.0),
                Speed = 200,
                Altitude = 100,
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

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<TolRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(24000));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }
        [Test]
        public void TestTolMissingLandingPoint()
        {
            var request = new TolRouteData()
            {
                Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56, TimeSpan.Zero),
                TakeOffPoint = new ServiceCartographic(31.0, -122.0, 15.0),
                //LandingPoint = new ServiceCartographic(40.0, -77.0, 180.0),
                Speed = 200,
                Altitude = 100,
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

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<TolRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(24000));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }
        [Test]
        public void TestTolMissingOptionalSpeed()
        {
            var request = new TolRouteData();
            Assert.That(request.Speed, Is.EqualTo(65));
        }
        [Test]
        public void TestTolMissingOptionalAltitude()
        {
            var request = new TolRouteData();
            Assert.That(request.Altitude, Is.EqualTo(1000));
        }
        [Test]
        public void TestTolMissingOptionalMeanSeaLevel()
        {
            var request = new TolRouteData();
            Assert.That(request.MeanSeaLevel, Is.EqualTo(true));
        }
        #endregion

        #region Raster Search
        [Test]
        public void TestRasterSearchRouteMissingStart()
        {
            var request = new RasterRouteData()
            {
                //Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56,TimeSpan.Zero),
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
            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<RasterRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600)); 
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestRasterSearchRouteMissingCenterPoint()
        {
            var request = new RasterRouteData()
            {
                Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56,TimeSpan.Zero),
                //CenterPoint = new ServiceCartographic2D(31.0, -122.0),
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
            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<RasterRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(24000)); 
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestRasterSearchRouteMissingLength()
        {
            var request = new RasterRouteData()
            {
                Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56, TimeSpan.Zero),
                CenterPoint = new ServiceCartographic2D(31.0, -122.0),
                //Length = 20000,
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
            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<RasterRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600)); //AS-145 will update this code to 24000
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestRasterSearchRouteMissingWidth()
        {
            var request = new RasterRouteData()
            {
                Start = new DateTimeOffset(2014, 01, 18, 8, 34, 56, TimeSpan.Zero),
                CenterPoint = new ServiceCartographic2D(31.0, -122.0),
                Length = 20000,
                //Width = 30000,
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
            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => RouteServices.GetRoute<RasterRouteData, ServiceCartographicWithTime>(request));
            Assert.That(exc.ErrorId, Is.EqualTo(23600)); //AS-145 will update this code to 24000
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestRasterSearchRouteOptionalSearchHeading()
        {
            var request = new RasterRouteData();
            Assert.That(request.SearchHeading,Is.EqualTo(0));
        }

        [Test]
        public void TestRasterSearchRouteOptionalTurningRadius()
        {
            var request = new RasterRouteData();
            Assert.That(request.TurningRadius, Is.EqualTo(200));
        }

        [Test]
        public void TestRasterSearchRouteOptionalSpeed()
        {
            var request = new RasterRouteData();
            Assert.That(request.Speed, Is.EqualTo(65));
        }

        [Test]
        public void TestRasterSearchRouteOptionalAltitude()
        {
            var request = new RasterRouteData();
            Assert.That(request.Altitude, Is.EqualTo(1000));
        }

        [Test]
        public void TestRasterSearchRouteOptionalMeanSeaLevel()
        {
            var request = new RasterRouteData();
            Assert.That(request.MeanSeaLevel, Is.EqualTo(true));
        }
        #endregion


    }
}