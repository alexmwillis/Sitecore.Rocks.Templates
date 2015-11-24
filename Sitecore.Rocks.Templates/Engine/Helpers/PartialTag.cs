using System.Collections.Generic;
using System.IO;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class PartialTag : InlineTagDefinition
    {
        private readonly TemplateEngine _engine;

        public PartialTag(TemplateEngine engine)
            : base("partial")
        {
            _engine = engine;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            yield return new TagParameter("partial") {IsRequired = true};
            yield return new TagParameter("source") { IsRequired = true};
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            var partial = (string)arguments["partial"];
            var source = arguments["source"];
            
            var rendered = _engine.RenderPartial(partial, source);
            writer.Write(rendered);
        }
    }
}
