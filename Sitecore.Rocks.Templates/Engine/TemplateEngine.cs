using System;
using System.Collections.Generic;
using Mustache;
using Sitecore.Rocks.Templates.Engine.TagDefinitions;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngine : ITemplateEngine
    {
        private readonly FormatCompiler _compiler;
        private readonly Dictionary<string, Func<string>> _getPartials = new Dictionary<string, Func<string>>();

        public TemplateEngine()
        {
            _compiler = new FormatCompiler
            {
                RemoveNewLines = true
            };

            _compiler.RegisterTag(new CamelCaseTag(), false);
            _compiler.RegisterTag(new PascelCaseTag(), false);
            _compiler.RegisterTag(new NotFirstTag(), false);
            _compiler.RegisterTag(new WhereTag(), false);
            _compiler.RegisterTag(new WithFirstTag(), false);
            _compiler.RegisterTag(new NewGuidTag(), false);
            _compiler.RegisterTag(new IfEqualTag(), false);
            _compiler.RegisterTag(new ElseEqualTag(), false);
            _compiler.RegisterTag(new PartialTag(this), false);
        }

        public string Render(string template, object source)
        {
            return _compiler
                .Compile(template)
                .Render(source);
        }

        public void RegisterPartial(string name, Func<string> getPartial)
        {
            _getPartials.Add(name, getPartial);
        }

        public string RenderPartial(string partial, object source)
        {
            Func<string> getPartial;
            return _getPartials.TryGetValue(partial, out getPartial)
                ? Render(getPartial(), source)
                : string.Empty;
        }
    }
}
