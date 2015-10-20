using System.Collections.Generic;
using System.Linq;
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

        protected static string GetParentPath(IEnumerable<ItemPath> path)
        {
            return path
                .Skip(1)
                .Reverse()
                .Aggregate("", (s, a) => s + "/" + a.Name);
        }
    }
}
