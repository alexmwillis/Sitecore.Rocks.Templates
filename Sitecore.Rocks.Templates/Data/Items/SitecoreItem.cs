using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data.Items
{
    public class SitecoreItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentPath { get; set; }

        public string TemplateId { get; set; }

        public string TemplateName { get; set; }

        public string TemplatePath { get; set; }

        public string Language { get; set; }

        public IEnumerable<SitecoreField> Fields { get; set; }

        public IEnumerable<SitecoreItem> Children { get; set; }

        public bool IsMediaItem { get; set; }

        public string Media64 { get; set; }
    }
}
