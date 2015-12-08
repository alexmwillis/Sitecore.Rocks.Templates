using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Engine;
using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Tests.Templates
{
    [TestFixture]
    public class SinjTests: BaseTemplateTest
    {
        private SitecoreItem _itemWithFields;
        private SitecoreItem _itemWithNoFields;

        [SetUp]
        public void SetUp()
        {
            FSharp.Helpers.Init();

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
                }.ToList()
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
            var expectedResult = $@"var itemWithNoFieldsTemplate: SinjItemDto = {{ 
    id: ""{_itemWithNoFields.Id}"",
    name: ""{_itemWithNoFields.Name}"",
    template: ""{_itemWithNoFields.TemplatePath}"",
    parent: ""{_itemWithNoFields.ParentPath}"",
    language: ""{_itemWithNoFields.Language}"",
    fields: {{
    }}
}}";
            var template = new TemplateMetaData
            {
                FullName = "..//..//..//Sitecore.Rocks.Templates//Resources//Item Templates//Sinj.hbs"
            };

            AssertThatTemplatesMatch(
                new TemplateEngineService().Render(template, _itemWithNoFields),
                expectedResult);
        }

        [Test]
        public void TestItemWithFieldsCorrectlyFormatted()
        {
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
            var template = new TemplateMetaData
            {
                FullName = "..//..//..//Sitecore.Rocks.Templates//Resources//Item Templates//Sinj.hbs"
            };

            AssertThatTemplatesMatch(
                new TemplateEngineService().Render(template, _itemWithFields),
                expectedResult);
        }
    }
}
