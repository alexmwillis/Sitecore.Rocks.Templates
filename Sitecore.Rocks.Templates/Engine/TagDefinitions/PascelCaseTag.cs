using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class PascelCaseTag : InlineTagDefinition
    {
        public PascelCaseTag()
            : base("pascalCase")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            yield return new TagParameter("string") {IsRequired = true};
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            var str = (string) arguments["string"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                var words = str.Split(' ');
                
                var pascal = string.Join("", words);

                writer.Write(pascal);
            }
        }
    }
}
