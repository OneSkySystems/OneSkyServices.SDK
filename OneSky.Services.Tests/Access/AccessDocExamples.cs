using System;
using System.Drawing;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Access;
using OneSky.Services.Services.Access;

namespace OneSky.Services.Tests.Access
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
            var rawResponse = AccessServices.GetSatellitePasses<string>(passRequest).Result;
            var passResults = JsonConvert.DeserializeObject<SatellitePassResults<ServiceCartographicWithTime>>(rawResponse, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            });

            // object graph verification of actual results with expected results
            var expected = JsonConvert.DeserializeObject<SatellitePassResults<ServiceCartographicWithTime>>(TestHelper.SatellitePassesIssAccessToSite);
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
