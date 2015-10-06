using System.Collections.Generic;
using System.IO;
using Mustache;

namespace Sitecore.Rocks.Templates.TagDefinitions
{
    public class RemoveWhiteSpaceDefinition : InlineTagDefinition
    {
        public RemoveWhiteSpaceDefinition()
            : base("removeWhiteSpace")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            return new[] { new TagParameter("string") };
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            var str = (string)arguments["string"];
            writer.Write(str.Replace(" ", string.Empty));
        }
    }
}
