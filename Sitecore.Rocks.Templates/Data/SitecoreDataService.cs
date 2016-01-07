using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Rocks.Templates.Extensions;
using Sitecore.VisualStudio.Data;
using Version = Sitecore.VisualStudio.Data.Version;

namespace Sitecore.Rocks.Templates.Data
{
    public class SitecoreDataService
    {
        private readonly DataService _dataService;

        public SitecoreDataService(DataService dataService)
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
            try
            {
                return Task.Run(() => GetChildHeadersAsync(uri)).WaitResult();
            }
            catch (TimeoutException)
            {
                throw new TimeoutException("unabled to retrieve children");
            }
            
        }

        private Task<IEnumerable<ItemHeader>> GetChildHeadersAsync(ItemUri uri)
        {
            var t = new TaskCompletionSource<IEnumerable<ItemHeader>>();

            GetItemsCompleted<ItemHeader> getChildrenCallback = (headers) =>
            {
                t.TrySetResult(headers);
            };

            _dataService.GetChildrenAsync(uri, getChildrenCallback);

            return t.Task;
        }

        private bool IsMediaItem(ItemUri itemUri)
        {
            return IsMediaItem(GetItem(itemUri));
        }

        public bool IsMediaItem(Item item)
        {
            var field = item.Fields.FirstOrDefault(f => f.Name == "Blob");
            return field != null && HasBlobStream(field);
        }

        public Task<string> GetMediaAsBase64Async(ItemUri itemUri)
        {
            var t = new TaskCompletionSource<string>();

            if (!IsMediaItem(itemUri))
            {
                t.SetResult(null);
                return t.Task;
            }
            
            ExecuteCompleted executeCompleted = ((response, result) =>
            {
                if (!DataService.HandleExecute(response, result))
                {
                    t.SetException(new DataServiceException("failed to download media"));
                }
                t.SetResult(response);
            });

            _dataService.ExecuteAsync(
                "Media.DownloadAttachment",
                executeCompleted,
                itemUri.DatabaseName.Name,
                itemUri.ItemId.ToString());

            return t.Task;
        }

        public string GetMediaAsBase64(ItemUri itemUri)
        {
            return Task.Run(() => GetMediaAsBase64Async(itemUri)).WaitResult();
        }

        private bool HasBlobStream(Field field)
        {
            return field.IsBlob && field.HasValue && GuidExtensions.CanParse(field.Value);
        }
    }
}
