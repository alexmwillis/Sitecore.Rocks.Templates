using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Rocks.Templates.UI;

namespace ConsoleApplication1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var templates = new[]
            {
                new TemplateViewModel {DisplayName = "Name 1", FullName = "Full Name 1"},
                new TemplateViewModel {DisplayName = "Name 2", FullName = "Full Name 2"}
            };

            var vm = new SelectTemplateViewModel
            {
                Templates = templates,
                SelectedTemplate = templates.First()
            };

            var showDialogResult = new SelectTemplateWindow(vm).ShowDialog();
        }
    }
}
