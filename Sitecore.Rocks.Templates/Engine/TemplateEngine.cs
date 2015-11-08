using System.Collections.Generic;
using Mustache;
using Sitecore.Rocks.Templates.Engine.TagDefinitions;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngine : ITemplateEngine
    {
        private readonly FormatCompiler _compiler;
        private readonly Dictionary<string,string> _partials = new Dictionary<string, string>();

        public TemplateEngine()
        {
            _compiler = new FormatCompiler
            {
                RemoveNewLines = false
            };

            _compiler.RegisterTag(new CamelCaseTag(), false);
            _compiler.RegisterTag(new PascelCaseTag(), false);
            _compiler.RegisterTag(new NotFirstTag(), false);
            _compiler.RegisterTag(new WhereTag(), false);
            _compiler.RegisterTag(new WithFirstTag(), false);
            _compiler.RegisterTag(new NewGuidTag(), false);
            _compiler.RegisterTag(new PartialTag(_partials), false);
        }

        public string Render(string template, object source)
        {
            return _compiler
                .Compile(template)
                .Render(source);
        }

        public void RegisterPartial(string name, string template)
        {
            _partials.Add(name, template);
        }
    }
}
