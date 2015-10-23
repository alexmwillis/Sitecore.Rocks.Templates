using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public Item GetItem(ItemUri uri)
        {
            return _dataService.GetItemFields(new ItemVersionUri(uri, Language.Current, Version.Latest));
        }

        public string GetParentPath(IEnumerable<ItemPath> path)
        {
            return path
                .Skip(1)
                .Reverse()
                .Aggregate("", (s, a) => s + "/" + a.Name);
        }

        public IEnumerable<ItemHeader> GetChildHeaders(ItemUri uri)
        {
            AutoResetEvent stopWaitHandle = new AutoResetEvent(false);

            IEnumerable<ItemHeader> result = null;

            GetItemsCompleted<ItemHeader> getChildrenCallback = (headers) =>
            {
                result = headers;
                stopWaitHandle.Set();
            };

            new Task(() => _dataService.GetChildrenAsync(uri, getChildrenCallback)).Start();

            stopWaitHandle.WaitOne();

            return result;
        }
    }
}
