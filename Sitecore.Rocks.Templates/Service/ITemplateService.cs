using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Service
{
    public interface ITemplateService
    {
        IEnumerable<ITemplateMetaData> GetTemplates();

        string Render(ITemplateMetaData template, object source);
    }
}
