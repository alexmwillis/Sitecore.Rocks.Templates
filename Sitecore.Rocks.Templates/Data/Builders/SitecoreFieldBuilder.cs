using Sitecore.VisualStudio.Data;
using Sitecore.Rocks.Templates.Extensions;

namespace Sitecore.Rocks.Templates.Data
{
    public static class SitecoreFieldBuilder
    {
        public static SitecoreField Build(Field field)
        {
            return new SitecoreField
            {
                Name = field.Name,
                Value = field.Value,
                IsStandardField = field.Name.StartsWith("__")
            };
        }
    }
}
