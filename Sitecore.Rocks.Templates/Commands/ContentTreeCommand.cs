using Sitecore.VisualStudio.Commands;
using Sitecore.VisualStudio.ContentTrees;

namespace Sitecore.Rocks.Templates.Commands
{
    [Command]
    public abstract class ContentTreeCommand : CommandBase
    {
        protected abstract bool CanExecuteInner(ContentTreeContext parameter);

        protected abstract void ExecuteInner(ContentTreeContext context);

        public override bool CanExecute(object parameter)
        {
            var context = parameter as ContentTreeContext;

            return context != null && CanExecuteInner(context);
        }
        
        public override void Execute(object parameter)
        {
            var context = parameter as ContentTreeContext;

            if (context != null) ExecuteInner(context);
        }
    }
}
