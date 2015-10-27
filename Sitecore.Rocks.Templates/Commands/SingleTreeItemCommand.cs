using Sitecore.Rocks.Templates.Data;
using Sitecore.Rocks.Templates.Data.Items;
using Sitecore.VisualStudio.Commands;
using Sitecore.VisualStudio.ContentTrees;
using Sitecore.VisualStudio.Extensions.IItemSelectionContextExtensions;

namespace Sitecore.Rocks.Templates.Commands
{
    [Command]
    public abstract class SingleTreeItemCommand : ContentTreeCommand
    {
        protected abstract void ExecuteInner(SitecoreItem item);

        protected abstract void ExecuteInner(SitecoreTemplate item);
        
        protected override bool CanExecuteInner(ContentTreeContext context)
        {
            return context.OneItemSelected() && context.GetSelectedAsItemTree() != null;
        }
        
        protected override void ExecuteInner(ContentTreeContext context)
        {
            var itemTree = context.GetSelectedAsItemTree();

            if (itemTree.IsTemplate)
            {
                ExecuteInner(
                    new SitecoreTemplateBuilder(
                        new SitecoreDataService(context.GetSite().DataService),
                        new SitecoreItemBuilder(new SitecoreDataService(context.GetSite().DataService))).Build(itemTree.ItemUri));
            }
            else
            {
                ExecuteInner(new SitecoreItemBuilder(new SitecoreDataService(context.GetSite().DataService)).Build(itemTree.ItemUri));
            }
        }        
    }
}
