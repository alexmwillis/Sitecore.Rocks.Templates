using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sitecore.VisualStudio.Data;
using Version = Sitecore.VisualStudio.Data.Version;

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

        protected IEnumerable<ItemHeader> GetChildren(ItemUri uri)
        {
            AutoResetEvent stopWaitHandle = new AutoResetEvent(false);

            IEnumerable<ItemHeader> result = null;

            GetItemsCompleted<ItemHeader> getChildrenCallback = (res) =>
            {
                result = res;
                stopWaitHandle.Set();
            };

            new Task(() => _dataService.GetChildrenAsync(uri, getChildrenCallback)).Start();

            stopWaitHandle.WaitOne();
           
            return result;
        }
    }
}
