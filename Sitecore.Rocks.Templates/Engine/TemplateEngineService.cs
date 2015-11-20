using System.Linq;
using MoreLinq;
using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngineService: ITemplateEngineService
    {
        private readonly ITemplateEngine _templateEngine;
        private readonly TemplateFileService _fileService;

        public TemplateEngineService()
        {
            _templateEngine = new TemplateEngine();
            _fileService = new TemplateFileService();

            RegisterPartials();
        }

        public string Render(TemplateMetaData template, object source)
        {
            var content = _fileService.GetTemplateContent(template);
            return _templateEngine.Render(content, source);
        }

        private void RegisterPartials()
        {
            var partials = _fileService.GetPartials();
            partials.ForEach(p => _templateEngine.RegisterPartial(p.Name, () => _fileService.GetTemplateContent(p)));
        }
    }
}
