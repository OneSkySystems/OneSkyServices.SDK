using System;
using System.Drawing;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Access;
using OneSky.Services.Services.Access;

namespace OneSky.Services.Tests.Validation.Access
{
    [TestFixture]
    public class AccessDocExamples
    {
        [Test]
        public void SatellitePasses_IssAccessToSite()
        {
            var passRequest = new SatelliteAccessPassData<IVerifiable>
            {
                Start = new DateTime(2014, 8, 19, 0, 0, 0, DateTimeKind.Utc),
                Stop = new DateTime(2014, 8, 19, 12, 0, 0, DateTimeKind.Utc)
            };
            var sd = new SiteData
            {
                Location = new ServiceCartographic(40.0012, -75.661, 19.0),
                MeanSeaLevel = true,
                OutputSettings =
                {
                    Step = 30,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat = {Coord = CoordinateRepresentation.LLA}
                }
            };
            passRequest.FromObjectPath = sd;
            passRequest.SSCs.Add(25544); // the International Space Station
            passRequest.FromObjectDark = true;
            passRequest.ToObjectLit = true;
            passRequest.UseMinElevation = true;
            passRequest.FromObjectMinElevation = 10.0;
            passRequest.LineOfSight = true;
            passRequest.IncludePathData = true;
            passRequest.IncludePathCzml = true;
            passRequest.SatelliteOrbitColor = Color.Magenta.ToString();
            passRequest.PassLinkColor = Color.Green.ToString();
            passRequest.Verify();

            // call the service
            var passResults = AccessServices.GetSatellitePasses<SatellitePassResults<ServiceCartographicWithTime>>
                                                                                                (passRequest).Result;
            Assert.That(passResults.Passes.Count == 2);
            // First Pass
            Assert.AreEqual(-2.893973737835,passResults.Passes[0].MaxMagnitude,1e-12);
            Assert.AreEqual(15.61516492386,passResults.Passes[0].MaximumElevationData.Elevation,1e-11);
            Assert.AreEqual("2014-08-19T00:22:13.1728536Z",passResults.Passes[0].AccessStart);
            Assert.AreEqual("2014-08-19T00:26:28.4117640Z",passResults.Passes[0].AccessStop);
            // Second Pass
            Assert.AreEqual(-4.341255364434,passResults.Passes[1].MaxMagnitude,1e-12);
            Assert.AreEqual(55.23840734827,passResults.Passes[1].MaximumElevationData.Elevation,1e-11);
            Assert.AreEqual("2014-08-19T01:58:17.8074201Z",passResults.Passes[1].AccessStart);
            Assert.AreEqual("2014-08-19T02:01:13.4157620Z",passResults.Passes[1].AccessStop);
        }
        //TODO complete this test when Sensor is implemented
        [Test, Explicit]
        public void SensorFor_Noaa16ToIss()
        {
            var sensorForRequest = new SensorForAccessData<IVerifiable>()
            {
                Start = new DateTime(2014, 2, 20, 0, 0, 0, DateTimeKind.Utc),
                Stop = new DateTime(2014, 2, 21, 0, 0, 0, DateTimeKind.Utc)
            };
            var toObjectPath = new Sgp4RouteData()
            {
                Start = new DateTime(2014, 2, 20, 0, 0, 0, DateTimeKind.Utc),
                Stop = new DateTime(2014, 2, 21, 0, 0, 0, DateTimeKind.Utc),
                SSC = 25544,
                OutputSettings =
                {
                    Step = 2,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat = {Coord = CoordinateRepresentation.LLA}
                }
            };
            var fromObjectCatalogPath = new CatalogRouteData()
            {
                Start = new DateTime(2014, 2, 20, 0, 0, 0, DateTimeKind.Utc),
                Stop = new DateTime(2014, 2, 21, 0, 0, 0, DateTimeKind.Utc),
                URI = "https://sdf10.agi.com/SocSearch/catalogs/spacecraft/items/bald7veoYUqyISdiy-KK6w/definition",
                OutputSettings =
                {
                    Step = 2,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat = {Coord = CoordinateRepresentation.LLA}
                }
            };
            sensorForRequest.FromObjectCatalogPath = fromObjectCatalogPath;
            sensorForRequest.ToObjectPath = toObjectPath;
            sensorForRequest.SensorNameContains = "Noaa19_Avhrr3";
            sensorForRequest.Sunlit = false;
            sensorForRequest.IncludePath = false;
            sensorForRequest.Verify();

            // call the service
            //var forAccessResults = AccessServices.GetSensorForAccess<>(sensorForRequest).Result;
            //Assert.That(forAccessResults..);
            
        }
    }
}
