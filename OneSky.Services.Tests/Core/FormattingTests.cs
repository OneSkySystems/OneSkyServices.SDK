using FluentAssertions;
using NUnit.Framework;
using OneSky.Services.Util;
using System;

namespace OneSky.Services.Tests.Core
{
    [TestFixture]
    public class FormattingTests
    {
        [Test]
        public void TestXmlOutput()
        {                
            var uri = Networking.GetFullUri(ServiceUris.NavigationGpsOutagesUri,Format.Xml);
            var result = Networking.HttpGetCall(Networking.AppendDateTimeAndPrnToUri(uri,DateTime.Parse("2018-01-01"),DateTime.Parse("2018-11-01"), 18)).Result;
            result.Should().BeEquivalentTo(TestHelper.FormattingXmlExample);
        }
    }
}
