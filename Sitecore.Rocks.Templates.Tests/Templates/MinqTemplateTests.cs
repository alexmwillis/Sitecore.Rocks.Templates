using System;
using System.IO;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Engine;
using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Tests.Templates
{
    [TestFixture]
    public class MinqTemplateTests:BaseTemplateTest
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
                            new SitecoreTemplateField {Name = "Field 1", Type = "Single-Line Text"},
                            new SitecoreTemplateField {Name = "Field 2", Type = "Rich Text"},
                            new SitecoreTemplateField {Name = "Field 3", Type = "Checkbox"}
                        }
                    },
                    new SitecoreTemplateSection
                    {
                        Fields = new[]
                        {
                            new SitecoreTemplateField {Name = "Field 4", Type = "Number"},
                            new SitecoreTemplateField {Name = "Field 5", Type = "General Link"},
                            new SitecoreTemplateField {Name = "Field 6", Type = "Image"}
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
public class TemplateModel : SitecoreItemModel
{{
    [SitecoreField(""Field 1"")]
    public string Field1 {{ get; set; }}

    [SitecoreField(""Field 2"")]
    public string Field2 {{ get; set; }}

    [SitecoreField(""Field 3"")]
    public bool Field3 {{ get; set; }}

    [SitecoreField(""Field 4"")]
    public int Field4 {{ get; set; }}

    [SitecoreField(""Field 5"")]
    public SLink Field5 {{ get; set; }}

    [SitecoreField(""Field 6"")]
    public SMedia Field6 {{ get; set; }}
}}
";
            var template = new TemplateMetaData
            {
                FullName = "..//..//..//Sitecore.Rocks.Templates//Resources//Template Templates//minq.hbs"
            };

            AssertThatTemplatesMatch(
                new TemplateEngineService().Render(template, _template),
                expectedResult);

            AssertThatTemplatesMatch(
                new TemplateEngineService().Render(template, _template),
                expectedResult);
        }
    }
}
