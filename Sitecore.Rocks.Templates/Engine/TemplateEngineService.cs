using MoreLinq;
using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngineService: ITemplateEngineService
    {
        private readonly TemplateFileService _fileService;

        public TemplateEngineService()
        {
            _fileService = new TemplateFileService();

            RegisterPartials();
        }

        public string Render(TemplateMetaData template, object data)
        {
            var source = _fileService.GetTemplateContent(template);

            FSharp.Helpers.Init(); // todo find better way to initialise this
            return FSharp.TemplateEngine.Compile(source)(data);
        }

        private void RegisterPartials()
        {
            var partials = _fileService.GetPartials();
            partials.ForEach(p => FSharp.TemplateEngine.RegisterPartial(p.Name, _fileService.GetTemplateContent(p)));
        }
    }
}
