using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.IO
{
    public enum TemplateType
    {
        SitecoreItem,
        SitecoreTemplate
    }

    public interface ITemplateFileService
    {
        IEnumerable<TemplateMetaData> GetTemplates(TemplateType type);

        string GetTemplateContent(TemplateMetaData template);
    }
}
