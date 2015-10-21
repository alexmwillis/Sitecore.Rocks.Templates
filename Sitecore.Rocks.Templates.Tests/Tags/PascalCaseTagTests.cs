using System.IO;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Tags
{
    [TestFixture]
    public class PascalCaseTagTests
    {
        [TestCase("Pascal Case")]
        [TestCase("Pascal  Case")]
        [TestCase("pascal Case")]
        [TestCase("Pascal  case")]
        public void GivenWordsThenReturnsPascalCase(string stringToFormat)
        {
            var expectedResult = "PascalCase";

            Assert.That(new TemplateEngine().Render("{{#pascalCase this}}", stringToFormat),
                Is.EqualTo(expectedResult));
        }

        [TestCase("1 Test")]
        [TestCase("1Test")]
        [TestCase("2Test")]
        [TestCase("10Test")]
        public void TestSanitiseNumbers(string stringToFormat)
        {
            Assert.That(new TemplateEngine().Render("{{#pascalCase this}}", stringToFormat),
                Is.EqualTo("Test"));
        }
    }
}
