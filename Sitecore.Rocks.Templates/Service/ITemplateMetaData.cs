using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Rocks.Templates.Service
{
    public interface ITemplateMetaData
    {
        string Name { get; set; }

        string FullName { get; set; }
    }
}
