using System;

namespace Sitecore.Rocks.Templates.Data.Items
{
    [Serializable]
    public class SitecoreField
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public bool IsStandardField { get; set; }
    }
}
