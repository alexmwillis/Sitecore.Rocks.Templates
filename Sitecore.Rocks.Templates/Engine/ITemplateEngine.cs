using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Rocks.Templates.Data;

namespace Sitecore.Rocks.Templates.Engine
{
    public interface ITemplateEngine
    {
        string Render(string template, object source);
    }
}
