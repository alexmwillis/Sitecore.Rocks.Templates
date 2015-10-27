using System.IO;
using System.Linq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests.Templates
{
    [TestFixture]
    public class SinjTemplateTests
    {
        private SitecoreTemplate _template;

        [SetUp]
        public void SetUp()
        {
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
                                Type = "scFieldTypes.text",
                                SortOrder = "100"
                            },
                            new SitecoreTemplateField
                            {
                                Id = "{C59E3208-89A0-467A-B066-721EA75F8D34}",
                                Name = "Field 2",
                                Type = "scFieldTypes.text",
                                SortOrder = "200"
                            }
                        }.ToList()
                    },
                    new SitecoreTemplateSection
                    {
                        Id = "{25004c23-1056-420b-a97d-7e95241dfcf5}",
                        Name = "Section 2"
                    }
                }.ToList(),
                StandardValues = new SitecoreItem
                {
                    Id = "{EB500643-2FA7-4F83-BCBA-4E623D1946F4}",
                    Fields = new[]
                    {
                        new SitecoreField {Name = "Field 1", Value = "Value 1"},
                        new SitecoreField {Name = "Field 2", Value = "Value 2"}
                    }.ToList()
                }
            };
        }

        [Test]
        public void TemplateFormatedCorrectly()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Template Templates//Sinj.hbs");

            var expectedResult = File.ReadAllText("..//..//Resources//sinj-template.js");

            Assert.That(new TemplateEngine().Render(template, _template),
                Is.EqualTo(expectedResult));
        }
    }
}
