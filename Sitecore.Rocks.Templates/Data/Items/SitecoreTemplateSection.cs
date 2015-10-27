using System;
using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data.Items
{
    [Serializable]
    public class SitecoreTemplateSection
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public List<SitecoreTemplateField> Fields { get; set; }
    }
}
