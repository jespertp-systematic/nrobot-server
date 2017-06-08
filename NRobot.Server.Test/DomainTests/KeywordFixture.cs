using System.Collections.Generic;
using NRobot.Server.Imp.Domain;

namespace NRobot.Server.Test.DomainTests
{
    using NUnit.Framework;

    [TestFixture]
    public class KeywordFixture
    {
        [Test]
        public void GenerateFriendlyNameFromCamelCase()
        {
            var names = new Dictionary<string, string>
            {
                {"SimpleCamelCase", "Simple Camel Case" },
                {"WithMultipleCAPitals", "With Multiple CA Pitals" },
                { "SimpleCamel_Cased_withUnderscores", "Simple Camel Cased with Underscores" },
                { "The_first_keyword", "The first keyword" }
            };
            foreach (var pair in names)
            {
                var keyword = new Keyword();
                var expected = pair.Value;
                var actual = keyword.GenerateFriendlyName(pair.Key);
                Assert.That(actual, Is.EqualTo(expected), "{0} should be {1} and not {2}", pair.Key, expected, actual);
            }
        }
    }
}