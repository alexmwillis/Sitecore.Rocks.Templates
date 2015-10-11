using System.Linq;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Engine;
using Sitecore.Rocks.Templates.Service;
using Sitecore.Rocks.Templates.UI;

namespace Sitecore.Rocks.Templates.Commands
{
    public class ItemToTemplateCommand : SingleTreeItemCommand
    {
        private ITemplateService _service;
        private SelectTemplateViewModel _selectTemplate;

        public ItemToTemplateCommand() 
        {
            Text = "Copy to template...";
            SortingValue = 1000;

            _service = new TemplateService(new TemplateEngine());
            _selectTemplate = new SelectTemplateViewModel(_service);
        }

        protected override void Execute(ISitecoreItem item)
        {
            ITemplateMetaData template;
            var templates = _service.GetTemplates();
            if (templates.Count() == 1)
            {
                template = _service.GetTemplates().First();
            }
            else
            {
                if (!ShowUIAndValidate()) return;

                template = templates.First(t => t.FullName == _selectTemplate.SelectedTemplate.FullName);
                
            }
            AppHost.Clipboard.SetText(_service.Render(template, item));
        }

        public bool ShowUIAndValidate()
        {
            SelectTemplateWindow window = new SelectTemplateWindow(_selectTemplate);

            bool? showDialog = window.ShowDialog();
            return showDialog ?? false;
        }
    }
}
