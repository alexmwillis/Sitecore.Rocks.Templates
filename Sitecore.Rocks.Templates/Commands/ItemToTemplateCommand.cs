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

        protected override void ExecuteInner(SitecoreTemplate item)
        {
            throw new System.NotImplementedException();
        }

        protected override void ExecuteInner(SitecoreItem item)
        {
            var template = GetTemplate(item);
            AppHost.Clipboard.SetText(_service.Render(template, item));
        }

        private ITemplateMetaData GetTemplate(SitecoreItem item)
        {
            var templates = _service.GetTemplates().ToList();

            switch (templates.Count)
            {
                case 0:
                    return null;
                case 1:
                    return _service.GetTemplates().First();

                default:
                    return ShowUiAndValidate()
                        ? templates.First(t => t.FullName == _selectTemplate.SelectedTemplate.FullName)
                        : null;
            }
        }

        public bool ShowUiAndValidate()
        {
            var window = new SelectTemplateWindow(_selectTemplate);

            var showDialog = window.ShowDialog();
            return showDialog ?? false;
        }
    }
}
