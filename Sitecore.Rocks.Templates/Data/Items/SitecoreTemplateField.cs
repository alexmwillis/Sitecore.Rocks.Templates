using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data.Items
{
    public class SitecoreTemplateField
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string SortOrder { get; set; }

        public IEnumerable<SitecoreField> Fields { get; set; }
    }
}
