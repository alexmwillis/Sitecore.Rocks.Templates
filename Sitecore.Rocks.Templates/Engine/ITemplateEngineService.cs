using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Engine
{
    interface ITemplateEngineService
    {
        string Render(TemplateMetaData template, object source);
    }
}
