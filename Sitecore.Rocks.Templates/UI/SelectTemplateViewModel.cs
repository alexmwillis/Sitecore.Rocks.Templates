using System.Collections.Generic;

namespace Sitecore.Rocks.Templates.UI
{
    public class SelectTemplateViewModel
    {
        public IEnumerable<TemplateViewModel> Templates { get; set; }

        public TemplateViewModel SelectedTemplate { get; set; }
    }
}
