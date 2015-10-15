using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Rocks.Templates.Data
{
    interface ISitecoreTemplate: ISitecoreItem
    {
        IEnumerable<ISitecoreSection> Sections { get; }

        ISitecoreItem StandardFields { get; }
    }
}
