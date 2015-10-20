using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data.Builders
{
    public class SitecoreBuilder
    {
        private readonly DataService _dataService;

        public SitecoreBuilder(DataService dataService)
        {
            _dataService = dataService;
        }

        protected Item GetItem(ItemUri uri)
        {
            return _dataService.GetItemFields(new ItemVersionUri(uri, Language.Current, Version.Latest));
        }
    }
}
