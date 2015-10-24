using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sitecore.Rocks.Templates.IO
{
    public class TemplateFileService : ITemplateFileService
    {
        private static readonly string LocalAppData =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private static readonly string ItemTemplateLocation =
            $"{LocalAppData}\\Sitecore\\Sitecore.Rocks\\Plugins\\Item Templates";

        private static readonly string TemplateTemplateLocation =
            $"{LocalAppData}\\Sitecore\\Sitecore.Rocks\\Plugins\\Template Templates";

        public IEnumerable<TemplateMetaData> GetTemplates(TemplateType type)
        {
            return GetFileInfos(type).Select(i => new TemplateMetaData
            {
                FullName = i.FullName,
                Name = i.Name
            });
        }

        public string GetTemplateContent(TemplateMetaData template)
        {
            return File.ReadAllText(template.FullName);
        }

        private static IEnumerable<FileInfo> GetFileInfos(TemplateType type)
        {
            switch (type)
            {
                case TemplateType.SitecoreItem:
                    return TryGetFiles(ItemTemplateLocation);

                case TemplateType.SitecoreTemplate:
                    return TryGetFiles(TemplateTemplateLocation);

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static IEnumerable<FileInfo> TryGetFiles(string directory)
        {
            return Directory.Exists(directory)
                ? Directory.GetFiles(directory).Select(f => new FileInfo(f))
                : Enumerable.Empty<FileInfo>();
        }
    }
}
