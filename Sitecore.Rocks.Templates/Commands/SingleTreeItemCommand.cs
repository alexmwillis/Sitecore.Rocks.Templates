using Sitecore.VisualStudio.Extensions.IItemSelectionContextExtensions;
using Sitecore.VisualStudio.Commands;
using Sitecore.VisualStudio.ContentTrees;
using Sitecore.VisualStudio.Data;
using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Extensions;
using Sitecore.VisualStudio.Extensions.DataServiceExtensions;

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

            var item = GetItem(itemTree.ItemUri, context);
            var template = GetItem(new ItemUri(item.ItemUri.DatabaseUri, item.TemplateId), context);

            Execute(new SitecoreItem(item, template, Language.Current));
        }

        private static Item GetItem(ItemUri uri, IItemSelectionContext context)
        {
            var dataService = context.GetSite().DataService;
            return dataService.GetItemFields(new ItemVersionUri(uri, Language.Current, Version.Latest));
        }
    }
}
