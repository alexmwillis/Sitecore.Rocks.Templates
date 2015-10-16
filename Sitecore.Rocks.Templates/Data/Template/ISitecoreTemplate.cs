using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data
{
    public interface ISitecoreTemplate
    {
        string Id { get; }

        string Name { get; }

        string ItemPath { get; }

        string TemplatePath { get; }

        string Icon { get; }

        string BaseTemplateList { get; }

        IEnumerable<ISitecoreSection> Sections { get; }

        ISitecoreItem StandardFields { get; }
    }
}
