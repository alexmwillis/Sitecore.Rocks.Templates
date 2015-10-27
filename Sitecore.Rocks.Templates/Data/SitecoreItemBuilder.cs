using System.Collections.Generic;
using System.Linq;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data
{
    public class SitecoreItemBuilder
    {
        private readonly SitecoreDataService _builder;

        public SitecoreItemBuilder(SitecoreDataService builder)
        {
            _builder = builder;
        }

        public SitecoreItem Build(ItemUri itemUri)
        {
            var item = GetItem(itemUri);
            item.Children = GetChildren(itemUri);
            return item;
        }

        public SitecoreItem GetItem(ItemUri itemUri)
        {
            var item = _builder.GetItem(itemUri);
            var template = _builder.GetItem(new ItemUri(item.ItemUri.DatabaseUri, item.TemplateId));

            return new SitecoreItem
            {
                Id = item.ItemUri.ItemId.ToString(),
                Name = item.Name,
                ParentPath = _builder.GetParentPath(item.Path),
                Language = Language.Current.ToString(),
                TemplateId = item.TemplateId.ToString(),
                TemplateName = item.TemplateName,
                TemplatePath = template.GetPath(),
                Fields = item.Fields.Select(GetField).ToList()
            };
        }

        private IEnumerable<SitecoreItem> GetChildren(ItemUri itemUri)
        {
            return _builder.GetChildHeaders(itemUri).Select(h => GetItem(h.ItemUri));
        }

        private static SitecoreField GetField(Field field)
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
