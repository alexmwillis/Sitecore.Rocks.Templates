using System;
using Moq;
using NUnit.Framework;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Formatting;

namespace Sitecore.Rocks.Templates.Tests
{
    [TestFixture]
    public class ToMinqCommandTest
    {
        [Test]
        public void TestFieldIsCorrectlyFormatted()
        {
            var expectedResult = @"
    [SitecoreField(""Field Name"")]
    public string FieldName { get; set; }
";

            var fieldMock = new Mock<ISitecoreField>();
            fieldMock.Setup(f => f.Name).Returns("Field Name");
            Assert.That(Formatter.GetFieldSource(fieldMock.Object), Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestModelIsCorrectlyFormatted()
        {
            var itemId = Guid.NewGuid();

            var expectedResult = 
$@"[SitecoreTemplate({itemId})]
public class ItemNameModel : SitecoreItemModel
{{
    [SitecoreField(""Field Name 1"")]
    public string FieldName1 {{ get; set; }}

    [SitecoreField(""Field Name 2"")]
    public string FieldName2 {{ get; set; }}
}}";

            var fieldMock1 = new Mock<ISitecoreField>();
            fieldMock1.Setup(f => f.Name).Returns("Field Name 1");

            var fieldMock2 = new Mock<ISitecoreField>();
            fieldMock2.Setup(f => f.Name).Returns("Field Name 2");

            var fieldMock3 = new Mock<ISitecoreField>();
            fieldMock3.Setup(f => f.Name).Returns("Ignore Field");
            fieldMock3.Setup(f => f.IsStandardField).Returns(true);

            var itemMock = new Mock<ISitecoreItem>();
            itemMock.Setup(i => i.Name).Returns("Item Name");
            itemMock.Setup(i => i.TemplateId).Returns(itemId.ToString());
            itemMock.Setup(i => i.Fields).Returns(new[] {fieldMock1.Object, fieldMock2.Object, fieldMock3.Object});

            Assert.That(Formatter.GetModelSource(itemMock.Object), Is.EqualTo(expectedResult));
        }
    }
}
