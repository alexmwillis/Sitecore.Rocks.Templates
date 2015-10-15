using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests
{
    [TestFixture]
    public class ToMinqTests
    {
        private Mock<ISitecoreItem> _itemWithFieldsMock;
        private Mock<ISitecoreItem> _itemWithNoFieldsMock;
        private string _templateId;

        [SetUp]
        public void SetUp()
        {
            _templateId = Guid.NewGuid().ToString();

            var fieldMock1 = new Mock<ISitecoreField>();
            fieldMock1.Setup(f => f.Name).Returns("Field Name 1");

            var fieldMock2 = new Mock<ISitecoreField>();
            fieldMock2.Setup(f => f.Name).Returns("Field Name 2");

            _itemWithFieldsMock = new Mock<ISitecoreItem>();
            _itemWithFieldsMock.Setup(i => i.Name).Returns("Item With Fields");
            _itemWithFieldsMock.Setup(i => i.TemplateId).Returns(_templateId);
            _itemWithFieldsMock.Setup(i => i.Fields).Returns(new[] { fieldMock1.Object, fieldMock2.Object });
            
            _itemWithNoFieldsMock = new Mock<ISitecoreItem>();
            _itemWithNoFieldsMock.Setup(i => i.Name).Returns("Item With No Fields");
            _itemWithNoFieldsMock.Setup(i => i.TemplateId).Returns(_templateId);
        }

        [Test]
        public void TestModelIsCorrectlyFormatted()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Minq.hbs");

            var expectedResult =
                $@"[SitecoreTemplate(""{_templateId}"")]
public class ItemWithFieldsModel : SitecoreItemModel
{{
    [SitecoreField(""Field Name 1"")]
    public string FieldName1 {{ get; set; }}

    [SitecoreField(""Field Name 2"")]
    public string FieldName2 {{ get; set; }}
}}";

            Assert.That(new TemplateEngine().Render(template, _itemWithFieldsMock.Object),
                Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestNothingIsReturnedWhenTheItemHasNoFields()
        {
            var template = File.ReadAllText("..//..//..//Sitecore.Rocks.Templates//Resources//Minq.hbs");
            
            Assert.That(new TemplateEngine().Render(template, _itemWithNoFieldsMock.Object),
                Is.EqualTo(string.Empty));
        }
    }
}
