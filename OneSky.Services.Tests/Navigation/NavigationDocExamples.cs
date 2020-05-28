using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Navigation;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Navigation;
using OneSky.Services.Services;

namespace OneSky.Services.Tests.Navigation
{
    [TestFixture]
    public class NavigationDocExamples
    {
        [Test]
        public void TestGpsSatelliteOutages()
        {
            // Use GetGpsOutages method when AS-146 is fixed
            var satelliteOutages = NavigationServices.GetGpsOutagesString(null,null,22).Result;
            Assert.That(!string.IsNullOrEmpty(satelliteOutages));
            Assert.That(satelliteOutages,Contains.Substring("1998-03-24T14:10:00Z"));
            //AS-146 will allow this test statement to be assessed
            //Assert.That(satelliteOutages[0].Start,Is.EqualTo(DateTime.Parse("1998-03-24T14:10:00Z")));
        }

        [Test]
        public void TestGpsData()
        {
            var psfData = NavigationServices.GetPsfData().Result;
            Assert.That(!string.IsNullOrEmpty(psfData));
        }

        [Test]
        public void TestGpsPredictedErrorsAtASite()
        {
            var request = new NavigationPredictionData<IVerifiable>()
            {
                AnalysisStart = DateTime.Parse("2014-05-03T00:00"),
                AnalysisStop = DateTime.Parse("2014-05-03T01:00"),
                NumberOfChannels = 10,
                MinimumElevationAngle = 7.5,
                ReceiverNoiseError = 1.2,
                Path = new SiteData
                {
                    Location = new ServiceCartographic(39.0, -104.0, 1000.0),
                    OutputSettings = new OutputSettings
                    {
                        Step = 900
                    },
                    MeanSeaLevel = true
                }
            };
            var result = NavigationServices.GetPredictedNavigationErrorsAtASite(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<SiteNavigationErrorResults>(TestHelper.NavigationSitePredictionDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestGpsPredictedErrorsAlongARasterRoute()
        {
            var request = new NavigationPredictionData<IVerifiable>()
            {
                NumberOfChannels = 10,
                MinimumElevationAngle = 7.5,
                ReceiverNoiseError = 1.2,
                Path = new RasterRouteData()
                {
                    Start = DateTime.Parse("2014-05-03T08:34:56Z"),
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
                        Step = 900
                    }
                }
            };
            var result = NavigationServices.GetPredictedNavigationErrorsOnARoute(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<RouteNavigationErrorResults>(TestHelper.NavigationRoutePredictionDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestGpsAssessedErrorsAtASite()
        {
            var request = new NavigationAssessmentData<IVerifiable>()
            {
                AnalysisStart = DateTime.Parse("2014-05-03T00:00"),
                AnalysisStop = DateTime.Parse("2014-05-03T01:00"),
                NumberOfChannels = 10,
                MinimumElevationAngle = 7.5,
                ReceiverNoiseError = 1.2,
                Path = new SiteData
                {
                    Location = new ServiceCartographic(39.0, -104.0, 1000.0),
                    OutputSettings = new OutputSettings
                    {
                        Step = 900
                    },
                    MeanSeaLevel = true
                }
            };
            var result = NavigationServices.GetAssessedNavigationErrorsAtASite(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<SiteNavigationErrorResults>(TestHelper.NavigationSiteAssessmentDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestGpsAssessedErrorsAlongARasterRoute()
        {
            var request = new NavigationAssessmentData<IVerifiable>()
            {
                NumberOfChannels = 10,
                MinimumElevationAngle = 7.5,
                ReceiverNoiseError = 1.2,
                Path = new RasterRouteData()
                {
                    Start = DateTime.Parse("2014-05-03T08:34:56Z"),
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
                        Step = 900
                    }
                }
            };
            var result = NavigationServices.GetAssessedNavigationErrorsOnARoute(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<RouteNavigationErrorResults>(TestHelper.NavigationRouteAssessmentDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestGpsDopAtASite()
        {
            var request = new NavigationData<IVerifiable>()
            {
                AnalysisStart = DateTime.Parse("2014-05-03T00:00"),
                AnalysisStop = DateTime.Parse("2014-05-03T04:00"),
                NumberOfChannels = 30,
                MinimumElevationAngle = 7.5,
                Constellations = new List<NavigationConstellationType> 
                {
                    NavigationConstellationType.Gps,
                    NavigationConstellationType.Glonass,
                    NavigationConstellationType.Sbas
                },
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
            var expectedResult = JsonConvert.DeserializeObject<SiteDopResults>(TestHelper.NavigationSiteDopDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }

        [Test]
        public void TestGpsDopAlongARasterRoute()
        {
            var request = new NavigationData<IVerifiable>()
            {
                NumberOfChannels = 10,
                MinimumElevationAngle = 7.5,
                Path = new RasterRouteData()
                {
                    Start = DateTime.Parse("2014-05-03T08:34:56Z"),
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
                        Step = 300
                    }
                }
            };
            var result = NavigationServices.GetDopOnARoute(request).Result;
            var expectedResult = JsonConvert.DeserializeObject<RouteDopResults>(TestHelper.NavigationRouteDopDocExample);
            result.Should().BeEquivalentTo(expectedResult, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info => info.SelectedMemberPath == "Time")
            );
        }
    }
}
