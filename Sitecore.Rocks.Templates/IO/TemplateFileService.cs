using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sitecore.Rocks.Templates.IO
{
    public class TemplateFileService : ITemplateFileService
    {
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
                    return TryGetFiles(".\\Resources\\Item Templates");

                case TemplateType.SitecoreTemplate:
                    return TryGetFiles(".\\Resources\\Template Templates");

                case TemplateType.Partial:
                    return TryGetFiles(".\\Resources\\Partials");

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
