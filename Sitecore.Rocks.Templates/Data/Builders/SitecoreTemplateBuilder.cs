using System.Linq;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data.Builders
{
    public static class SitecoreTemplateBuilder
    {
        public static SitecoreTemplate Build(Item item) 
        {
            return new SitecoreTemplate
            {
                Id = item.ItemUri.ItemId.ToString(),
                Name = item.Name,
                ParentPath = item.ItemUri.ToString(),
                BaseTemplateList = item.Fields.First(f => f.Name == "__Base Template List").Value,
                Icon = item.Fields.First(f => f.Name == "Icon").Value,
                Sections = item.

            }
        }

        private SitecoreTemplateSection GetSection(Item item) 
        {
            throw new Exception();
        }
    }
}
