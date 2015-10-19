using System.Linq;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data
{
    public class SitecoreItemBuilder
    {
        private readonly DataService _dataService;

        public SitecoreItemBuilder(DataService dataService)
        {
            _dataService = dataService;
        }

        public SitecoreItem Build(ItemUri itemUri)
        {
            var item = GetItem(itemUri);
            var template = GetItem(new ItemUri(item.ItemUri.DatabaseUri, item.TemplateId));

            return new SitecoreItem
            {
                Id = item.ItemUri.ItemId.ToString(),
                Name = item.Name,
                ParentPath = item.ItemUri.ToString(),
                Language = Language.Current.ToString(),
                TemplateId = item.TemplateId.ToString(),
                TemplateName = item.TemplateName,
                TemplatePath = template.GetPath(),
                Fields = item.Fields.Select(f => SitecoreFieldBuilder.Build(f))
            };
        }

        private Item GetItem(ItemUri uri)
        {
            return _dataService.GetItemFields(new ItemVersionUri(uri, Language.Current, Version.Latest));
        }
    }
}
