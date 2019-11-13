using System;
using NUnit.Framework;
using OneSky.Services.Inputs;
using OneSky.Services.Inputs.Access;
using OneSky.Services.Inputs.Routing;
using OneSky.Services.Services.Overflight;
using OneSky.Services.Services.Terrain;

namespace OneSky.Services.Tests.Validation.Overflight
{
    [TestFixture]
    public class RegionalOverflightDocExamples
    {
        [Test]
        public void IssForThirtyMinutes()
        {
            var overflightInput = new OverflightAccessData<IVerifiable> {IncludePath = true};
            var path = new Sgp4RouteData
            {
                Start = DateTimeOffset.Parse("2014-02-10T00:00:00Z"),
                Stop = DateTimeOffset.Parse("2014-02-10T00:30:00Z"),
                SSC = 25544,
                OutputSettings =
                {
                    Step = 300,
                    TimeFormat = TimeRepresentation.UTC,
                    CoordinateFormat = {Coord = CoordinateRepresentation.XYZ, Frame = FrameRepresentation.Fixed}
                }
            };
            overflightInput.Path = path;

            var result = OverflightServices.GetRegionalOverflight<ServiceCartesianWithTime>(overflightInput).Result;

            Assert.That(result != null);
            Assert.AreEqual(15, result.Count);
            Assert.AreEqual("USA",result[0].CountryId);
            Assert.AreEqual("United States", result[0].Name);
            Assert.AreEqual(451211.62334118644, result[0].Entry.Position.X);
            Assert.AreEqual(-5040730.1564817084, result[0].Entry.Position.Y);
            Assert.AreEqual(4522633.5835356982, result[0].Entry.Position.Z);
            Assert.AreEqual("2014-02-10T00:00:00", result[0].Entry.Time.ToString("s"));
            Assert.AreEqual(1, result[0].Path.Count);

            Assert.AreEqual("ETH", result[14].CountryId);
            Assert.AreEqual("Ethiopia", result[14].Name);
            Assert.AreEqual(5250413.7026270721, result[14].Exit.Position.X);
            Assert.AreEqual(4282717.8522880469, result[14].Exit.Position.Y);
            Assert.AreEqual(485537.05931469012, result[14].Exit.Position.Z);
            Assert.AreEqual("2014-02-10T00:30:00", result[14].Exit.Time.ToString("s"));
            Assert.AreEqual(1, result[14].Path.Count);
        }
    }
}




