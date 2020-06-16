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
        #region Airspace Intersections with Routes
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
		
        [Test]
        public void TestAirspaceIntersectionFromRouteWithRegionCriteria()
        {
            var request = new StaticAirspaceRouteData<IVerifiable>
            {
                Path = new SimpleFlightRouteData
                {
                    Start = DateTimeOffset.Parse("2016-09-18T00:00:00Z"),
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
                IncludePath = false,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                   UseRegionalAirspaceQuery = true,
                   RegionRadius = 100000,
                   RegionCenter = new ServiceCartographic
                   {
                       Latitude = 35.74664809,
                       Longitude = -82.68041262,
                       Altitude = 100
                   }
                }
            };

            var result = AirspaceServices.GetAirspaceCrossingsForARoute<ServiceCartographicWithTime>(request).Result;
            var expected = JsonConvert.DeserializeObject
                <StaticAirspaceAccessResult<AirspaceCrossingResult<ServiceCartographicWithTime>>>(
                TestHelper.AirspaceIntersectionFromARouteWithRegionCriteria);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TestGreatArcIntersectionWithRegionCriteria()
        {
            var request = new StaticAirspaceRouteData<IVerifiable>
            {
                Path = new GreatArcRouteData
                {
                    Waypoints = new List<ServiceCartographicWithTime>
                    {
                        new ServiceCartographicWithTime
                        {
                            Position = new ServiceCartographic
                            {
                                Latitude = 34.65853034,
	                            Longitude = -76.64861499,
	                            Altitude = 30.48

                            },
                            Time = "2017-09-12T18:00:00Z"
                        },
                        new ServiceCartographicWithTime
                        {
                            Position = new ServiceCartographic
                            {
                                Latitude = 34.71899801,
                                Longitude = -76.64239038,
                                Altitude = 30.48

                            },
                            Time = "2017-09-12T18:01:55Z"
                        },
                        new ServiceCartographicWithTime
                        {
                            Position = new ServiceCartographic
                            {
                                Latitude = 34.84704720,
                                Longitude = -77.12257484,
                                Altitude = 30.48

                            },
                            Time = "2017-09-12T18:13:44Z"
                        },
                        new ServiceCartographicWithTime
                        {
                            Position = new ServiceCartographic
                            {
                                Latitude = 34.52781345,
                                Longitude = -77.44714396,
                                Altitude = 30.48

                            },
                            Time = "2017-09-12T18:26:20Z"
                        },
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
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory> { AirspaceCategory.Parks },
                    UseRegionalAirspaceQuery = true,
                    RegionRadius = 70000,
                    RegionCenter = new ServiceCartographic
                    {
                        Latitude = 34.65853034,
                        Longitude = -76.64861499,
                        Altitude = 30.48
                    }
                },
                UseHorizontalProximity =  false,
                HorizontalProximityThreshold = 30,
	            UseVerticalProximity =  false
            };

            var result = AirspaceServices.GetAirspaceCrossingsForARoute<ServiceCartographicWithTime>(request).Result;
            var expected = JsonConvert.DeserializeObject
                <StaticAirspaceAccessResult<AirspaceCrossingResult<ServiceCartographicWithTime>>>(
                TestHelper.AirspaceGreatArcIntersectionWithRegionCriteria);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TestGreatArcIntersectionWithHorzProx()
        {
            var request = new StaticAirspaceRouteData<IVerifiable>
            {
                Path = new GreatArcRouteData
                {
                    Waypoints = new List<ServiceCartographicWithTime>
                    {
                        new ServiceCartographicWithTime
                        {
                            Position = new ServiceCartographic
                            {
                                Latitude = 34.65853034,
                                Longitude = -76.64861499,
                                Altitude = 30.48

                            },
                            Time = "2017-09-12T18:00:00Z"
                        },
                        new ServiceCartographicWithTime
                        {
                            Position = new ServiceCartographic
                            {
                                Latitude = 34.71899801,
                                Longitude = -76.64239038,
                                Altitude = 30.48

                            },
                            Time = "2017-09-12T18:01:55Z"
                        },
                        new ServiceCartographicWithTime
                        {
                            Position = new ServiceCartographic
                            {
                                Latitude = 34.84704720,
                                Longitude = -77.12257484,
                                Altitude = 30.48

                            },
                            Time = "2017-09-12T18:13:44Z"
                        },
                        new ServiceCartographicWithTime
                        {
                            Position = new ServiceCartographic
                            {
                                Latitude = 34.52781345,
                                Longitude = -77.44714396,
                                Altitude = 30.48

                            },
                            Time = "2017-09-12T18:26:20Z"
                        },
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
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory> { AirspaceCategory.Parks },
                    UseRegionalAirspaceQuery = true,
                    RegionRadius = 70000,
                    RegionCenter = new ServiceCartographic
                    {
                        Latitude = 34.65853034,
                        Longitude = -76.64861499,
                        Altitude = 30.48
                    }
                },
                UseHorizontalProximity = true,
                UseVerticalProximity = false
            };

            var result = AirspaceServices.GetAirspaceCrossingsForARoute<ServiceCartographicWithTime>(request).Result;
            var expected = JsonConvert.DeserializeObject
                <StaticAirspaceAccessResult<AirspaceCrossingResult<ServiceCartographicWithTime>>>(
                TestHelper.AirspaceGreatArcIntersectionWithHorzProx);
            result.Should().BeEquivalentTo(expected);
        }
        #endregion

        #region Airspace Intersection with Point Flights
        [Test]
        public void TestPointFlightWithRegionAndProx()
        {
            var request = new StaticAirspacePointFlightData
            {
                Center = new ServiceCartographic
                {
                    Latitude = 38.4392428,
                    Longitude = -77.5364245
                },
                Radius = 1,
                MaxAltitude = 10001,
                MinAltitude = 0,
                MeanSeaLevel = false,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory> { AirspaceCategory.Default },
                    OnSurface = false,
                    UseRegionalAirspaceQuery = true,
                    RegionRadius = 50000,
                    RegionCenter = new ServiceCartographic
                    {
                        Latitude = 38.4392428,
                        Longitude = -77.5364245
                    }
                },
                UseHorizontalProximity = true,
                HorizontalProximityThreshold = 1,
                UseVerticalProximity = false,
                VerticalProximityThreshold = 1
                
            };

            var result = AirspaceServices.GetAirspaceCrossingsForACylinder(request).Result;
            var expected = JsonConvert.DeserializeObject<StaticAirspaceAccessResult<AirspaceIdentifier>>(TestHelper.AirspacePointIntersectionWithRegionAndProx);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TestPointFlightLaanc()
        {
            var request = new StaticAirspacePointFlightData
            {
                Center = new ServiceCartographic
                {
                    Latitude = 43.0918973,
                    Longitude = -108.6251340,
                    Altitude = 0
                },
                Radius = 600,
                MaxAltitude = 1600,
                MinAltitude = 0,
                MeanSeaLevel = false,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory> { AirspaceCategory.Laanc },
                    UseRegionalAirspaceQuery = true,
                    RegionRadius = 50000,
                    RegionCenter = new ServiceCartographic
                    {
                        Latitude = 43.0918973,
                        Longitude = -108.6251340,
                        Altitude = 0
                    }
                },
                UseHorizontalProximity = true,
                HorizontalProximityThreshold = 1000,
                UseVerticalProximity = true,
                VerticalProximityThreshold = 100

            };

            var result = AirspaceServices.GetAirspaceCrossingsForACylinder(request).Result;
            var expected = JsonConvert.DeserializeObject<StaticAirspaceAccessResult<AirspaceIdentifier>>(TestHelper.AirspacePointLaanc);
            result.Should().BeEquivalentTo(expected);
        }
        #endregion

        #region Airspace Selection
        [Test]
        public void TestAirspaceSelectionSearch()
        {
            var request = new AirspaceSelectionOptions
            {
                Categories = new List<AirspaceCategory> { AirspaceCategory.ControlledAirspace },
                AirspaceIds = new List<string>
                {
                    "CLASS E"
                },
                Search = true,
                UseRegionalAirspaceQuery = true,
                RegionRadius = 150000,
                RegionCenter = new ServiceCartographic
                {
                    Latitude = 40,
                    Longitude = -104.77,
                    Altitude = 0
                }
            };

            var result = AirspaceServices.SelectAirspaces(request).Result;
            var expected = JsonConvert.DeserializeObject<AirspaceIdResult>(TestHelper.AirspaceSelectionWithSearch);
            result.Should().BeEquivalentTo(expected);
        }       
        #endregion

        #region Airspace real time
        [Test]
        public void TestAirspaceInRealtime()
        {
            var request = new StaticAirspaceRealTimeData
            {
                Location = new ServiceCartographic
                {
                    Latitude = 38.4392428,
                    Longitude = -77.5364245,
                    Altitude = 100
                },
                MeanSeaLevel = false,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory> { AirspaceCategory.Default },
                    UseRegionalAirspaceQuery = true,
                    RegionRadius = 50000,
                    RegionCenter = new ServiceCartographic
                    {
                        Latitude = 38.4392428,
                        Longitude = -77.5364245
                    }
                }
            };            

            var result = AirspaceServices.RealTime(request).Result;
            var expected = JsonConvert.DeserializeObject<StaticAirspaceAccessResult<AirspaceIdentifier>>(TestHelper.AirspaceRealtimeWithRegion);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TestRealTimeLaanc()
        {
            var request = new StaticAirspaceRealTimeData
            {
                Location = new ServiceCartographic
                {
                    Latitude = 43.0918973,
                    Longitude = -108.6251340,
                    Altitude = 1600
                },
                MeanSeaLevel = false,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    Categories = new List<AirspaceCategory> { AirspaceCategory.Laanc },
                    UseRegionalAirspaceQuery = true,
                    RegionRadius = 50000,
                    RegionCenter = new ServiceCartographic
                    {
                        Latitude = 43.0918973,
                        Longitude = -108.6251340,
                        Altitude = 1690
                    }
                },
                UseHorizontalProximity = true,
                HorizontalProximityThreshold = 1000,
                UseVerticalProximity = true,
                VerticalProximityThreshold = 100

            };

            var result = AirspaceServices.RealTime(request).Result;
            var expected = JsonConvert.DeserializeObject<StaticAirspaceAccessResult<AirspaceIdentifier>>(TestHelper.AirspaceRealTimeLaanc);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TestAirspaceInRealtimeUnrecognized()
        {
            var request = new StaticAirspaceRealTimeData
            {
                Location = new ServiceCartographic
                {
                    Latitude = 38.4392428,
                    Longitude = -77.5364245,
                    Altitude = 100
                },
                MeanSeaLevel = false,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    AirspaceIds = new List<string>
                    {
                        "SOME AIRSPACE NAME"
                    }
                }
            };

            var result = AirspaceServices.RealTime(request).Result;
            var expected = JsonConvert.DeserializeObject<StaticAirspaceAccessResult<AirspaceIdentifier>>(TestHelper.AirspaceRealTimeUnrecognized);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TestAirspaceInRealtimeDefinedId()
        {
            var request = new StaticAirspaceRealTimeData
            {
                Location = new ServiceCartographic
                {
                    Latitude = 38.4392428,
                    Longitude = -77.5364245,
                    Altitude = 100
                },
                MeanSeaLevel = false,
                AirspaceOptions = new AirspaceSelectionOptions
                {
                    AirspaceIds = new List<string>
                    {
                        "FLYING T FARM"
                    }
                }
            };

            var result = AirspaceServices.RealTime(request).Result;
            var expected = JsonConvert.DeserializeObject<StaticAirspaceAccessResult<AirspaceIdentifier>>(TestHelper.AirspaceRealtimeWithRegion);
            result.Should().BeEquivalentTo(expected);
        }
        #endregion
    }

}