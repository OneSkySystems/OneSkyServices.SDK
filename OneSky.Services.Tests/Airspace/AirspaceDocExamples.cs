using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Airspace;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Airspace;
using OneSky.Services.Services.Airspace;
using System;
using System.Collections.Generic;


namespace OneSky.Services.Tests.Airspace
{
    [TestFixture]
    public class AirspaceDocExamples
    {
        [Test]
        public void TestFindAllAirspacesWithin1Km()
        {
            var request = new AirspaceSelectionOptions
            {
                Categories = new List<AirspaceCategory>
                {
                    AirspaceCategory.ControlledAirspace,
                    AirspaceCategory.Airport,
                    AirspaceCategory.Restricted,
                    AirspaceCategory.SpecialUseArea,
                    AirspaceCategory.Parks
                },
                RegionCenter = new ServiceCartographic(38.890903, -77.036035, 0),
                UseRegionalAirspaceQuery = true,
                RegionRadius = 1000

            };
            var results = AirspaceServices.SelectAirspaces(request).Result;
            var expected = JsonConvert.DeserializeObject<AirspaceIdResult>(TestHelper.AirspaceSelectAirspacesDocExample);
            results.Should().BeEquivalentTo(expected);
        }
    }
}
