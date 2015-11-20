using NUnit.Framework;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Tags
{
    [TestFixture]
    public class FirstTagTests
    {
        [Test]
        public void Test()
        {
            var template = "{{#each this}}{{#if @first}}{{#index}}{{/if}}{{/each}}";

            Assert.That(new TemplateEngine().Render(template, new[] { 1, 2, 3}),
                Is.EqualTo("1"));
        }
    }
}
