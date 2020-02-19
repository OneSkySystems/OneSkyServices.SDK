using System;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Access;
using OneSky.Services.Services.Access;

namespace OneSky.Services.Tests.Basic.Access
{
    [TestFixture]
    public class BasicAccessTests
    {
        [Test]
        public void TestSatellitePasses()
        {
            var passRequest = new SatelliteAccessPassData<IVerifiable>();
            passRequest.SSCs.Add(25544); // the International Space Station
            passRequest.Start = new DateTime(2019, 5, 27, 12, 0, 0, DateTimeKind.Utc);
            passRequest.Stop = passRequest.Start.AddDays(7);
            passRequest.FromObjectPath = new SiteData
            {
                Location = new ServiceCartographic(40.0, -75.0, 0.0)
            };
            passRequest.ToObjectLit = true;
            passRequest.FromObjectDark = true;
            passRequest.IncludePathCzml = true;
            passRequest.Verify();

            // call the service
            var rawResponse = AccessServices.GetSatellitePasses<string>(passRequest).Result;
            var passResults = JsonConvert.DeserializeObject<SatellitePassResults<ServiceCartographicWithTime>>(rawResponse, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            });

            // object graph verification of actual results with expected results
            var expected = JsonConvert.DeserializeObject<SatellitePassResults<ServiceCartographicWithTime>>(TestHelper.SatelliteAccessPassData);
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
            
            Assert.That(!string.IsNullOrEmpty(passResults.CzmlForPasses));
            // Head to cesiumjs.org, click the "Tap to Interact" button. Once the globe appears, Drag the file produced 
            // in the next line to the globe.  Use the globe controls to move forward and backward in time, and move the 
            // camera to see the orbit, and highlighted visibility times.
            File.WriteAllText("ISS.czml", passResults.CzmlForPasses);
        }
    }
}