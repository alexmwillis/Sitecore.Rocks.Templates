using System.Collections.Generic;
using System.Linq;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data
{
    public class SitecoreTemplateBuilder
    {
        private readonly SitecoreDataService _builder;

        public SitecoreTemplateBuilder(SitecoreDataService builder)
        {
            _builder = builder;
        }

        public SitecoreTemplate Build(ItemUri itemUri)
        {
            var item = _builder.GetItem(itemUri);

            return new SitecoreTemplate
            {
                Id = item.ItemUri.ItemId.ToString(),
                Name = item.Name,
                ParentPath = _builder.GetParentPath(item.Path),
                BaseTemplateList = item.BaseTemplates.Aggregate("", (s, a) => s + "|" + a.ItemId.ToString()),
                Icon = item.Icon.IconPath,
                Sections = GetSections(itemUri)
            };
        }

        private IEnumerable<SitecoreTemplateSection> GetSections(ItemUri itemUri)
        {
            var children = _builder.GetChildHeaders(itemUri);
            return children.Select(i => new SitecoreTemplateSection
            {
                Id = i.ItemId.ToString(),
                Name = i.Name,
                Icon = i.Icon.IconPath,
                Fields = GetFields(i.ItemUri)
            });
        }

        private IEnumerable<SitecoreTemplateField> GetFields(ItemUri itemUri)
        {
            var children = _builder.GetChildHeaders(itemUri);
            return children.Select(f => new SitecoreTemplateField
            {
                Id = f.ItemId.ToString(),
                Name = f.Name,
                SortOrder = f.SortOrder.ToString(),
                Type = "TODO"
            });
        }
    }
}
