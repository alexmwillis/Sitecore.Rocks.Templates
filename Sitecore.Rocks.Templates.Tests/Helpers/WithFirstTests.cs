using NUnit.Framework;

namespace Sitecore.Rocks.Templates.Tests.Helpers
{
    [TestFixture]
    public class WithFirstTests
    {
        [Test]
        public void GivenWhereTagFiltersEmptyValuesThenReturnsOnlyNonEmpty()
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
    }
}
