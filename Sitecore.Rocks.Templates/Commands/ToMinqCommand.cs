using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Formatters;

namespace Sitecore.Rocks.Templates.Commands
{
    public class ToMinqCommand: SingleTreeItemCommand
    {
        protected override void Execute(ISitecoreItem item)
        {
            AppHost.Clipboard.SetText(Formatter.GetModelSource(item));
        }

        public ToMinqCommand()
        {
            this.Text = "Copy to Minq...";
            this.SortingValue = 1000;
        }
    }
}
