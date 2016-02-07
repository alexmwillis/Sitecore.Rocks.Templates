using NUnit.Framework;
using Sitecore.Rocks.Templates.Extensions;

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

            Assert.That(ToPascalCase(stringToFormat), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestPascalCaseEmpty()
        {
            Assert.That(ToPascalCase(string.Empty), Is.EqualTo(string.Empty));
        }
        
        [TestCase("1 Test")]
        [TestCase("1Test")]
        [TestCase("2Test")]
        [TestCase("10Test")]
        public void TestSanitiseNumbers(string stringToFormat)
        {
            Assert.That(ToPascalCase(stringToFormat), Is.EqualTo("Test"));
        }

        [TestCase("Test-Case")]
        [TestCase("Test_Case")]
        [TestCase("Test - Case")]
        public void TestSanitiseSpecialCharacters(string stringToFormat)
        {
            Assert.That(ToPascalCase(stringToFormat), Is.EqualTo("TestCase"));
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

            Assert.That(ToCamelCase(stringToFormat), Is.EqualTo(expectedResult));
        }

        [TestCase("\t\n\r", @"\t\n\r")]
        [TestCase("\''\\", @"\'\'\\")]
        [TestCase(@"
", @"\r\n")]
        [TestCase("\b\v\f", @"\b\v\f")]
        [TestCase("\"\'\"", "\\\"\\\'\\\"")]
        [TestCase("", "")]
        [TestCase("a", "a")]
        public void TestToLiteral(string actualString, string expectedLiteral)
        {
            Assert.That(ToLiteral(actualString), Is.EqualTo(expectedLiteral));
        }

        private static string ToPascalCase(string str)
        {
            return FSharp.StringFunctions.ToPascalCase.Invoke(str);
        }

        private static string ToCamelCase(string str)
        {
            return FSharp.StringFunctions.ToCamelCase.Invoke(str);
        }

        private static string ToLiteral(string str)
        {
            return FSharp.StringFunctions.ToLiteral(str);
        }
    }
}
