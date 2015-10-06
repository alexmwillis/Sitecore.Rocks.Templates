using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mustache;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.TagDefinitions;

namespace Sitecore.Rocks.Templates.Formatting
{
    public static class Formatter
    {
        public static string GetFieldSource(ISitecoreField field)
        {
            var template = @"{{#newline}}
    [SitecoreField(""{{Name}}"")]{{#newline}}
    public string {{#removeWhiteSpace Name}} { get; set; }{{#newline}}";

            return GetCompiler().Compile(template).Render(field);
        }

        public static string GetModelSource(ISitecoreItem item)
        {
            var template = TemplateManager.GetTemplate("Minq");

            return GetCompiler().Compile(template).Render(item);
        }

        private static FormatCompiler GetCompiler()
        {
            var compiler = new FormatCompiler();
            compiler.RegisterTag(new RemoveWhiteSpaceDefinition(), false);
            return compiler;
        }
    }
}
