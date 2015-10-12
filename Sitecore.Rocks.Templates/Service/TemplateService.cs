using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sitecore.Rocks.Templates.Engine;

namespace Sitecore.Rocks.Templates.Service
{
    public class TemplateService : ITemplateService
    {
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
            var files = Directory.GetFiles(".//Item Templates").Select(f => new FileInfo(f));

            return files.Select(i => new TemplateMetaData
            {
                FullName = i.FullName,
                Name = i.Name
            });
        }

        private static string GetTemplate(ITemplateMetaData template)
        {
            return File.ReadAllText(template.FullName);
        }
    }
}
