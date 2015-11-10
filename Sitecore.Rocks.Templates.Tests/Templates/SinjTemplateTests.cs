using System.IO;
using System.Linq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Engine;
using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Tests.Templates
{
    [TestFixture]
    public class SinjTemplateTests
    {
        private SitecoreTemplate _template;
        private SitecoreTemplate _templateNoSectionsNorStandardValues;

        [SetUp]
        public void SetUp()
        {
            _templateNoSectionsNorStandardValues = new SitecoreTemplate
            {
                Id = "{A2956C5B-41C0-4719-A0DA-9F33504E34AC}",
                Name = "Template",
                Icon = "icon",
                ParentPath = "parentPath"
            };

            _template = new SitecoreTemplate
            {
                Id = "{A2956C5B-41C0-4719-A0DA-9F33504E34AC}",
                Name = "Template",
                Icon = "icon",
                ParentPath = "parentPath",
                Sections = new[]
                {
                    new SitecoreTemplateSection
                    {
                        Id = "{0A4ED1F8-BC5C-4761-B712-F5D109BF84D1}",
                        Name = "Section 1",
                        Icon = "icon",
                        Fields = new[]
                        {
                            new SitecoreTemplateField
                            {
                                Id = "{A6D93073-6EB4-4F43-A117-6CE1DA444371}",
                                Name = "Field 1",
                                Type = "Rich Text",
                                SortOrder = "100",
                                Fields= new []
                                {
                                    new SitecoreField { Name = "__Other Field", Value = "Value" }
                                }
                            },
                            new SitecoreTemplateField
                            {
                                Id = "{C59E3208-89A0-467A-B066-721EA75F8D34}",
                                Name = "Field 2",
                                Type = "Rich Text",
                                SortOrder = "200",
                                Fields= new []
                                {
                                    new SitecoreField { Name = "__Other Field", Value = "Value" }
                                }
                            }
                        }
                    },
                    new SitecoreTemplateSection
                    {
                        Id = "{25004c23-1056-420b-a97d-7e95241dfcf5}",
                        Name = "Section 2"
                    }
                },
                StandardValues = new SitecoreItem
                {
                    Id = "{EB500643-2FA7-4F83-BCBA-4E623D1946F4}",
                    Fields = new[]
                    {
                        new SitecoreField {Name = "Field 1", Value = "Value 1"},
                        new SitecoreField {Name = "Field 2", Value = "Value 2"}
                    }
                }
            };
        }

        [Test]
        public void TemplateFormatedCorrectly()
        {
            var template = new TemplateMetaData
            {
                FullName = "..//..//..//Sitecore.Rocks.Templates//Resources//Template Templates//copy-sinj.hbs"
            };

            var expectedResult = File.ReadAllText("..//..//Resources//sinj-template.js");

            Assert.That(new TemplateEngineService().Render(template, _template),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TemplateWithNoSectionsNorStandardValuesFormatedCorrectly()
        {
            var template = new TemplateMetaData
            {
                FullName = "..//..//..//Sitecore.Rocks.Templates//Resources//Template Templates//copy-sinj.hbs"
            };

            var expectedResult = File.ReadAllText("..//..//Resources//sinj-template-no-sections.js");

            Assert.That(new TemplateEngineService().Render(template, _templateNoSectionsNorStandardValues),
                Is.EqualTo(expectedResult));
        }
    }
}
