using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Service
{
    public class TemplateService : ITemplateService
    {
        private static readonly string TtemTemplateLocation =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Sitecore\\Sitecore.Rocks\\Plugins\\Item Templates";

        private static readonly string TemplateTemplateLocation =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Sitecore\\Sitecore.Rocks\\Plugins\\Template Templates";

        private readonly ITemplateEngine _engine;

        public TemplateService(ITemplateEngine engine)
        {
            _engine = engine;
        }

        public string Render(ITemplateMetaData template, object source)
        {
            return _engine.Render(GetTemplate(template), source);
        }
        
        public IEnumerable<ITemplateMetaData> GetTemplates(TemplateType type)
        {
            return GetFileInfos(type).Select(i => new TemplateMetaData
            {
                FullName = i.FullName,
                Name = i.Name
            });
        }

        private static string GetTemplate(ITemplateMetaData template)
        {
            return File.ReadAllText(template.FullName);
        }

        private static IEnumerable<FileInfo> GetFileInfos(TemplateType type)
        {
            switch (type)
            {
                case TemplateType.SitecoreItem:
                    return TryGetFiles(TtemTemplateLocation);

                case TemplateType.SitecoreTemplate:
                    return TryGetFiles(TemplateTemplateLocation);

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static IEnumerable<FileInfo> TryGetFiles(string directory)
        {
            return Directory.Exists(directory) ? Directory.GetFiles(directory).Select(f => new FileInfo(f)) : Enumerable.Empty<FileInfo>();
        }
    }
}
