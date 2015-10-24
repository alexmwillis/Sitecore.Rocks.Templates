using System;
using System.IO;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Templates
{
    [TestFixture]
    public class SinjTests
    {
        private SitecoreItem _itemWithFields;
        private SitecoreItem _itemWithNoFields;

        [SetUp]
        public void SetUp()
        {
            var itemId = Guid.NewGuid().ToString();

            _itemWithFields = new SitecoreItem
            {
                Id = itemId,
                Name = "Item With Fields",
                ParentPath = "/item/path/",
                Language = "en",
                TemplatePath = "/template/path/",
                TemplateName = "Template",
                Fields = new[]
                {
                    new SitecoreField {Name = "Field Name 1", Value = "Field Value 1"},
                    new SitecoreField {Name = "Field Name 2", Value = "Field Value 2"},
                    new SitecoreField {Name = "Field Name 3", Value = ""}
                }
            };

            _itemWithNoFields = new SitecoreItem
            {
                Id = itemId,
                Name = "Item With No Fields",
                ParentPath = "/item/path/",
                Language = "en",
                TemplatePath = "/template/path/",
                TemplateName = "Template"
            };
        }

        [Test]
        public void TestNoFieldsItemIsCorrectlyFormatted()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Item Templates//Sinj.hbs");

            var expectedResult = $@"var itemWithNoFieldsTemplate: SinjItemDto = {{ 
    id: ""{_itemWithNoFields.Id}"",
    name: ""{_itemWithNoFields.Name}"",
    template: ""{_itemWithNoFields.TemplatePath}"",
    parent: ""{_itemWithNoFields.ParentPath}"",
    language: ""{_itemWithNoFields.Language}"",
    fields: {{
    }}
}}";
            Assert.That(new TemplateEngine().Render(template, _itemWithNoFields),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestItemWithNoFieldsCorrectlyFormatted()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Item Templates//Sinj.hbs");

            var expectedResult = $@"var itemWithFieldsTemplate: SinjItemDto = {{ 
    id: ""{_itemWithFields.Id}"",
    name: ""{_itemWithFields.Name}"",
    template: ""{_itemWithFields.TemplatePath}"",
    parent: ""{_itemWithFields.ParentPath}"",
    language: ""{_itemWithFields.Language}"",
    fields: {{
        ""Field Name 1"": ""Field Value 1"",
        ""Field Name 2"": ""Field Value 2""
    }}
}}";

            Assert.That(new TemplateEngine().Render(template, _itemWithFields),
                Is.EqualTo(expectedResult));
        }
    }
}
