using System.Collections.Generic;
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

        public ItemToTemplateCommand() 
        {
            Text = "Copy to template...";
            SortingValue = 1000;

            _service = new TemplateService(new TemplateEngine());
        }

        protected override void ExecuteInner(SitecoreTemplate item)
        {
            var template = GetTemplate(TemplateType.SitecoreTemplate);
            AppHost.Clipboard.SetText(_service.Render(template, item));
        }

        protected override void ExecuteInner(SitecoreItem item)
        {
            var template = GetTemplate(TemplateType.SitecoreItem);
            AppHost.Clipboard.SetText(_service.Render(template, item));
        }

        private ITemplateMetaData GetTemplate(TemplateType type)
        {
            var templates = _service.GetTemplates(type).ToArray();

            switch (templates.Length)
            {
                case 0:
                    return null;
                case 1:
                    return templates.First();

                default:
                    return ShowUiAndGetTemplate(templates);
            }
        }

        public ITemplateMetaData ShowUiAndGetTemplate(ITemplateMetaData[] templates)
        {
            var templatesViewModel = new SelectTemplateViewModel(templates);
            
            var showDialogResult = new SelectTemplateWindow(templatesViewModel).ShowDialog();

            return (showDialogResult ?? false)
                ? templates.First(t => t.FullName == templatesViewModel.SelectedTemplate.FullName)
                : null;
        }
    }
}
