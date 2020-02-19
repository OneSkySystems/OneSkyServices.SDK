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

            // call the service
            var rawResponse = AccessServices.GetSatellitePasses<string>(passRequest).Result;
            var passResults = JsonConvert.DeserializeObject<SatellitePassResults<ServiceCartographicWithTime>>(rawResponse, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            });

            // object graph verification of actual results with expected results
            var expected = JsonConvert.DeserializeObject<SatellitePassResults<ServiceCartographicWithTime>>(TestHelper.SatelliteAccessGeoToLeo);
            passResults.Passes.Should().BeEquivalentTo(expected.Passes, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
                .Using<string>(ctx => ctx.Subject.Should().StartWith(ctx.Expectation.Substring(0, TestHelper.PrecisionStringLengthTime)))
                .When(info =>
                    info.SelectedMemberPath == "AccessStart" ||
                    info.SelectedMemberPath == "AccessStop" ||
                    info.SelectedMemberPath == "AccessBeginData.Time" ||
                    info.SelectedMemberPath == "AccessEndData.Time" ||
                    info.SelectedMemberPath == "MaximumElevationData.Time")
            );
        }

    }
}