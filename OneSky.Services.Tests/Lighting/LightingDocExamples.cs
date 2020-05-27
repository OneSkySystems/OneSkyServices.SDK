using System;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Lighting;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services.Lighting;

namespace OneSky.Services.Tests.Lighting
{
    [TestFixture]
    public class LightingDocExamples
    {
        [Test]
        public void SolarLighting_SiteLightingForThreeDays()
        {
            var site = new SiteData
            {
                Location = new ServiceCartographic(39.0, -75.77, 0)
            };
            var solarData = new SolarLightingData<SiteData>
            {
                Path = site,
                AnalysisStart = new DateTimeOffset(2018, 4, 9,
                    0, 0, 0, new TimeSpan(0, 0, 0)),
                AnalysisStop = new DateTimeOffset(2018, 4, 11, 
                    0, 0, 0, new TimeSpan(0, 0, 0)),
                OutputTimeOffset = -4.0f
            };

            var lighting = LightingServices.GetLightingAtASite(solarData).Result;

            // setup expected dates, then test
            var firstSunrise = new DateTimeOffset(2018, 4, 9,
                6, 35, 24, 756, new TimeSpan(-4, 0, 0));
            var firstAstroPmTwilightStop = new DateTimeOffset(2018, 4, 9,
                21, 08, 04, 142, new TimeSpan(-4, 0, 0));
            Assert.That(lighting.Lighting.Count == 3);
            Assert.AreEqual(firstSunrise.ToUnixTimeMilliseconds(),
                lighting.Lighting[0].Sunrise.ToUnixTimeMilliseconds());
            Assert.AreEqual(firstAstroPmTwilightStop.ToUnixTimeMilliseconds(),
                lighting.Lighting[0].AstronomicalTwilightPmStop.ToUnixTimeMilliseconds());
            Assert.IsTrue(lighting.Lighting[0].IsRiseDefined);
            Assert.IsTrue(lighting.Lighting[0].IsSetDefined);

            var secondSunset = new DateTimeOffset(2018, 4, 10,
                19, 35, 29, 937, new TimeSpan(-4, 0, 0));
            Assert.AreEqual(secondSunset.ToUnixTimeMilliseconds(),
                lighting.Lighting[1].Sunset.ToUnixTimeMilliseconds());

            var thirdNauticalPmTwilightStop = new DateTimeOffset(2018, 4, 11, 
                20, 36, 34, 092, new TimeSpan(-4, 0, 0));
            Assert.AreEqual(thirdNauticalPmTwilightStop.ToUnixTimeMilliseconds(),
                lighting.Lighting[2].NauticalTwilightPmStop.ToUnixTimeMilliseconds());
        }

        [Test]
        public void SolarLightingAlongARoute()
        {
            var route = new PointToPointRouteData();
            route.Waypoints.Add(new ServiceCartographicWithTime
            {
                Position = new ServiceCartographic(39.07096,-75.78509,600.0),
                Time = new DateTimeOffset(2014,03,25,18,30,0,new TimeSpan(0,0,0)).ToString()
            });
            route.Waypoints.Add(new ServiceCartographicWithTime
            {
                Position = new ServiceCartographic(42.06308,-75.7850,620.0),
                Time = new DateTimeOffset(2014,03,25,23,30,20,new TimeSpan(0,0,0)).ToString()
            });
            
            var solarData = new SolarLightingData<PointToPointRouteData>
            {
                Path = route,
                OutputTimeOffset = -4.0f
            };

            var result = LightingServices.GetLightingAlongARoute(solarData).Result;

            // setup expected dates, then test
            var sunset = new DateTimeOffset(2014, 3, 25,
                19, 20, 03, 664, new TimeSpan(-4, 0, 0));
            
            Assert.AreEqual(sunset.ToUnixTimeMilliseconds(),result.FlightLightingInfo.Sunset.ToUnixTimeMilliseconds());
            Assert.IsFalse(result.SunriseBetweenStartAndEnd);
            Assert.IsTrue(result.SunsetBetweenStartAndEnd);
            Assert.AreEqual("Daylight",result.BeginningOfFlightLightingCondition);
            Assert.AreEqual("CivilTwilight",result.EndOfFlightLightingCondition);
        }

         [Test]
        public void SolarAngles_SiteSolarAngles()
        {
            var site = new SiteData
            {
                Location = new ServiceCartographic(39.0, -75.77, 0)
            };
            site.OutputSettings.Step = 7200;
            var solarData = new SolarLightingData<SiteData>
            {
                Path = site,
                AnalysisStart = new DateTimeOffset(2018, 4, 9,
                    11, 0, 0, new TimeSpan(0, 0, 0)),
                AnalysisStop = new DateTimeOffset(2018, 4, 9, 
                    23, 0, 0, new TimeSpan(0, 0, 0)),
                OutputTimeOffset = -4.0f
            };

            var angles = LightingServices.GetSolarAnglesAtASite(solarData).Result;
            
            Assert.That(angles.Count == 7);
            // Validated against STK Components
            var firstAzimuth = 83.31917202200728;
            var lastAzimuth = 275.3959413062687;
            var firstElevation = 3.8993683721190497;
            var lastElevation = 5.791008759822339;
            var midAzimuth = 177.80736204126316;
            var midElevation = 58.7187946597877;
            var firstTime = new DateTimeOffset(2018, 4, 9,
                11, 0, 0, new TimeSpan(0, 0, 0));
            var midTime = new DateTimeOffset(2018, 4, 9,
                17, 0, 0, new TimeSpan(0, 0, 0));
            var lastTime = new DateTimeOffset(2018, 4, 9,
                23, 0, 0, new TimeSpan(0, 0, 0));

            var tol = 1e-6;
            Assert.AreEqual(firstTime,angles[0].Time);
            Assert.AreEqual(firstAzimuth,angles[0].Azimuth, tol);
            Assert.AreEqual(firstElevation,angles[0].Elevation, tol);
            Assert.AreEqual(midTime,angles[3].Time);
            Assert.AreEqual(midAzimuth,angles[3].Azimuth, tol);
            Assert.AreEqual(midElevation,angles[3].Elevation, tol);
            Assert.AreEqual(lastTime,angles[6].Time);
            Assert.AreEqual(lastAzimuth,angles[6].Azimuth, tol);
            Assert.AreEqual(lastElevation,angles[6].Elevation, tol);
        }

        [Test]
        public void SolarAnglesAlongARoute()
        {
            var route = new PointToPointRouteData();
            route.Waypoints.Add(new ServiceCartographicWithTime
            {
                Position = new ServiceCartographic(39.07096,-75.78509,2000.0),
                Time = new DateTimeOffset(2014,03,25,18,30,0,new TimeSpan(0,0,0)).ToString()
            });
            route.Waypoints.Add(new ServiceCartographicWithTime
            {
                Position = new ServiceCartographic(39.06308,-75.7850,2010.0),
                Time = new DateTimeOffset(2014,03,25,18,30,20,new TimeSpan(0,0,0)).ToString()
            });
            route.OutputSettings.Step = 5;
            
            var solarData = new SolarLightingData<PointToPointRouteData>
            {
                Path = route,
                OutputTimeOffset = -4.0f
            };

            var angles = LightingServices.GetSolarAnglesAlongARoute(solarData).Result;
            
            Assert.That(angles.Count == 5);

            // Validated against STK Components
            var firstAzimuth = 211.51272744968563;
            var lastAzimuth = 211.63258762262419;
            var firstElevation = 48.583033190527004;
            var lastElevation = 48.555909311795077;

            var firstTime = new DateTimeOffset(2014, 3, 25,
                18, 30, 0, new TimeSpan(0, 0, 0));
            var lastTime = new DateTimeOffset(2014, 3, 25,
                18, 30, 20, new TimeSpan(0, 0, 0));
            
            Assert.AreEqual(firstTime,angles[0].Time);
            Assert.AreEqual(firstAzimuth,angles[0].Azimuth);
            Assert.AreEqual(firstElevation,angles[0].Elevation);
            Assert.AreEqual(lastTime,angles[4].Time);
            Assert.AreEqual(lastAzimuth,angles[4].Azimuth);
            Assert.AreEqual(lastElevation,angles[4].Elevation);
        }
    }
}
