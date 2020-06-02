using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Exceptions;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Navigation;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Navigation;
using OneSky.Services.Services;

namespace OneSky.Services.Tests.Navigation
{
    [TestFixture]
    public class NavigationTests
    {
        #region GpsData
        //PSF
        [Test]
        public void TestPsfDataWithdate()
        {
            var psf = NavigationServices.GetPsfData(new DateTime(2019,01,01)).Result;
            Assert.That(!string.IsNullOrEmpty(psf));
            Assert.That(psf, Contains.Substring("PredictionSupportFile"));
            Assert.That(psf,Contains.Substring("2019-01-01"));
        }
        [Test]
        public void TestPsfDataWithoutdate()
        {
            var psf = NavigationServices.GetPsfData().Result;
            Assert.That(!string.IsNullOrEmpty(psf));
            Assert.That(psf, Contains.Substring("PredictionSupportFile"));
        }

        //PAF
        [Test]
        public void TestPafWithoutDate()
        {
            var paf = NavigationServices.GetPafData().Result;
            Assert.That(paf, Contains.Substring("PerformanceAssessmentFile"));
            Assert.That(!string.IsNullOrEmpty(paf));
        }
        [Test]
        public void TestPafWithDate()
        {
            var paf = NavigationServices.GetPafData(new DateTime(2019,01,01)).Result;
            Assert.That(!string.IsNullOrEmpty(paf));
            Assert.That(paf, Contains.Substring("PerformanceAssessmentFile"));
            Assert.That(paf, Contains.Substring("2019-01-01"));
        }
       
        //SOF and GPS Outages
        [Test]
        public void TestLatestSof()
        {
            var sof = NavigationServices.GetSofData().Result;
            Assert.That(!string.IsNullOrEmpty(sof));
            Assert.That(sof, Contains.Substring("SatelliteOutageFile"));
            Assert.That(sof, Contains.Substring("Latest"));
        }

        [Test]
        public void TestGpsOutagesWithDates()
        {
            //Update to use GetGpsOutages when AS-146 is addressed
            var result = NavigationServices.GetGpsOutagesString(
                new DateTime(2018, 02, 01),
                new DateTime(2018, 04, 01)).Result;
            Assert.That(!string.IsNullOrEmpty(result));
            Assert.That(result, Contains.Substring("2018-03-09T06:15:00Z"));
        }

        [Test]
        public void TestGpsOutagesWithDatesAndPrn()
        {
            // Update to use GetGpsOutages when AS-146 is addressed
            var outages = NavigationServices.GetGpsOutagesString(
                new DateTime(2018, 01, 01), 
                new DateTime(2018, 11, 01),
                18).Result;
            Assert.That(!string.IsNullOrEmpty(outages));
            Assert.That(outages,Contains.Substring("2018-01-23T16:50:00Z"));
        }

        //Almanac
        [Test]
        public void TestAlmanacWithoutDate()
        {
            var alm = NavigationServices.GetAlmanacData().Result;
            Assert.That(!string.IsNullOrEmpty(alm));
            Assert.That(alm,Contains.Substring("SEMAlmanac"));
        }
         [Test]
        public void TestAlmanacWithDate()
        {
            var alm = NavigationServices.GetAlmanacData(new DateTime(2019,02,04)).Result;
            Assert.That(!string.IsNullOrEmpty(alm));
            Assert.That(alm, Contains.Substring("SEMAlmanac"));
            Assert.That(alm, Contains.Substring("2019-02-04"));
        }
        #endregion

        #region Predicted
        [Test]
        public void TestPredictedErrorsOnSimpleFlightRoute()
        {
            var input = new NavigationPredictionData<IVerifiable> ();
            var path = new SimpleFlightRouteData
            {
                Start = new DateTimeOffset(2014, 5, 3, 0, 0, 0, 0, new TimeSpan(0))
            };
            path.Waypoints.Add(new ServiceCartographic2D(39.0,-104.77));
            path.Waypoints.Add(new ServiceCartographic2D(30.0,-98.0));
            path.Waypoints.Add(new ServiceCartographic2D(40.0,-77.0));
            path.TurningRadius = 1000.0;
            path.Speed = 111.76;
            path.Altitude = 9144.0;
            path.MeanSeaLevel = true;
            path.OutputSettings = new OutputSettings()
            {
                Step = 7200,
                TimeFormat = TimeRepresentation.Epoch,
                CoordinateFormat = new CoordinateType()
                {
                    Coord = CoordinateRepresentation.LLA,
                    Frame = FrameRepresentation.Fixed
                }
            };

            input.Path = path;
            input.BestN = false;
            input.MinimumElevationAngle = 5.0;
            input.NumberOfChannels = 12;
            input.ReceiverNoiseError = 0.8;
            input.UseBestAvailableData = true;
            input.ScaleToConfidence = true;
            input.PercentConfidence = 95;

            var expectedResult = JsonConvert.DeserializeObject<RouteNavigationErrorResults>(TestHelper.NavigationRoutePredictionScaled);
            var result = NavigationServices.GetPredictedNavigationErrorsOnARoute(input).Result;
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestPredictedErrorsOnSimpleFlightRouteNoBestAvailableData()
        {
            var input = new NavigationPredictionData<IVerifiable>();
            var path = new SimpleFlightRouteData
            {
                Start = DateTime.UtcNow.AddMonths(3)
            };
            path.Waypoints.Add(new ServiceCartographic2D(39.0, -104.77));
            path.Waypoints.Add(new ServiceCartographic2D(30.0, -98.0));
            path.Waypoints.Add(new ServiceCartographic2D(40.0, -77.0));
            path.TurningRadius = 1000.0;
            path.Speed = 111.76;
            path.Altitude = 9144.0;
            path.MeanSeaLevel = true;
            path.OutputSettings = new OutputSettings()
            {
                Step = 7200,
                TimeFormat = TimeRepresentation.Epoch,
                CoordinateFormat = new CoordinateType()
                {
                    Coord = CoordinateRepresentation.LLA,
                    Frame = FrameRepresentation.Fixed
                }
            };

            input.Path = path;
            input.BestN = false;
            input.MinimumElevationAngle = 5.0;
            input.NumberOfChannels = 12;
            input.ReceiverNoiseError = 0.8;
            input.UseBestAvailableData = false;
            input.ScaleToConfidence = true;
            input.PercentConfidence = 95;

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => NavigationServices.GetPredictedNavigationErrorsOnARoute(input));
            Assert.That(exc.ErrorId, Is.EqualTo(21300));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }
        #endregion

        #region Assessed
        [Test]
        public void TestAssessedErrorsAtSiteExtrapolated()
        {
            var request = new NavigationAssessmentData<IVerifiable>()
            {
                Path = new SiteData
                {
                    Location = new ServiceCartographic(35.6606, -77.376, 37.46),
                    OutputSettings = new OutputSettings() {Step = 7200},
                    MeanSeaLevel = false
                },
                AnalysisStart = DateTime.Parse("2014-01-30T20:00"),
                AnalysisStop = DateTime.Parse("2014-01-31T00:00"),
                NumberOfChannels = 50,
                MinimumElevationAngle = 10.5,
                BestN = false,
                UseBestAvailableData = true,
                ExtrapolatePafData = true

            };

            var expectedResult = JsonConvert.DeserializeObject<SiteNavigationErrorResults>(TestHelper.NavigationSiteAssessedExtrapolated);
            var result = NavigationServices.GetAssessedNavigationErrorsAtASite(request).Result;
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestAssessedErrorsAtSiteExtrapolatedWithoutPermission()
        {
            var request = new NavigationAssessmentData<IVerifiable>()
            {
                Path = new SiteData
                {
                    Location = new ServiceCartographic(35.6606, -77.376, 37.46),
                    OutputSettings = new OutputSettings() {Step = 7200},
                    MeanSeaLevel = false
                },
                AnalysisStart = DateTime.Parse("2014-01-30T20:00"),
                AnalysisStop = DateTime.Parse("2014-01-31T00:00"),
                NumberOfChannels = 50,
                MinimumElevationAngle = 10.5,
                BestN = false,
                UseBestAvailableData = true,
                // ExtrapolatePafData is false by default
            };

            var exc = Assert.CatchAsync<AnalyticalServicesException>(() => NavigationServices.GetAssessedNavigationErrorsAtASite(request));
            Assert.That(exc.ErrorId, Is.EqualTo(21250));
            Assert.That(exc.HelpLink, !Is.Empty);
            Assert.That(exc.Message, !Is.Empty);
        }

        [Test]
        public void TestAssessedErrorsOnRouteExtrapolated()
        {
            var request = new NavigationAssessmentData<IVerifiable>();
            var path = new SimpleFlightRouteData
            {
                Start = DateTime.Parse("2016-09-18T00:00:00Z"),
                Waypoints = new List<ServiceCartographic2D>
                {
                    new ServiceCartographic2D(35, -82),
                    new ServiceCartographic2D(-35, -150)
                },
                TurningRadius = 15,
                Speed = 20,
                Altitude = 100,
                MeanSeaLevel = true,
                OutputSettings = new OutputSettings
                {
                    Step = 172800,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat = new CoordinateType
                    {
                        Coord = CoordinateRepresentation.LLA
                    }
                }
            };
            request.Path = path;
            request.NumberOfChannels = 50;
            request.MinimumElevationAngle = 10.5;
            request.BestN = false;
            request.UseBestAvailableData = true;
            request.ExtrapolatePafData = true;

            var expectedResult = JsonConvert.DeserializeObject<RouteNavigationErrorResults>(TestHelper.NavigationRouteAssessedExtrapolated);
            var result = NavigationServices.GetAssessedNavigationErrorsOnARoute(request).Result;
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }
        #endregion

        #region Dop
        [Test]
        public void TestGpsDopAtASiteGpsOnly()
        {
            var request = new NavigationData<IVerifiable>()
            {
                AnalysisStart = DateTime.Parse("2014-05-03T00:00"),
                AnalysisStop = DateTime.Parse("2014-05-03T04:00"),
                NumberOfChannels = 30,
                MinimumElevationAngle = 7.5,
                Path = new SiteData
                {
                    Location = new ServiceCartographic(39.0, -104.0, 1000.0),
                    OutputSettings = new OutputSettings
                    {
                        Step = 3600
                    },
                    MeanSeaLevel = true
                }
            };
            var result = NavigationServices.GetDopAtASite(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<SiteDopResults>(TestHelper.NavigationSiteDopGpsOnly);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestGpsDopAtASiteEarthLosOff()
        {
            var request = new NavigationData<IVerifiable>()
            {
                AnalysisStart = DateTime.Parse("2014-05-03T00:00"),
                AnalysisStop = DateTime.Parse("2014-05-03T02:00"),
                NumberOfChannels = 90,
                MinimumElevationAngle = -90.0,
                EarthLineOfSight = false, // Earth is invisible
                Path = new SiteData
                {
                    Location = new ServiceCartographic(39.0, -104.0, 1000.0),
                    OutputSettings = new OutputSettings
                    {
                        Step = 3600
                    },
                    MeanSeaLevel = true
                }
            };
            var result = NavigationServices.GetDopAtASite(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<SiteDopResults>(TestHelper.NAvigationSiteDopEarthLineOfSightOff);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }
        #endregion
    }
}
