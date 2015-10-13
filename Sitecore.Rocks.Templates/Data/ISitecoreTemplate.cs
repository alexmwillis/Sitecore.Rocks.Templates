using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Rocks.Templates.Data
{
    interface ISitecoreTemplate: ISitecoreItem
    {
        string Id { get; }

        string Name { get; }

        string ItemPath { get; }

        string Language { get; }

        IEnumerable<ISitecoreField> Fields { get; }

        IEnumerable<ISitecoreSection> Sections { get; }

        ISitecoreItem StandardFields { get; }
    }
}
