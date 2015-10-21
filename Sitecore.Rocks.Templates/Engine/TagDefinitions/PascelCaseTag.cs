using System;
using System.Collections.Generic;
using System.Globalization;
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

            if (string.IsNullOrWhiteSpace(str)) return;

            var words = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str).Split(' ');

            var pascal = SanitiseNumber(words[0]) +
                         string.Join("", words.Skip(1));

            writer.Write(pascal);
        }

        private static string SanitiseNumber(string str)
        {
            return char.IsNumber(str[0])
                ? new string(str
                    .Reverse()
                    .TakeWhile(c => !char.IsNumber(c))
                    .Reverse()
                    .ToArray())
                : str;
        }
    }
}
