using System.Collections.Generic;
using System.IO;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class RemoveWhiteSpaceTag : InlineTagDefinition
    {
        public RemoveWhiteSpaceTag()
            : base("removeWhiteSpace")
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
                writer.Write(str.Replace(" ", string.Empty));
            }
        }
    }
}
