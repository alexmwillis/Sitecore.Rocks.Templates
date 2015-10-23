using System.Collections.Generic;
using System.Linq;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data.Builders
{
    public class SitecoreItemBuilder
    {
        private readonly SitecoreBuilder _builder;

        public SitecoreItemBuilder(SitecoreBuilder builder)
        {
            _builder = builder;
        }

        public SitecoreItem Build(ItemUri itemUri)
        {
            var item = GetItem(itemUri);
            item.Children = _builder.GetChildHeaders(itemUri).Select(h => GetItem(h.ItemUri));
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
                Fields = item.Fields.Select(SitecoreFieldBuilder.Build)
            };
        }
    }
}
