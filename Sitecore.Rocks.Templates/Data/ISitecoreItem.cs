using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
