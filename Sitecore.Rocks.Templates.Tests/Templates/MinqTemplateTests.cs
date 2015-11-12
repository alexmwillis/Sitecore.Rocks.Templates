using System;
using System.IO;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Engine;
using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Tests.Templates
{
    [TestFixture]
    public class MinqTemplateTests
    {
        private SitecoreTemplate _template;

        [SetUp]
        public void SetUp()
        {
            var templateId = Guid.NewGuid().ToString();

            _template = new SitecoreTemplate
            {
                Id = templateId,
                Name = "Template",
                Sections = new[]
                {
                    new SitecoreTemplateSection
                    {
                        Fields = new[]
                        {
                            new SitecoreTemplateField {Name = "Field 1", Type = "Rich Text"},
                            new SitecoreTemplateField {Name = "Field 2", Type = "general link"}
                        }
                    },
                    new SitecoreTemplateSection
                    {
                        Fields = new[]
                        {
                            new SitecoreTemplateField {Name = "Field 3", Type = "Rich Text"},
                            new SitecoreTemplateField {Name = "Field 4", Type = "Rich Text"}
                        }
                    }
                }
            };
        }

        [Test]
        public void TestModelIsCorrectlyFormatted()
        {
            var expectedResult =
                $@"[SitecoreTemplate(""{_template.Id}"")]
public class ItemWithFieldsModel : SitecoreItemModel
{{
    [SitecoreField(""Field Name 1"")]
    public string FieldName1 {{ get; set; }}

    [SitecoreField(""Field Name 2"")]
    public string FieldName2 {{ get; set; }}
}}";
            var template = new TemplateMetaData
            {
                FullName = "..//..//..//Sitecore.Rocks.Templates//Resources//Template Templates//minq.hbs"
            };

            Assert.That(new TemplateEngineService().Render(template, _template),
                Is.EqualTo(expectedResult));

            Assert.That(new TemplateEngineService().Render(template, _template),
                Is.EqualTo(expectedResult));
        }
    }
}
