using NUnit.Framework;

namespace Sitecore.Rocks.Templates.Tests.Helpers
{
    [TestFixture]
    public class EqualTests
    {
        //[SetUp]
        //public void SetUp()
        //{
        //    FSharp.Helpers.Init();
        //}

        [Test]
        public void TestOutputIfStringsMatch()
        {
            var expectedResult = "if";

            var template = "{{#equal Context 'sameValue'}}if{{else}}else{{/equal}}";

            Assert.That(FSharp.TemplateEngine.Compile(template)(new {Context = "sameValue" }),
                Is.EqualTo(expectedResult));

            template = "{{#equal 'sameValue' Context}}if{{else}}else{{/equal}}";

            Assert.That(FSharp.TemplateEngine.Compile(template)(new { Context = "sameValue" }),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestNotOutputIfStringsDontMatch()
        {
            var expectedResult = "else";

            var template = "{{#equal Context 'value2'}}if{{else}}else{{/equal}}";

            Assert.That(FSharp.TemplateEngine.Compile(template)(new { Context = "value1"}),
                Is.EqualTo(expectedResult));

            template = "{{#equal 'value1' Context}}if{{else}}else{{/equal}}";

            Assert.That(FSharp.TemplateEngine.Compile(template)(new { Context = "value2"}),
                Is.EqualTo(expectedResult));
        }
    }
}
