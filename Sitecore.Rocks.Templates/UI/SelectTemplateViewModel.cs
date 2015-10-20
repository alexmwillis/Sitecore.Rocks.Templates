using System.Collections.Generic;
using System.Linq;
using Sitecore.Rocks.Templates.Service;

namespace Sitecore.Rocks.Templates.UI
{
    public class SelectTemplateViewModel
    {
        public SelectTemplateViewModel(IEnumerable<ITemplateMetaData> templates)
        {
            Templates = templates.Select(t => new TemplateViewModel
            {
                DisplayName = t.Name,
                FullName = t.FullName
            });
            SelectedTemplate = Templates.First();
        }

        public IEnumerable<TemplateViewModel> Templates { get; }

        public TemplateViewModel SelectedTemplate { get; }
    }
}
