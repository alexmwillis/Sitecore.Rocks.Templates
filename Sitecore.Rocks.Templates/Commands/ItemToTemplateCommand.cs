using System.Linq;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.Rocks.Templates.Engine;
using Sitecore.Rocks.Templates.IO;
using Sitecore.Rocks.Templates.UI;

namespace Sitecore.Rocks.Templates.Commands
{
    public class ItemToTemplateCommand : SingleTreeItemCommand
    {
        private readonly ITemplateFileService _fileService;
        private readonly ITemplateEngine _engineService;

        public ItemToTemplateCommand()
        {
            Text = "Copy to template...";
            SortingValue = 1000;

            _fileService = new TemplateFileService();
            _engineService = new TemplateEngine();
        }

        protected override void ExecuteInner(SitecoreTemplate item)
        {
            var template = GetTemplate(TemplateType.SitecoreTemplate);
            CopyToClipboard(template, item);
        }

        protected override void ExecuteInner(SitecoreItem item)
        {
            var template = GetTemplate(TemplateType.SitecoreItem);
            CopyToClipboard(template, item);
        }

        private void CopyToClipboard(TemplateMetaData template, object item)
        {
            if (template == null || item == null) return;

            var content = _fileService.GetTemplateContent(template);

            AppHost.Clipboard.SetText(_engineService.Render(content, item));
        }

        private TemplateMetaData GetTemplate(TemplateType type)
        {
            var templates = _fileService.GetTemplates(type).ToArray();

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

        public TemplateMetaData ShowUiAndGetTemplate(TemplateMetaData[] templates)
        {
            var templatesViewModel = templates
                .Select(t => new TemplateViewModel {DisplayName = t.Name, FullName = t.FullName})
                .ToArray();

            var selectTemplateViewModel = new SelectTemplateViewModel
            {
                Templates = templatesViewModel,
                SelectedTemplate = templatesViewModel.First()
            };

            var showDialogResult = new SelectTemplateWindow(selectTemplateViewModel).ShowDialog();

            return (showDialogResult ?? false)
                ? templates.First(t => t.FullName == selectTemplateViewModel.SelectedTemplate.FullName)
                : null;
        }
    }
}
