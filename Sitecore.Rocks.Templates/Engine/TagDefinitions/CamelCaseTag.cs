using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mustache;

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
            if (!string.IsNullOrWhiteSpace(str))
            {
                var words = str.Split(' ');
                
                var camel = string.Concat(words.First().ToLowerInvariant(), string.Join("", words.Skip(1)));              
                
                writer.Write(camel);
            }
        }
    }
}
