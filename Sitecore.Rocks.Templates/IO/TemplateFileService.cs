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

        //TODO does this need to be local app data?
        private static readonly string ItemTemplateLocation =
            $"{LocalAppData}\\Sitecore\\Sitecore.Rocks\\Plugins\\Item Templates";

        private static readonly string TemplateTemplateLocation =
            $"{LocalAppData}\\Sitecore\\Sitecore.Rocks\\Plugins\\Template Templates";

        private static readonly string PartialTemplateLocation =
            $"{LocalAppData}\\Sitecore\\Sitecore.Rocks\\Plugins\\Partials";

        public IEnumerable<TemplateMetaData> GetTemplates(TemplateType type)
        {
            return GetFileInfos(type).Select(GetTemplateMetaData);
        }

        public IEnumerable<TemplateMetaData> GetPartials()
        {
            return GetFileInfos(TemplateType.Partial).Select(GetTemplateMetaData);
        }

        public string GetTemplateContent(TemplateMetaData template)
        {
            return File.ReadAllText(template.FullName);
        }

        private static TemplateMetaData GetTemplateMetaData(FileSystemInfo fileInfo)
        {
            return new TemplateMetaData
            {
                FullName = fileInfo.FullName,
                Name = Path.GetFileNameWithoutExtension(fileInfo.Name).TrimStart('_')
            };
        }

        private static IEnumerable<FileInfo> GetFileInfos(TemplateType type)
        {
            switch (type)
            {
                case TemplateType.SitecoreItem:
                    return TryGetFiles(ItemTemplateLocation);

                case TemplateType.SitecoreTemplate:
                    return TryGetFiles(TemplateTemplateLocation);

                case TemplateType.Partial:
                    return TryGetFiles(PartialTemplateLocation);

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
