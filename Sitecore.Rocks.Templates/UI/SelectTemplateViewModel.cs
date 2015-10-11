using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Rocks.Templates.Service;

namespace Sitecore.Rocks.Templates.UI
{
    public class SelectTemplateViewModel
    {
        private readonly IEnumerable<ITemplateMetaData> _templates;

        public SelectTemplateViewModel(ITemplateService service) {
            _templates = service.GetTemplates();
        }

        public IEnumerable<TemplateViewModel> Templates
        {
            get
            {
                return _templates.Select(t => new TemplateViewModel
                {
                    DisplayName = t.Name,
                    FullName = t.FullName
                });
            }
        }

        public TemplateViewModel SelectedTemplate { get; set; }
    }
}
