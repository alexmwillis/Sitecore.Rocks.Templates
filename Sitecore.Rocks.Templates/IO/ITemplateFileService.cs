using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.IO
{
    public enum TemplateType
    {
        SitecoreItem,
        SitecoreTemplate,
        Partial
    }

    public interface ITemplateFileService
    {
        IEnumerable<TemplateMetaData> GetTemplates(TemplateType type);

        IEnumerable<TemplateMetaData> GetPartials();

        string GetTemplateContent(TemplateMetaData template);
    }
}
