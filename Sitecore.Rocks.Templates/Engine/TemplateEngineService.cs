using System;
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

            var compileOutput =
                FSharp.TemplateEngine.Render(FSharp.TemplateEngine.CompileSourceParamater.NewString(source));

            if (compileOutput is FSharp.TemplateEngine.CompileOutput<object>.StringFunction)
            {
                return (compileOutput as FSharp.TemplateEngine.CompileOutput<object>.StringFunction).Item(data);
            }
            throw new Exception();
        }

        private void RegisterPartials()
        {
            var partials = _fileService.GetPartials();
            partials.ForEach(p => FSharp.TemplateEngine.RegisterPartial(p.Name, _fileService.GetTemplateContent(p)));
        }
    }
}
