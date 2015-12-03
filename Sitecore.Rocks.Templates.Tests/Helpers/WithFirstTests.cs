using System.Linq;
using NUnit.Framework;

namespace Sitecore.Rocks.Templates.Tests.Helpers
{
    [TestFixture]
    public class WithFirstTests
    {
        [SetUp]
        public void SetUp()
        {
            FSharp.Helpers.Init();
        }

        [Test]
        public void GivenCollectionFirstItemIsOutput()
        {
            var collection = new[]
            {
                new {Key = "Key 1", Value = "Value 1"},
                new {Key = "Key 2", Value = "Value 2"},
                new {Key = "Key 3", Value = "Value 3"},
            };

            var expectedResult = "N:Key 1 V:Value 1";

            var template = "{{#withFirst this}}N:{{Key}} V:{{Value}}{{/withFirst}}";

            Assert.That(FSharp.TemplateEngine.Compile(template)(collection),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void GivenEmptyCollectionNothingIsOutput()
        {
            var template = "{{#withFirst this}}N:{{Key}} V:{{Value}}{{/withFirst}}";

            Assert.That(FSharp.TemplateEngine.Compile(template)(Enumerable.Empty<object>()),
                Is.EqualTo(string.Empty));
        }

        [Test]
        public void GivenNullSourceNothingIsOutput()
        {
            var template = "{{#withFirst this}}N:{{Key}} V:{{Value}}{{/withFirst}}";

            Assert.That(FSharp.TemplateEngine.Compile(template)(null),
                Is.EqualTo(string.Empty));
        }
    }
}
