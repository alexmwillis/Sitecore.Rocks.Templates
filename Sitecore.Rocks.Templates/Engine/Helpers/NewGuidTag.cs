using System;
using System.Collections.Generic;
using System.IO;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    class NewGuidTag : InlineTagDefinition
    {
        public NewGuidTag()
            : base("newGuid")
        {
        }

        public override void GetText(TextWriter writer, Dictionary<string, object> arguments, Scope context)
        {
            writer.Write($"{{{Guid.NewGuid()}}}");
        }
    }
}
