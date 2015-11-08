using NUnit.Framework;
using Sitecore.Rocks.Templates.Utils;

namespace Sitecore.Rocks.Templates.Tests.Utils
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("Pascal Case")]
        [TestCase("Pascal  Case")]
        [TestCase("pascal Case")]
        [TestCase("Pascal  case")]
        public void TestPascalCase(string stringToFormat)
        {
            var expectedResult = "PascalCase";

            Assert.That(stringToFormat.PascalCase(), Is.EqualTo(expectedResult));
        }

        [TestCase("1 Test")]
        [TestCase("1Test")]
        [TestCase("2Test")]
        [TestCase("10Test")]
        public void TestSanitiseNumbers(string stringToFormat)
        {
            Assert.That(stringToFormat.PascalCase(), Is.EqualTo("Test"));
        }

        [TestCase("Test-Case")]
        [TestCase("Test_Case")]
        [TestCase("Test - Case")]
        public void TestSanitiseSpecialCharacters(string stringToFormat)
        {
            Assert.That(stringToFormat.PascalCase(), Is.EqualTo("TestCase"));
        }

        [TestCase("Camel Case")]
        [TestCase("Camel  Case")]
        [TestCase("camel Case")]
        [TestCase("Camel  case")]
        [TestCase("1 Camel  case")]
        [TestCase("2Camel  case")]
        public void TestCamelCase(string stringToFormat)
        {
            var expectedResult = "camelCase";

            Assert.That(stringToFormat.CamelCase(), Is.EqualTo(expectedResult));
        }

        [TestCase("\t\n\r", @"\t\n\r")]
        [TestCase("\''\\", @"\'\'\\")]
        [TestCase(@"
", @"\r\n")]
        [TestCase("\b\v\f", @"\b\v\f")]
        public void TestToLiteral(string actualString, string expectedLiteral)
        {
            Assert.That(actualString.ToLiteral(), Is.EqualTo(expectedLiteral));
        }
    }
}
