using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mustache;
using Sitecore.Rocks.Templates.Data;

namespace Sitecore.Rocks.Templates.Formatters
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

    public static class Formatter
    {
        private static string RemoveWhiteSpace(this string str)
        {
            return str.Replace(" ", string.Empty);
        }

        public static string GetFieldSource(ISitecoreField field)
        {
            var compiler = new FormatCompiler();
            compiler.RegisterTag(new RemoveWhiteSpaceDefinition(), false);

            var template = @"{{#newline}}
    [SitecoreField(""{{Name}}"")]{{#newline}}
    public string {{#removeWhiteSpace Name}} { get; set; }{{#newline}}";

            return compiler.Compile(template).Render(field);
        }

        public static string GetModelSource(ISitecoreItem item)
        {
            var itemName = item.Name.RemoveWhiteSpace();
            var templateKey = item.TemplateId;

            var fields = item.Fields
                .Where(f => !f.IsStandardField);

            var fieldSource = fields
                .Select(GetFieldSource)
                .Aggregate((i, j) => $"{i}{j}");

            return $@"
[SitecoreTemplate({templateKey})]
public class {itemName}Model : SitecoreItemModel
{{{fieldSource}}}";
        }
    }
}
