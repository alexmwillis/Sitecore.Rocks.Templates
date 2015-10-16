using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests
{
    [TestFixture]
    public class ToTemplateSinjTests
    {
        private Mock<ISitecoreTemplate> _templateMock;
        private string _templatePath;
        private string _itemId;
        private string _itemPath;
        private string _language;
        private string _templateName;

        [SetUp]
        public void SetUp()
        {
            _templatePath = "/template/path/";
            _templateName = "Template";
            _itemPath = "/item/path/";
            _itemId = Guid.NewGuid().ToString();
            _language = "en";

            var fieldMock1 = new Mock<ISitecoreField>();
            fieldMock1.Setup(f => f.Name).Returns("Field Name 1");
            fieldMock1.Setup(f => f.Value).Returns("Field Value 1");

            var fieldMock2 = new Mock<ISitecoreField>();
            fieldMock2.Setup(f => f.Name).Returns("Field Name 2");
            fieldMock2.Setup(f => f.Value).Returns("Field Value 2");

            var emptyFieldMock = new Mock<ISitecoreField>();
            emptyFieldMock.Setup(f => f.Name).Returns("Field Name 3");
            emptyFieldMock.Setup(f => f.Value).Returns("");
            
            _templateMock = new Mock<ISitecoreTemplate>();
            _templateMock.Setup(i => i.Name).Returns("Item With Fields");
            _templateMock.Setup(i => i.Id).Returns(_itemId);
            _templateMock.Setup(i => i.ItemPath).Returns(_itemPath);
            _templateMock.Setup(i => i.Language).Returns(_language);
            _templateMock.Setup(i => i.TemplatePath).Returns(_templatePath);
            _templateMock.Setup(i => i.TemplateName).Returns(_templateName);
            _templateMock.Setup(i => i.Fields)
                .Returns(new[]
                {
                    fieldMock1.Object, fieldMock2.Object, emptyFieldMock.Object
                });
        }

        [Test]
        public void TestNoFieldsItemIsCorrectlyFormatted()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Sinj.hbs");

            var expectedResult = $@"var itemWithNoFieldsTemplate: SinjItemDto = {{ 
    id: ""{_itemId}"",
    name: ""Item With No Fields"",
    template: ""{_templatePath}"",
    parent: ""{_itemPath}"",
    language: ""{_language}"",
    fields: {{
    }}
}}";
            Assert.That(new TemplateEngine().Render(template, _itemWithNoFieldsMock.Object),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestNothingIsReturnedWhenTheItemHasNoFields()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Sinj.hbs");

            var expectedResult = $@"var itemWithFieldsTemplate: SinjItemDto = {{ 
    id: ""{_itemId}"",
    name: ""Item With Fields"",
    template: ""{_templatePath}"",
    parent: ""{_itemPath}"",
    language: ""{_language}"",
    fields: {{
        ""Field Name 1"": ""Field Value 1"",
        ""Field Name 2"": ""Field Value 2""
    }}
}}";

            Assert.That(new TemplateEngine().Render(template, _templateMock.Object),
                Is.EqualTo(expectedResult));
        }
    }
}
