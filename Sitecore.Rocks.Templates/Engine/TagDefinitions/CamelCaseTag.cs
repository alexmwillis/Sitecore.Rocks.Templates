using System.Collections.Generic;
using System.IO;
using Mustache;
using Sitecore.Rocks.Templates.Extensions;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class CamelCaseTag : InlineTagDefinition
    {
        public CamelCaseTag()
            : base("camelCase")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            yield return new TagParameter("string") {IsRequired = true};
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            var str = (string) arguments["string"];

            if (string.IsNullOrWhiteSpace(str)) return;

            writer.Write(str.CamelCase());
        }
    }
}
