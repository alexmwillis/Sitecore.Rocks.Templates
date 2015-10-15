using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Data
{
    public interface ISitecoreItem
    {
        string Id { get; }

        string Name { get; }

        string ItemPath { get; }

        string TemplateId { get; }

        string TemplateName { get; }

        string TemplatePath { get; }

        string Language { get; }

        IEnumerable<ISitecoreField> Fields { get; }
    }
}
