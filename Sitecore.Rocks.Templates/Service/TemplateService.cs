using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly string _itemTemplateLocation =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Sitecore\\Sitecore.Rocks\\Plugins\\Item Templates";

        private readonly ITemplateEngine _engine;

        public TemplateService(ITemplateEngine engine)
        {
            _engine = engine;
        }

        public string Render(ITemplateMetaData template, object source)
        {
            return _engine.Render(GetTemplate(template), source);
        }

        public IEnumerable<ITemplateMetaData> GetTemplates()
        {
            var files = TryGetFiles(_itemTemplateLocation);

            return files.Select(i => new TemplateMetaData
            {
                FullName = i.FullName,
                Name = i.Name
            });
        }

        private static IEnumerable<FileInfo> TryGetFiles(string directory)
        {
            return Directory.Exists(directory) 
                ? Directory.GetFiles(directory).Select(f => new FileInfo(f)) 
                : Enumerable.Empty<FileInfo>();
        }

        private static string GetTemplate(ITemplateMetaData template)
        {
            return File.ReadAllText(template.FullName);
        }
    }
}
