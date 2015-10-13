using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Rocks.Templates.Service;

namespace Sitecore.Rocks.Templates.UI
{
    public class SelectTemplateViewModel
    {
        private readonly Func<IEnumerable<ITemplateMetaData>> _getTemplates;

        public SelectTemplateViewModel(ITemplateService service)
        {
            _getTemplates = service.GetTemplates;
        }

        public IEnumerable<TemplateViewModel> Templates =>

            _getTemplates().Select(t => new TemplateViewModel
            {
                DisplayName = t.Name,
                FullName = t.FullName
            });

        public TemplateViewModel SelectedTemplate { get; set; }
    }
}
