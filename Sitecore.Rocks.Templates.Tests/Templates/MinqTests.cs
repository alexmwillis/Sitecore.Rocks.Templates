﻿using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Templates
{
    [TestFixture]
    public class MinqTests
    {
        private SitecoreItem _itemWithFields;
        private SitecoreItem _itemWithNoFields;

        [SetUp]
        public void SetUp()
        {
            var templateId = Guid.NewGuid().ToString();

            _itemWithFields = new SitecoreItem
            {
                Name = "Item With Fields",
                TemplateId = templateId,
                Fields = new[]
                {
                    new SitecoreField {Name = "Field Name 1", Value = "Field Value 1"},
                    new SitecoreField {Name = "Field Name 2", Value = "Field Value 2"},
                }.ToList()
            };

            _itemWithNoFields = new SitecoreItem
            {
                Name = "Item With No Fields",
                TemplateId = templateId
            };
        }

        [Test]
        public void TestModelIsCorrectlyFormatted()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Item Templates//Minq.hbs");

            var expectedResult =
                $@"[SitecoreTemplate(""{_itemWithFields.TemplateId}"")]
public class ItemWithFieldsModel : SitecoreItemModel
{{
    [SitecoreField(""Field Name 1"")]
    public string FieldName1 {{ get; set; }}

    [SitecoreField(""Field Name 2"")]
    public string FieldName2 {{ get; set; }}
}}";

            Assert.That(new TemplateEngine().Render(template, _itemWithFields),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestNothingIsReturnedWhenTheItemHasNoFields()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Item Templates//Minq.hbs");
            
            Assert.That(new TemplateEngine().Render(template, _itemWithNoFields),
                Is.EqualTo(string.Empty));
        }
    }
}
