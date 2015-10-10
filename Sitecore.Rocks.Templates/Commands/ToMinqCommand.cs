using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Formatting;

namespace Sitecore.Rocks.Templates.Commands
{
    public class ToMinqCommand: SingleTreeItemCommand
    {
        protected override void Execute(ISitecoreItem item)
        {
            var template = TemplateManager.GetTemplate("Minq");

            AppHost.Clipboard.SetText(Formatter.RenderItemTemplate(template, item));
        }

        public ToMinqCommand()
        {
            Text = "Copy to Minq...";
            SortingValue = 1000;
        }
    }
}
