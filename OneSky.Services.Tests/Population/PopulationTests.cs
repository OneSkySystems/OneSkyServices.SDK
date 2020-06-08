using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Population;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Outputs.Population;
using OneSky.Services.Services.Lighting;
using System.Collections.Generic;

namespace OneSky.Services.Tests.Population
{
    [TestFixture]
    public class PopulationTests
    {        
        [Test]
        public void PopulationCountAtASite()
        {
            var sitePopData = new PointPopulationData
            {
                Path = new SiteData
                {
                    Location = new ServiceCartographic(44.0,-104.77,0)
                },
                PointFlightRadius = 800,
                PopulationType = PopulationDataType.Count
            };
            var popResults = PopulationServices.GetPopulationAtASite(sitePopData).Result;
            var expected = JsonConvert.DeserializeObject<PopulationResults>(TestHelper.PopulationSiteCount);
            popResults.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()                
            );
        }

        [Test]
        public void PopulationDensityAlongARoute()
        {
            var routePopData = new RoutePopulationData<IVerifiable>()
            {
                PopulationType = PopulationDataType.Density
            };
            var routePath = new PointToPointRouteData()
            {
                Waypoints = new List<ServiceCartographicWithTime>(),
                IncludeWaypointsInRoute = true,
                OutputSettings = new OutputSettings()
                {
                    Step = 60,
                    TimeFormat = TimeRepresentation.Epoch,
                    CoordinateFormat = new CoordinateType()
                    {
                        Coord = CoordinateRepresentation.LLA
                    }
                }
            };
            routePath.Waypoints.Add(new ServiceCartographicWithTime()
            {
                Position = new ServiceCartographic(39.07096, -104.78509, 2000.0),
                Time = "2014-03-25T18:30:00Z"
            });
            routePath.Waypoints.Add(new ServiceCartographicWithTime()
            {
                Position = new ServiceCartographic(38.1,-104.785,1600.0),
                Time = "2014-03-25T20:30:00Z"
            });

            routePopData.Path = routePath;

            var routePopResults = PopulationServices.GetPopulationAlongARoute(routePopData).Result;
            var expected = JsonConvert.DeserializeObject<PopulationResults>(TestHelper.PopulationRouteDensity);
            routePopResults.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
            );
        }

        [Test]
        public void PopulationDensityAlongARoute30SecStep()
        {
            var routePopData = new RoutePopulationData<IVerifiable>()
            {
                PopulationType = PopulationDataType.Density
            };
            var routePath = new PointToPointRouteData()
            {
                Waypoints = new List<ServiceCartographicWithTime>(),
                IncludeWaypointsInRoute = true,
                OutputSettings = new OutputSettings()
                {
                    Step = 30,
                    TimeFormat = TimeRepresentation.Epoch,
                    CoordinateFormat = new CoordinateType()
                    {
                        Coord = CoordinateRepresentation.LLA
                    }
                }
            };
            routePath.Waypoints.Add(new ServiceCartographicWithTime()
            {
                Position = new ServiceCartographic(39.07096, -104.78509, 2000.0),
                Time = "2014-03-25T18:30:00Z"
            });
            routePath.Waypoints.Add(new ServiceCartographicWithTime()
            {
                Position = new ServiceCartographic(38.1, -104.785, 1600.0),
                Time = "2014-03-25T20:30:00Z"
            });

            routePopData.Path = routePath;

            var routePopResults = PopulationServices.GetPopulationAlongARoute(routePopData).Result;
            var expected = JsonConvert.DeserializeObject<PopulationResults>(TestHelper.PopulationRouteDensity30SecStep);
            routePopResults.Should().BeEquivalentTo(expected, options => options
                .Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, TestHelper.PrecisionDouble))
                .WhenTypeIs<double>()
            );
        }
    }
}




