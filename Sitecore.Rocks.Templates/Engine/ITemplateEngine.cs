using System;
using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Engine
{
    public interface ITemplateEngine
    {
        string Render(string template, object source);

        void RegisterPartial(string name, Func<string> getPartial);
    }
}
