using System;
using System.IO;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests
{
    [TestFixture]
    public class SinjSubItemTests
    {
        private SitecoreItem _itemWithSubItems;
        private SitecoreItem _subItem1;
        private SitecoreItem _subItem2;

        [SetUp]
        public void SetUp()
        {
            var itemId = Guid.NewGuid().ToString();

            _subItem1 = new SitecoreItem
            {
                Id = itemId,
                Name = "Sub Item 1",
                ParentPath = "/item/path/",
                Language = "en",
                TemplatePath = "/template/path/",
                TemplateName = "Template",
                Fields = new[]
                {
                    new SitecoreField {Name = "Field Name 1", Value = "Field Value 1"},
                    new SitecoreField {Name = "Field Name 2", Value = "Field Value 2"}
                }
            };

            _subItem2 = new SitecoreItem
            {
                Id = itemId,
                Name = "Sub Item 2",
                ParentPath = "/item/path/",
                Language = "en",
                TemplatePath = "/template/path/",
                TemplateName = "Template",
                Fields = new[]
                {
                    new SitecoreField {Name = "Field Name 1", Value = "Field Value 1"},
                    new SitecoreField {Name = "Field Name 2", Value = "Field Value 2"}
                }
            };

            _itemWithSubItems = new SitecoreItem
            {
                Children = new[] { _subItem1, _subItem2 }
            };
        }

        [Test]
        public void TestSubItemsAreCorrectlyFormatted()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//sinj-subitems.hbs");

            var expectedResult = $@"var subItem2Template: SinjItemDto = {{ 
    id: ""{_subItem1.Id}"",
    name: ""{_subItem1.Name}"",
    template: ""{_subItem1.TemplatePath}"",
    parent: ""{_subItem1.ParentPath}"",
    language: ""{_subItem1.Language}"",
    fields: {{
    }}
}}

var subItem2Template: SinjItemDto = {{ 
    id: ""{_subItem2.Id}"",
    name: ""{_subItem2.Name}"",
    template: ""{_subItem2.TemplatePath}"",
    parent: ""{_subItem2.ParentPath}"",
    language: ""{_subItem2.Language}"",
    fields: {{
    }}
}}";
            Assert.That(new TemplateEngine().Render(template, _itemWithSubItems),
                Is.EqualTo(expectedResult));
        }
    }
}
