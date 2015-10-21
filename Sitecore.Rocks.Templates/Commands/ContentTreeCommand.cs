using System;
using System.Windows;
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
            try
            {
                var context = parameter as ContentTreeContext;

                if (context != null) ExecuteInner(context);
            }
            catch (Exception e)
            {
                AppHost.MessageBox(e.Message, "Error executing command", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
