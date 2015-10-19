using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Data.Template;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests
{
    [TestFixture]
    public class TemplateSinjTests
    {
        private Mock<ISitecoreTemplate> _templateMock;

        [SetUp]
        public void SetUp()
        {
            var field1 = new Mock<ISitecoreTemplateField>();
            field1.Setup(f => f.Id).Returns("{A6D93073-6EB4-4F43-A117-6CE1DA444371}");
            field1.Setup(f => f.Name).Returns("Field 1");
            field1.Setup(f => f.Type).Returns("scFieldTypes.text");
            field1.Setup(f => f.SortOrder).Returns("100");

            var field2 = new Mock<ISitecoreTemplateField>();
            field2.Setup(f => f.Id).Returns("{C59E3208-89A0-467A-B066-721EA75F8D34}");
            field2.Setup(f => f.Name).Returns("Field 2");
            field2.Setup(f => f.Type).Returns("scFieldTypes.text");
            field2.Setup(f => f.SortOrder).Returns("200");

            var section1 = new Mock<ISitecoreTemplateSection>();
            section1.Setup(s => s.Id).Returns("{0A4ED1F8-BC5C-4761-B712-F5D109BF84D1}");
            section1.Setup(s => s.Name).Returns("Section 1");
            section1.Setup(s => s.Icon).Returns("icon");
            section1.Setup(s => s.Fields)
                .Returns(new[] 
                {
                    field1.Object, field2.Object
                });

            var section2 = new Mock<ISitecoreTemplateSection>();
            section2.Setup(s => s.Id).Returns("{25004c23-1056-420b-a97d-7e95241dfcf5}");
            section2.Setup(s => s.Name).Returns("Section 2");

            var standardValues = new Mock<ISitecoreItem>();
            standardValues.Setup(v => v.Id).Returns("{EB500643-2FA7-4F83-BCBA-4E623D1946F4}");
            //standardValues.Setup(v => v.Fields).Returns("{EB500643-2FA7-4F83-BCBA-4E623D1946F4}");

            _templateMock = new Mock<ISitecoreTemplate>();
            _templateMock.Setup(i => i.Name).Returns("Template");
            _templateMock.Setup(i => i.Icon).Returns("icon");
            _templateMock.Setup(i => i.Id).Returns("{A2956C5B-41C0-4719-A0DA-9F33504E34AC}");
            _templateMock.Setup(i => i.ParentPath).Returns("parentPath");
            _templateMock.Setup(i => i.Sections)
                .Returns(new[]
                {
                    section1.Object,
                    section2.Object
                });
            _templateMock.Setup(i => i.StandardValues).Returns(standardValues.Object);
        }

        [Test]
        public void TemplateFormatedCorrectly()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Sinj-Template.hbs");

            var expectedResult = File.ReadAllText("..//..//Resources//sinj-template.js");

            Assert.That(new TemplateEngine().Render(template, _templateMock.Object),
                Is.EqualTo(expectedResult));
        }
    }
}
