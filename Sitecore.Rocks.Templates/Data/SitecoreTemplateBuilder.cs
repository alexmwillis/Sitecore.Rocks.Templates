using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Sitecore.Rocks.Templates.Data.Items;
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
                BaseTemplateList = item.BaseTemplates.Aggregate("", (s, a) => s + "|" + a.ItemId.ToString()),
                Icon = item.Icon.IconPath,
                Sections = GetSections(itemUri).ToList(),
                StandardValues = GetStandardValues(itemUri)
            };

            return template;
        }

        private SitecoreItem GetStandardValues(ItemUri itemUri)
        {
            var children = _service.GetChildHeaders(itemUri);
            var standardValues = children.FirstOrDefault(i => i.Name != "__Standard Values");
            return standardValues != null ? _itemBuilder.Build(standardValues.ItemUri) : null;
        }

        private IEnumerable<SitecoreTemplateSection> GetSections(ItemUri itemUri)
        {
            var children = _service.GetChildHeaders(itemUri);
            return children.Where(i => i.Name != "__Standard Values").Select(i => new SitecoreTemplateSection
            {
                Id = i.ItemId.ToString(),
                Name = i.Name,
                Icon = i.Icon.IconPath,
                Fields = GetFields(i.ItemUri).ToList()
            });
        }

        private IEnumerable<SitecoreTemplateField> GetFields(ItemUri itemUri)
        {
            var children = _service.GetChildHeaders(itemUri).Select(i => _service.GetItem(i.ItemUri));
            return children.Select(f => new SitecoreTemplateField
            {
                Id = f.ItemUri.ItemId.ToString(),
                Name = f.Name,
                SortOrder = f.Fields.First(t => t.Name == "__Sortorder").Value,
                Type = f.Fields.First(t => t.Name == "Type").Value
            });
        }
    }
}
