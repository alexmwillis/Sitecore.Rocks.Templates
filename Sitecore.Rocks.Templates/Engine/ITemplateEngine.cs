﻿using Sitecore.Rocks.Templates.IO;

namespace Sitecore.Rocks.Templates.Engine
{
    public interface ITemplateEngine
    {
        string Render(string template, object source);
    }
}