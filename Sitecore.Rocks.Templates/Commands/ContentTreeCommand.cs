using Sitecore.VisualStudio.Commands;
using Sitecore.VisualStudio.ContentTrees;

namespace Sitecore.Rocks.Templates.Commands
{
    [Command]
    public abstract class ContentTreeCommand : CommandBase
    {
        protected abstract bool CanExecute(ContentTreeContext parameter);

        public override bool CanExecute(object parameter)
        {
            var context = parameter as ContentTreeContext;

            return context != null && CanExecute(context);
        }

        protected abstract void Execute(ContentTreeContext context);

        public override void Execute(object parameter)
        {
            var context = parameter as ContentTreeContext;

            if (context != null) Execute(context);
        }
    }
}
