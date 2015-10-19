using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data
{
    public class SitecoreItem : ISitecoreItem
    {
        private readonly Language _language;
        private readonly Item _item;
        private readonly Item _template;

        public SitecoreItem(Item item, Item template, Language language)
        {
            _item = item;
            _template = template;
            _language = language;
        }

        public string Id => _item.ItemUri.ItemId.ToString();

        public string Name => _item.Name;

        public string ItemPath => _item.GetPath();

        public string Language => _language.ToString();

        public string TemplateId => _item.TemplateId.ToString();

        public string TemplateName => _item.TemplateName;

        public string TemplatePath => _template.GetPath();

        public IEnumerable<ISitecoreField> Fields => _item.Fields.Select(f => new SitecoreField(f));

        public IEnumerable<ISitecoreItem> Children { get { throw new NotImplementedException();} }
    }
}
