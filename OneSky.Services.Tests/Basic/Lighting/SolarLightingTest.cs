using System;
using NUnit.Framework;
using OneSky.Services.Inputs.Lighting;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services.Lighting;

namespace OneSky.Services.Tests.Basic.Lighting
{
    [TestFixture]
    public class SolarLightingTest
    {
        [Test]
        public void TestSolarLighting()
        {
            var request = new SolarLightingData<SiteData>();
            request.Path = new SiteData();
            request.Path.Location.Latitude = 39.0;
            request.Path.Location.Longitude = -104.77;
            request.Path.Location.Altitude = 1910;
            request.AnalysisStart = new DateTime(2018,5,5);
            request.AnalysisStop = new DateTime(2018,5,5);
            request.OutputTimeOffset = -6.0f;
            var lightingResult = LightingServices.GetLightingAtASite(request).Result;
            Assert.That(lightingResult != null);
            Assert.That(lightingResult.Lighting[0].Sunrise.Hour == 5);
            Assert.That(lightingResult.Lighting[0].Sunrise.Minute == 56);
            Assert.That(lightingResult.Lighting[0].Sunrise.Second == 20);
            Assert.That(lightingResult.Lighting[0].Sunset.Hour == 19);
            Assert.That(lightingResult.Lighting[0].Sunset.Minute == 55);
            Assert.That(lightingResult.Lighting[0].Sunset.Second == 49);

        }

        [Test]
        public void TestSolarAngles()
        {
            var request = new SolarLightingData<SiteData>();
            request.Path = new SiteData();
            request.Path.Location.Latitude = 39.0;
            request.Path.Location.Longitude = -104.77;
            request.Path.Location.Altitude = 1910;
            request.AnalysisStart = new DateTime(2018,5,5);
            request.AnalysisStop = new DateTime(2018,5,5);
            request.OutputTimeOffset = -6.0f;
            var lightingResult = LightingServices.GetSolarAnglesAtASite(request).Result;
            Assert.That(lightingResult != null);
        }
    }
}