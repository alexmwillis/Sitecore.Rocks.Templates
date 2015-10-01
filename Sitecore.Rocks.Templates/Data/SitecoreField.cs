using Sitecore.VisualStudio.Data;
using Sitecore.Rocks.Templates.Extensions;

namespace Sitecore.Rocks.Templates.Data
{
    public interface ISitecoreField
    {
        string Name { get; }
        bool IsStandardField { get; }
    }

    public class SitecoreField : ISitecoreField
    {
        private readonly Field _innerField;

        public SitecoreField(Field innerField)
        {
            _innerField = innerField;
        }

        public string Name => _innerField.Name;

        public bool IsStandardField => this.IsStandardField();
    }
}
