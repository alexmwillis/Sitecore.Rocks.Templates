using NUnit.Framework;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Tags
{
    [TestFixture]
    public class PartialTagTests
    {
        [Test]
        public void TestPartialRendered()
        {
            var engine = new TemplateEngine();
            
            var myPartial = "Hello {{Name}}";
            var template = "{{#partial 'myPartial' this}}";

            engine.RegisterPartial("myPartial", () => myPartial);

            Assert.That(engine.Render(template, new { Name = "World"}),
                Is.EqualTo("Hello World"));
        }
    }
}
