using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Population;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services.Lighting;

namespace OneSky.Services.Tests.Validation.Population
{
    [TestFixture]
    public class PopulationDocExamples
    {
        
        [Test]
        public void PopulationDensity_AtASite()
        {
            var sitePopData = new PointPopulationData
            {
                Path = new SiteLocationData
                {
                    Location = new ServiceCartographic(44.0,-104.77,0)
                },
                PointFlightRadius = 800,
                PopulationType = PopulationDataType.Density
            };
            var popResults = PopulationServices.GetPopulationAtASite(sitePopData).Result;
            Assert.AreEqual(0.65041154623031616,popResults.Mean); // value is # of people per km^2 (pop density)
            // Mean and weighted mean should be equal in this case
            Assert.AreEqual(popResults.Mean,popResults.WeightedMean);
            Assert.AreEqual(1,popResults.PopulationValues.Count);
            Assert.AreEqual(0.65041154623031616,popResults.PopulationValues[0]);
            Assert.AreEqual(1,popResults.Weights.Count);
            Assert.AreEqual(1,popResults.Weights[0]);
            Assert.AreEqual(1,popResults.SumOfWeights);
        }
        [Test]
        public void PopulationDensity_AlongARoute()
        {
            var routePopData = new RoutePopulationData<IVerifiable>()
            {
                PopulationType = PopulationDataType.Count
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
                Position = new ServiceCartographic(39.07096,-104.78509,2000.0),
                Time = DateTimeOffset.Parse("2014-03-25T18:30:00Z")
            });
            routePath.Waypoints.Add(new ServiceCartographicWithTime()
            {
                Position = new ServiceCartographic(38.1,-104.785,1600.0),
                Time = DateTimeOffset.Parse("2014-03-25T20:30:00Z")
            });

            routePopData.Path = routePath;

            var routePopResults = PopulationServices.GetPopulationAlongARoute(routePopData).Result;
            Assert.AreEqual(8088.3678982750444,routePopResults.WeightedMean); // value is # of people along the route (pop count)
            Assert.AreEqual(7862.6120876669884,routePopResults.Mean);
            Assert.AreEqual(121,routePopResults.SumOfWeights);
            Assert.AreEqual(24,routePopResults.Weights.Count);
            Assert.AreEqual(routePopResults.Weights.Count,routePopResults.PopulationValues.Count);
            Assert.AreEqual(1581.945556640625,routePopResults.PopulationValues[0]);
            Assert.AreEqual(41.257167816162109,routePopResults.PopulationValues[routePopResults.PopulationValues.Count-1]);
            Assert.AreEqual(4,routePopResults.Weights[0]);
            Assert.AreEqual(4,routePopResults.Weights[routePopResults.Weights.Count-1]);

        }
    }
}




