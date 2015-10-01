using Sitecore.VisualStudio.Extensions.IItemSelectionContextExtensions;
using Sitecore.VisualStudio.Commands;
using Sitecore.VisualStudio.ContentTrees;
using Sitecore.VisualStudio.Data;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Extensions;

namespace Sitecore.Rocks.Templates.Commands
{
    [Command]
    public abstract class SingleTreeItemCommand : ContentTreeCommand
    {
        protected override bool CanExecute(ContentTreeContext context)
        {
            return context.OneItemSelected() && context.GetSelectedAsItemTree() != null;
        }

        protected abstract void Execute(ISitecoreItem item);

        protected override void Execute(ContentTreeContext context)
        {
            var itemTree = context.GetSelectedAsItemTree();

            var dataService = context.GetSite().DataService;

            var item = dataService.GetItemFields(new ItemVersionUri(itemTree.ItemUri, Language.Current, Version.Latest));

            Execute(new SitecoreItem(item));
        }
    }
}
