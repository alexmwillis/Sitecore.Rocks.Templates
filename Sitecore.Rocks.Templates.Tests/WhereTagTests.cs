using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests
{
    [TestFixture]
    public class WhereTagTests
    {
        private KeyValuePair<string, string>[] _collection;

        [SetUp]
        public void SetUp() 
        {
            _collection = new[]
            {
                new KeyValuePair<string,string>("Key 1","Value 1"),
                new KeyValuePair<string,string>("Key 2","Value 2"),
                new KeyValuePair<string,string>("Key 3","")            
            };
        }

        [Test]
        public void GivenInvalidWhereTagThenReturnsEmpty()
        {
            var expectedResult = string.Empty;

            var template = File.ReadAllText("..//..//Resources//Where-Invalid.hbs");
            
            Assert.That(new TemplateEngine().Render(template, _collection),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void GivenWhereTagFiltersEmptyValuesThenReturnsOnlyNonEmpty()
        {
            var expectedResult = "N:Key 1 V:Value 1 N:Key 2 V:Value 2 ";

            var template = File.ReadAllText("..//..//Resources//Where-Value.hbs");

            Assert.That(new TemplateEngine().Render(template, _collection),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void GivenWhereTagMatches3RdKeyThenReturnsOnly3RdKeyReturned()
        {
            var expectedResult = "N:Key 2 V:Value 2 ";

            var template = File.ReadAllText("..//..//Resources//Where-Match-Key.hbs");

            Assert.That(new TemplateEngine().Render(template, _collection),
                Is.EqualTo(expectedResult));
        }
    }
}
