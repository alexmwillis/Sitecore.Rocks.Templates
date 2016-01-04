using MoreLinq;
using Sitecore.Rocks.Templates.FSharp;
using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngineService: ITemplateEngineService
    {
        private readonly TemplateFileService _fileService;

        public TemplateEngineService()
        {
            FSharp.Helpers.Init(); // todo find better way to initialise this

            _fileService = new TemplateFileService();

            RegisterPartials();
        }

        public string Render(TemplateMetaData template, object data)
        {
            var source = _fileService.GetTemplateContent(template);
            
            return TemplateEngine.Compile(source)(data);
        }

        private void RegisterPartials()
        {
            var partials = _fileService.GetPartials();
            partials.ForEach(p => TemplateEngine.RegisterPartial(p.Name, _fileService.GetTemplateContent(p)));
        }
    }
}
