using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.Service
{
    public enum TemplateType
    {
        SitecoreItem,
        SitecoreTemplate
    }

    public interface ITemplateService
    {
        IEnumerable<ITemplateMetaData> GetTemplates(TemplateType type);

        string Render(ITemplateMetaData template, object source);
    }
}
