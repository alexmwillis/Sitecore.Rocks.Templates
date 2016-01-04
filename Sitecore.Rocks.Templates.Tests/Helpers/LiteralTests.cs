using NUnit.Framework;

namespace Sitecore.Rocks.Templates.Tests.Helpers
{
    [TestFixture]
    public class LiteralTests
    {
        [SetUp]
        public void SetUp()
        {
            FSharp.Helpers.Init();
        }

        [TestCase("<div>\''\\</div>", @"<div>\'\'\\</div>")]
        public void Test(string actualString, string expectedLiteral)
        {
            var template = "{{literal this}}";

            Assert.That(FSharp.TemplateEngine.Compile(template)(actualString),
                Is.EqualTo(expectedLiteral));
        }
    }
}
