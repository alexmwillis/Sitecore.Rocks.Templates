using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sitecore.Rocks.Templates.Formatting
{
    public static class TemplateManager
    {
        private static readonly IEnumerable<string> SearchPaths = new[] {
            "{0}.txt",
            "..//..//Resources//{0}.txt"
        };

        private static string GetTemplate(string searchPath, string name)
        {
            var itemPath = string.Format(searchPath, name);

            if (File.Exists(itemPath))
            {
                return File.ReadAllText(itemPath);
            }
            return null;
        }

        public static string GetTemplate(string name)
        {
            return SearchPaths
                .Select(p => GetTemplate(p, name))
                .First(t => t != null);
        }
    }
}
