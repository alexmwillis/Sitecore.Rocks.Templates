using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Tags
{
    [TestFixture]
    public class NotFirstTests
    {
        private KeyValuePair<string, string>[] _collection;

        [SetUp]
        public void SetUp() 
        {
            _collection = new[]
            {
                new KeyValuePair<string,string>("Key 1","Value 1"),
                new KeyValuePair<string,string>("Key 2","Value 2"),
                new KeyValuePair<string,string>("Key 3","Value 3")            
            };
        }

        [Test]
        public void TestStringNotOutputOnLastItem()
        {
            var expectedResult = "N:Key 1 V:Value 1, N:Key 2 V:Value 2, N:Key 3 V:Value 3";

            var template = "{{#each this}}{{#notFirst ', '}}N:{{Key}} V:{{Value}}{{/each}}";
            
            Assert.That(new TemplateEngine().Render(template, _collection),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestNoErrorThrowIfNoCollection()
        {
            var template = "{{#notFirst 'no collection'}}";

            Assert.That(new TemplateEngine().Render(template, null),
                Is.EqualTo(string.Empty));
        }
    }
}
