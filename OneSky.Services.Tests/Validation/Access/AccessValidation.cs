using System;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Access;
using OneSky.Services.Services.Access;

namespace OneSky.Services.Tests.Validation.Access
{
    [TestFixture]
    public class AccessValidation
    {
        [Test]
        public void GeoToLeo()
        {
            var passRequest = new SatelliteAccessPassData<IVerifiable>
            {
                Start = new DateTime(2014, 8, 19, 0, 0, 0, DateTimeKind.Utc),
                Stop = new DateTime(2014, 8, 19, 12, 0, 0, DateTimeKind.Utc)
            };
            var fromObjectPath = new Sgp4RouteData
            {
                Start = new DateTime(2014, 8, 19, 0, 0, 0, DateTimeKind.Utc),
                Stop = new DateTime(2014, 8, 19, 12, 0, 0, DateTimeKind.Utc),
                SSC = 19548,
                OutputSettings =
                {
                    TimeFormat = TimeRepresentation.UTC
                }
            };
            passRequest.FromObjectPath = fromObjectPath;
            passRequest.SSCs.Add(25544); // the International Space Station
            passRequest.FromObjectDark = false;
            passRequest.ToObjectLit = true;
            passRequest.UseMinElevation = false;
            passRequest.LineOfSight = true;
            passRequest.IncludePathCzml = true;
            passRequest.Verify();

            // call the service
            var passResults = AccessServices.GetSatellitePasses<SatellitePassResults<ServiceCartographicWithTime>>
                                                                                                (passRequest).Result;
            Assert.That(passResults.Passes.Count == 13);
            // First Pass
            Assert.AreEqual(7.285152814472,passResults.Passes[0].MaxMagnitude,1e-12);
            Assert.AreEqual(-80.727902792933861,passResults.Passes[0].MaximumElevationData.Elevation,1e-15);
            Assert.AreEqual("2014-08-19T00:10:47.7098365Z",passResults.Passes[0].AccessStart);
            Assert.AreEqual("2014-08-19T00:28:20.1973285Z",passResults.Passes[0].AccessStop);
            // Second Pass
            Assert.AreEqual(4.137398061182,passResults.Passes[12].MaxMagnitude,1e-12);
            Assert.AreEqual(-80.7507306151,passResults.Passes[12].MaximumElevationData.Elevation,1e-10);
            Assert.AreEqual("2014-08-19T11:54:24.1844298Z",passResults.Passes[12].AccessStart);
            Assert.AreEqual("2014-08-19T12:00:00.0000000Z",passResults.Passes[12].AccessStop);
        }

    }
}