using NUnit.Framework;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Helpers
{
    [TestFixture]
    public class HandleBarsTest
    {
        [Test]
        public void Test()
        {
            string source =
@"<div class=""entry"">
  <h1>{{title}}</h1>
  <div class=""body"">
    {{body}}
  </div>
</div>";
            string expected =
                @"<div class=""entry"">
  <h1>My new post</h1>
  <div class=""body"">
    This is my first post!
  </div>
</div>";
            var data = new
            {
                title = "My new post",
                body = "This is my first post!"
            };

            var result = new TemplateEngine().Render(source, data);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
