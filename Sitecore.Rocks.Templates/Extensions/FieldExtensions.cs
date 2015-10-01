using Sitecore.VisualStudio.Data;
using Sitecore.Rocks.Templates.Data;

namespace Sitecore.Rocks.Templates.Extensions
{
    public static class FieldExtensions
    {
        public static bool IsStandardField(this ISitecoreField field)
        {
            return field.Name.StartsWith("__");
        }
    }
}
