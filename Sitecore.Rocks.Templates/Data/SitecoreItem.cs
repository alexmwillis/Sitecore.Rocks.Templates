using System.Collections.Generic;
using System.Linq;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data
{
    public class SitecoreItem : ISitecoreItem
    {
        private readonly Item _innerItem;

        public SitecoreItem(Item innerItem)
        {
            _innerItem = innerItem;
        }

        public IEnumerable<ISitecoreField> Fields => _innerItem.Fields.Select(f => new SitecoreField(f));

        public string TemplateId => _innerItem.TemplateId.ToString();

        public string Name => _innerItem.Name;
    }

    public interface ISitecoreItem
    {
        IEnumerable<ISitecoreField> Fields { get; }

        string TemplateId { get; }

        string Name { get; }
    }
}
