using System.Collections.Generic;
using System.Linq;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data
{
    public class SitecoreItemBuilder
    {
        private readonly SitecoreDataService _service;

        public SitecoreItemBuilder(SitecoreDataService service)
        {
            _service = service;
        }

        public SitecoreItem Build(ItemUri itemUri)
        {
            var item = GetItem(itemUri);
            item.Children = GetChildren(itemUri);
            return item;
        }

        public SitecoreItem GetItem(ItemUri itemUri)
        {
            var item = _service.GetItem(itemUri);
            var template = _service.GetItem(new ItemUri(item.ItemUri.DatabaseUri, item.TemplateId));

            return new SitecoreItem
            {
                Id = item.ItemUri.ItemId.ToString(),
                Name = item.Name,
                ParentPath = _service.GetParentPath(item.Path),
                Language = Language.Current.ToString(),
                TemplateId = item.TemplateId.ToString(),
                TemplateName = item.TemplateName,
                TemplatePath = template.GetPath(),
                Fields = item.Fields.Select(GetField)
            };
        }

        private IEnumerable<SitecoreItem> GetChildren(ItemUri itemUri)
        {
            return _service.GetChildHeaders(itemUri).Select(h => GetItem(h.ItemUri));
        }

        public SitecoreField GetField(Field field)
        {
            return new SitecoreField
            {
                Name = field.Name,
                Value = field.Value,
                IsStandardValue = field.StandardValue,
                IsStandardField = field.Name.StartsWith("__")
            };
        }
    }
}
