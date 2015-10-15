using System.Collections.Generic;
using System.IO;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class IfNotFirstTag : InlineTagDefinition
    {
        public IfNotFirstTag()
            : base("ifNotFirst")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            yield return new TagParameter("string") {IsRequired = true};
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            var str = arguments["string"] as string;

            object indexObj;
            if (!context.TryFind("index", out indexObj)) return;

            if (indexObj == null || str == null) return;

            var index = (int) indexObj;

            if (index != 0)
            {
                writer.Write(str);
            }
        }
    }
}
