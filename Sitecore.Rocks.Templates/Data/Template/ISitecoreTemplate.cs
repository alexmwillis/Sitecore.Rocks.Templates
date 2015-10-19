using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data.Template
{
    public interface ISitecoreTemplate
    {
        string Id { get; }

        string Name { get; }

        string ParentPath { get; }

        string Icon { get; }

        string BaseTemplateList { get; }

        IEnumerable<ISitecoreTemplateSection> Sections { get; }

        ISitecoreItem StandardValues { get; }
    }
}
