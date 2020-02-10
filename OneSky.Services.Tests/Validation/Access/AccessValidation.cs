using System;
using FluentAssertions;
using Newtonsoft.Json;
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
            var expected = JsonConvert.DeserializeObject<SatellitePassResults<ServiceCartographicWithTime>>(TestHelper.SatelliteAccessGeoToLeo);

            // call the service
            var passResults = AccessServices.GetSatellitePasses<SatellitePassResults<ServiceCartographicWithTime>>
                                                                                                (passRequest).Result;
            Assert.That(passResults.Passes.Count == 13);

            // object graph verification of actual results with expected results
            passResults.Passes.Should().BeEquivalentTo(expected.Passes);
        }

    }
}