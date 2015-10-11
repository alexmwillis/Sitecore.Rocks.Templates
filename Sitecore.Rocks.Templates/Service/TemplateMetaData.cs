using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Rocks.Templates.Service
{
    public class TemplateMetaData : ITemplateMetaData
    {
        public string Name { get; set; }

        public string FullName { get; set; }
    }
}
