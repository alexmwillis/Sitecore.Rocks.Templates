using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Tests
{
    [TestFixture]
    public class WhereTagTests
    {
        private string _templateId;
        private Mock<ISitecoreItem> _itemWithFieldsMock;

        [Test]
        public void TestThat()
        {
            var expectedResult = "'Field Name 1'\r\n'Field Name 2'\r\n";

            _templateId = Guid.NewGuid().ToString();

            var fieldMock1 = new Mock<ISitecoreField>();
            fieldMock1.Setup(f => f.Name).Returns("Field Name 1");
            fieldMock1.Setup(f => f.Value).Returns("Field Value 1");

            var fieldMock2 = new Mock<ISitecoreField>();
            fieldMock2.Setup(f => f.Name).Returns("Field Name 2");
            fieldMock2.Setup(f => f.Value).Returns("Field Value 2");

            _itemWithFieldsMock = new Mock<ISitecoreItem>();
            _itemWithFieldsMock.Setup(i => i.Name).Returns("Item With Fields");
            _itemWithFieldsMock.Setup(i => i.TemplateId).Returns(_templateId);
            _itemWithFieldsMock.Setup(i => i.Fields).Returns(new[] { fieldMock1.Object, fieldMock2.Object });

            var template = File.ReadAllText("..//..//Resources//Where.txt");

            Assert.That(new TemplateEngine().Render(template, _itemWithFieldsMock.Object),
                Is.EqualTo(expectedResult));
        }
    }
}
