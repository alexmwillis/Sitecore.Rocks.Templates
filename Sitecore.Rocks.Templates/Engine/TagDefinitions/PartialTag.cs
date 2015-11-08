using System.Collections.Generic;
using System.IO;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class PartialTag : InlineTagDefinition
    {
        private readonly IDictionary<string, string> _partials;

        public PartialTag(IDictionary<string, string> partials)
            : base("partial")
        {
            _partials = partials;
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

            string template;
            if (_partials.TryGetValue(partial, out template))
            {
                var rendered = new TemplateEngine().Render(template, source);
                writer.Write(rendered);
            }
        }
    }
}
