﻿using System.Collections.Generic;
using System.Linq;
using Sitecore.VisualStudio.Data;

namespace Sitecore.Rocks.Templates.Data.Builders
{
    public class SitecoreTemplateBuilder: SitecoreBuilder
    {
        public SitecoreTemplateBuilder(DataService dataService) : base(dataService)
        {
        }

        public  SitecoreTemplate Build(ItemUri itemUri)
        {
            var item = GetItem(itemUri);

            return new SitecoreTemplate
            {
                Id = item.ItemUri.ItemId.ToString(),
                Name = item.Name,
                ParentPath = item.ItemUri.ToString(),
                BaseTemplateList = item.BaseTemplates.Aggregate("", (s, a) => s + "|" + a.ItemId.ToString()),
                Icon = item.Icon.IconPath,
                Sections = GetSections(item)
            };
        }

        private IEnumerable<SitecoreTemplateSection> GetSections(Item item)
        {
            return null;
        }
    }
}
