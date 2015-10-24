using Mustache;
using Sitecore.Rocks.Templates.Engine.TagDefinitions;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngine : ITemplateEngine
    {
        private readonly FormatCompiler _compiler;

        public TemplateEngine()
        {
            _compiler = new FormatCompiler();
            _compiler.RegisterTag(new CamelCaseTag(), false);
            _compiler.RegisterTag(new PascelCaseTag(), false);
            _compiler.RegisterTag(new NotFirstTag(), false);
            _compiler.RegisterTag(new WhereTag(), false);
        }

        public string Render(string template, object source)
        {
            return _compiler
                .Compile(template)
                .Render(source);
        }
    }
}
