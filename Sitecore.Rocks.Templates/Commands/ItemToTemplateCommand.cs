using System.Linq;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Engine;
using Sitecore.Rocks.Templates.Service;
using Sitecore.Rocks.Templates.UI;

namespace Sitecore.Rocks.Templates.Commands
{
    public class ItemToTemplateCommand : SingleTreeItemCommand
    {
        private readonly ITemplateService _service;
        private readonly SelectTemplateViewModel _selectTemplate;

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
            var templates = _service.GetTemplates().ToList();

            switch (templates.Count)
            {
                case 0:
                    return;
                case 1:
                    template = _service.GetTemplates().First();
                    break;
                default:
                    if (!ShowUiAndValidate()) return;

                    template = templates.First(t => t.FullName == _selectTemplate.SelectedTemplate.FullName);
                    break;
            }
            AppHost.Clipboard.SetText(_service.Render(template, item));
        }

        public bool ShowUiAndValidate()
        {
            var window = new SelectTemplateWindow(_selectTemplate);

            var showDialog = window.ShowDialog();
            return showDialog ?? false;
        }
    }
}
