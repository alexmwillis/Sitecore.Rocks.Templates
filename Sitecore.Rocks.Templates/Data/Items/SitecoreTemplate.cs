﻿using System;
using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data.Items
{
    [Serializable]
    public class SitecoreTemplate
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentPath { get; set; }

        public string Icon { get; set; }

        public string BaseTemplates { get; set; }

        public IEnumerable<SitecoreTemplateSection> Sections { get; set; }

        public SitecoreItem StandardValues { get; set; }
    }
}
