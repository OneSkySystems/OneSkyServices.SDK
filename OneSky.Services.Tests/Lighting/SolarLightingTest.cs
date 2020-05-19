using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs.Lighting;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Lighting;
using OneSky.Services.Services.Lighting;

namespace OneSky.Services.Tests.Basic.Lighting
{
    [TestFixture]
    public class SolarLightingTest
    {
        [Test]
        public void TestSolarLighting()
        {
            var request = new SolarLightingData<SiteData>
            {
                Path = new SiteData { Location = { Latitude = 39.0, Longitude = -104.77, Altitude = 1910 } },
                AnalysisStart = new DateTime(2018, 5, 5),
                AnalysisStop = new DateTime(2018, 5, 5),
                OutputTimeOffset = -6.0f
            };
            var lightingResult = LightingServices.GetLightingAtASite(request).Result;
            var expected = JsonConvert.DeserializeObject<FlightLightingConditions>(TestHelper.LightingBasicSite);
            lightingResult.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void TestSolarAngles()
        {
            var request = new SolarLightingData<SiteData>
            {
                Path = new SiteData
                {
                    Location = { Latitude = 39.0, Longitude = -75.77, Altitude = 0 },
                    OutputSettings = { Step = 7200}
                },
                AnalysisStart = new DateTime(2018, 4, 9, 11, 0, 0, DateTimeKind.Utc),
                AnalysisStop = new DateTime(2018, 4, 9, 23, 0, 0, DateTimeKind.Utc),
                OutputTimeOffset = -4.0f
            };
            var lightingResult = LightingServices.GetSolarAnglesAtASite(request).Result;
            var expected = JsonConvert.DeserializeObject<List<SolarAngles>>(TestHelper.LightingBasicAngles);
            lightingResult.Should().BeEquivalentTo(expected);
        }
    }
}