using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Tags
{
    [TestFixture]
    public class WhereTagTests
    {
        
        [Test]
        public void GivenInvalidWhereTagThenReturnsEmpty()
        {
            var collection = new[]
            {
                new {Key = "Key 1", Value = "Value 1"}
            };

            var expectedResult = string.Empty;

            var template = "{{#where this 'Invalid'}}{{#each this}}N:{{Key}} V:{{Value}} {{/each}}{{/where}}";
            
            Assert.That(new TemplateEngine().Render(template, collection),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void GivenWhereTagFiltersEmptyValuesThenReturnsOnlyNonEmpty()
        {
            var collection = new[]
            {
                new {Key = "Key 1", Value = "Value 1"},
                new {Key = "Key 2", Value = "Value 2"},
                new {Key = "Key 3", Value = ""}
            };

            var expectedResult = "N:Key 1 V:Value 1 N:Key 2 V:Value 2 ";

            var template = "{{#where this 'Value'}}{{#each this}}N:{{Key}} V:{{Value}} {{/each}}{{/where}}";
            
            Assert.That(new TemplateEngine().Render(template, collection),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void GivenWhereTagMatches2RdKeyThenReturnsOnly3RdKeyReturned()
        {
            var collection = new[]
            {
                new {Key = "Key 1", Value = "Value 1"},
                new {Key = "Key 2", Value = "Value 2"},
                new {Key = "Key 3", Value = "Value 3"}
            };

            var expectedResult = "N:Key 2 V:Value 2 ";

            var template = "{{#where this 'Key' '2$'}}{{#each this}}N:{{Key}} V:{{Value}} {{/each}}{{/where}}";

            Assert.That(new TemplateEngine().Render(template, collection),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestWhereValueTruePrinted()
        {
            var collection = new[]
            {
                new {Key = "Key 1", Value = true},
                new {Key = "Key 2", Value = false},
                new {Key = "Key 3", Value = true},
                new {Key = "Key 4", Value = false}
            };

            var expectedResult = "N:Key 1 N:Key 3 ";

            var template = "{{#where this 'Value' 'true'}}{{#each this}}N:{{Key}} {{/each}}{{/where}}";

            Assert.That(new TemplateEngine().Render(template, collection),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestWhereValueFalsePrinted()
        {
            var collection = new[]
            {
                new {Key = "Key 1", Value = true},
                new {Key = "Key 2", Value = false},
                new {Key = "Key 3", Value = true},
                new {Key = "Key 4", Value = false}
            };

            var expectedResult = "N:Key 2 N:Key 4 ";

            var template = "{{#where this 'Value' 'false'}}{{#each this}}N:{{Key}} {{/each}}{{/where}}";

            Assert.That(new TemplateEngine().Render(template, collection),
                Is.EqualTo(expectedResult));
        }
    }
}
