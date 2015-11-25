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

        public string Render(TemplateMetaData template, object data)
        {
            var source = _fileService.GetTemplateContent(template);

            return FSharp.TemplateEngine.render(source)(data);
            
        }

        private void RegisterPartials()
        {
            var partials = _fileService.GetPartials();
            partials.ForEach(p => FSharp.TemplateEngine.registerPartial(p.Name, _fileService.GetTemplateContent(p)));
        }
    }
}
