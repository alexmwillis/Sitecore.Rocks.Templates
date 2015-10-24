using System.Linq;
using Sitecore.VisualStudio.ContentTrees;
using Sitecore.VisualStudio.ContentTrees.Items;

namespace Sitecore.Rocks.Templates.Commands
{
    public static class ContentTreeContextExtensions
    {
        public static bool OneItemSelected(this ContentTreeContext context)
        {
            return context.SelectedItems != null && context.SelectedItems.Count() == 1;
        }

        public static ItemTreeViewItem GetSelectedAsItemTree(this ContentTreeContext context)
        {
            return context.SelectedItems.First() as ItemTreeViewItem;
        }
    }
}
