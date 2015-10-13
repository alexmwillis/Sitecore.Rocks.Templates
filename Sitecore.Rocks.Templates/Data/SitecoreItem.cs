using System;
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

        public string Id => _innerItem.ItemUri.ItemId.ToString();

        public string Name => _innerItem.Name;

        public string ItemPath => _innerItem.Path.ToString();

        public string Language => "TODO";

        public string TemplateId => _innerItem.TemplateId.ToString();

        public string TemplateName => _innerItem.TemplateName;

        public string TemplatePath => "TODO";

        public IEnumerable<ISitecoreField> Fields => _innerItem.Fields.Select(f => new SitecoreField(f));

        public IEnumerable<ISitecoreField> StandardFields => _innerItem.Fields.Select(f => new SitecoreField(f)).Where(f => f.IsStandardField);
    }
}
