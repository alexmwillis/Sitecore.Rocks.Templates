using System.Collections.Generic;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Tags
{
    [TestFixture]
    public class IfEqualTagTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestOutputIfStringsMatch()
        {
            var expectedResult = "if";

            var template = "{{#ifEqual Context 'sameValue'}}if{{elseEqual}}else{{/ifEqual}}";

            Assert.That(new TemplateEngine().Render(template, new {Context = "sameValue" }),
                Is.EqualTo(expectedResult));

            template = "{{#ifEqual 'sameValue' Context}}if{{#elseEqual}}else{{/ifEqual}}";

            Assert.That(new TemplateEngine().Render(template, new {Context = "sameValue" }),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestNotOutputIfStringsDontMatch()
        {
            var expectedResult = "else";

            var template = "{{#ifEqual Context 'value2'}}if{{#elseEqual}}else{{/ifEqual}}";

            Assert.That(new TemplateEngine().Render(template, new {Context = "value1"}),
                Is.EqualTo(expectedResult));

            template = "{{#ifEqual 'value1' Context}}if{{#elseEqual}}else{{/ifEqual}}";

            Assert.That(new TemplateEngine().Render(template, new {Context = "value2"}),
                Is.EqualTo(expectedResult));
        }
    }
}
