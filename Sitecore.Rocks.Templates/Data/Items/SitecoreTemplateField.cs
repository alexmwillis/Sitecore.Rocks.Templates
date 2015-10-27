using System;

namespace Sitecore.Rocks.Templates.Data.Items
{
    [Serializable]
    public class SitecoreTemplateField
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string SortOrder { get; set; }
    }
}
