using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data.Items
{
    public class SitecoreTemplateSection
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public IEnumerable<SitecoreTemplateField> Fields { get; set; }
    }
}
