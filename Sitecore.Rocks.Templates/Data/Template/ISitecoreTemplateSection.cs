using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data.Template
{
    public interface ISitecoreTemplateSection
    {
        string Id { get; }

        string Name { get; }

        string Icon { get; }

        IEnumerable<ISitecoreTemplateField> Fields { get; }
    }
}
