using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests
{
    [TestFixture]
    public class ToSinjTests
    {
        private Mock<ISitecoreItem> _itemWithFieldsMock;
        private Mock<ISitecoreItem> _itemWithNoFieldsMock;
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
            
            _itemWithFieldsMock = new Mock<ISitecoreItem>();
            _itemWithFieldsMock.Setup(i => i.Name).Returns("Item With Fields");
            _itemWithFieldsMock.Setup(i => i.Id).Returns(_itemId);
            _itemWithFieldsMock.Setup(i => i.ItemPath).Returns(_itemPath);
            _itemWithFieldsMock.Setup(i => i.Language).Returns(_language);
            _itemWithFieldsMock.Setup(i => i.TemplatePath).Returns(_templatePath);
            _itemWithFieldsMock.Setup(i => i.TemplateName).Returns(_templateName);
            _itemWithFieldsMock.Setup(i => i.Fields)
                .Returns(new[]
                {
                    fieldMock1.Object, fieldMock2.Object, emptyFieldMock.Object
                });

            _itemWithNoFieldsMock = new Mock<ISitecoreItem>();
            _itemWithNoFieldsMock.Setup(i => i.Name).Returns("Item With No Fields");
            _itemWithNoFieldsMock.Setup(i => i.Id).Returns(_itemId);
            _itemWithNoFieldsMock.Setup(i => i.ItemPath).Returns(_itemPath);
            _itemWithNoFieldsMock.Setup(i => i.Language).Returns(_language);
            _itemWithNoFieldsMock.Setup(i => i.TemplatePath).Returns(_templatePath);
            _itemWithNoFieldsMock.Setup(i => i.TemplateName).Returns(_templateName);
        }

        [Test]
        public void TestNoFieldsItemIsCorrectlyFormatted()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Sinj.hbs");

            var expectedResult = $@"var ItemWithNoFieldsTemplate: SinjItemDto = {{ 
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

            var expectedResult = $@"var ItemWithFieldsTemplate: SinjItemDto = {{ 
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

            Assert.That(new TemplateEngine().Render(template, _itemWithFieldsMock.Object),
                Is.EqualTo(expectedResult));
        }
    }
}
