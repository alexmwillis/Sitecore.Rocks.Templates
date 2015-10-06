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
        public static string RenderItemTemplate(string template, ISitecoreItem item)
        {
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
