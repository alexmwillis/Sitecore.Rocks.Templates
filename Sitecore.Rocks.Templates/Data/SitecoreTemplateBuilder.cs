using System.Collections.Generic;
using System.Linq;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Utils;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data
{
    public class SitecoreTemplateBuilder
    {
        private readonly SitecoreDataService _service;
        private readonly SitecoreItemBuilder _itemBuilder;

        public SitecoreTemplateBuilder(SitecoreDataService service, SitecoreItemBuilder itemBuilder)
        {
            _service = service;
            _itemBuilder = itemBuilder;
        }

        public SitecoreTemplate Build(ItemUri itemUri)
        {
            var item = _service.GetItem(itemUri);

            var template = new SitecoreTemplate
            {
                Id = item.ItemUri.ItemId.ToString(),
                Name = item.Name,
                ParentPath = _service.GetParentPath(item.Path),
                BaseTemplates = GetFieldValue(item, "__Base template"),
                Icon = GetIconPath(item.Icon),
                Sections = GetSections(itemUri),
                StandardValues = GetStandardValues(itemUri)
            };

            return template;
        }
        
        private SitecoreItem GetStandardValues(ItemUri itemUri)
        {
            var children = _service.GetChildHeaders(itemUri);
            var standardValues = children.FirstOrDefault(i => i.Name == "__Standard Values");
            return standardValues != null ? _itemBuilder.Build(standardValues.ItemUri) : null;
        }

        private IEnumerable<SitecoreTemplateSection> GetSections(ItemUri itemUri)
        {
            var children = _service.GetChildHeaders(itemUri);
            return children
                .Where(i => i.Name != "__Standard Values")
                .Select(GetSection);
        }

        private SitecoreTemplateSection GetSection(ItemHeader itemHeader)
        {
            return new SitecoreTemplateSection
            {
                Id = itemHeader.ItemId.ToString(),
                Name = itemHeader.Name,
                Icon = GetIconPath(itemHeader.Icon),
                Fields = GetFields(itemHeader.ItemUri)
            };
        }

        private IEnumerable<SitecoreTemplateField> GetFields(ItemUri itemUri)
        {
            var children = _service.GetChildHeaders(itemUri).Select(i => _service.GetItem(i.ItemUri));
            return children.Select(f => new SitecoreTemplateField
            {
                Id = f.ItemUri.ItemId.ToString(),
                Name = f.Name,
                Type = GetFieldValue(f, "Type"),
                SortOrder = GetFieldValue(f, "Sortorder"),
                Fields = f.Fields
                    .Where(ff => !ff.Name.In("Type", "Sortorder"))
                    .Select(_itemBuilder.GetField)
            });
        }

        private static string GetIconPath(Icon icon)
        {
            return icon.IconPath.Replace("/temp/IconCache/", "");
        }

        private static string GetFieldValue(Item item, string fieldName)
        {
            return item.Fields.First(f => f.Name == fieldName).Value;
        }
    }
}
