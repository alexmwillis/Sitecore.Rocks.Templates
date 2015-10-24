using System.Collections.Generic;
using System.IO;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class NotFirstTag : InlineTagDefinition
    {
        public NotFirstTag()
            : base("notFirst")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            yield return new TagParameter("string") {IsRequired = true};
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            var str = arguments["string"] as string;

            if (str == null) return;

            object indexObj;
            if (!context.TryFind("index", out indexObj)) return;
            
            var index = (int) indexObj;

            if (index > 0)
            {
                writer.Write(str);
            }
        }
    }
}
