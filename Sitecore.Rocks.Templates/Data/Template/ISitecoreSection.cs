using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data.Template
{
    interface ISitecoreSection
    {
        string Id { get; }

        string Name { get; }

        string Icon { get; }

        IEnumerable<ISitecoreTemplateField> Fields { get; }
    }
}
