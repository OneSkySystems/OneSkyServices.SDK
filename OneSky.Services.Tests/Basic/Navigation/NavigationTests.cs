using System;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Navigation;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Navigation;
using OneSky.Services.Services.Navigation;

namespace OneSky.Services.Tests.Basic.Navigation
{
    [TestFixture]
    public class NavigationTests
    {       
        [Test]
        public void TestPsfDataWithdate()
        {
            var psf = NavigationServices.GetPsfData(new DateTime(2019,01,01)).Result;
            Assert.That(!string.IsNullOrEmpty(psf));
        }
        [Test]
        public void TestPsfDataWithoutdate()
        {
            var psf = NavigationServices.GetPsfData().Result;
            Assert.That(!string.IsNullOrEmpty(psf));
        }
        [Test]
        public void TestPafWithoutDate()
        {
            var paf = NavigationServices.GetPafData().Result;
            Assert.That(!string.IsNullOrEmpty(paf));
        }
         [Test]
        public void TestPafWithDate()
        {
            var paf = NavigationServices.GetPafData(new DateTime(2019,01,01)).Result;
            Assert.That(!string.IsNullOrEmpty(paf));
        }
        [Test]
        public void TestSofWithoutDate()
        {
            var sof = NavigationServices.GetSofData().Result;
            Assert.That(!string.IsNullOrEmpty(sof));
        }
         [Test]
        public void TestSofWithDate()
        {
            var sof = NavigationServices.GetSofData(new DateTime(2019,01,01)).Result;
            Assert.That(!string.IsNullOrEmpty(sof));
        }
        [Test]
        public void TestAlmanacWithoutDate()
        {
            var alm = NavigationServices.GetAlmanacData().Result;
            Assert.That(!string.IsNullOrEmpty(alm));
        }
         [Test]
        public void TestAlmanacWithDate()
        {
            var alm = NavigationServices.GetAlmanacData(new DateTime(2019,02,04)).Result;
            Assert.That(!string.IsNullOrEmpty(alm));
        }

        [Test]
        public void TestGpsOutagesDatesAndPrn()
        {
            var outages = NavigationServices.GetGpsOutages(
                new DateTime(2019,01,01),new DateTime(2019,03,22)).Result;
            Assert.That(!string.IsNullOrEmpty(outages));
        }

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
                Step = 60,
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
            input.ScaleToConfidence = false;
            input.PercentConfidence = 95;

            var expectedResult = JsonConvert.DeserializeObject<RouteNavigationErrorResults>(TestHelper.NavigationBasicPrediction);
            var result = NavigationServices.GetPredictedNavigationErrorsOnARoute(input).Result;
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
