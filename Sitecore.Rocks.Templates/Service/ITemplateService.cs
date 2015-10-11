using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Rocks.Templates.Service
{
    public interface ITemplateService
    {
        IEnumerable<ITemplateMetaData> GetTemplates();

        string Render(ITemplateMetaData template, object source);
    }
}
