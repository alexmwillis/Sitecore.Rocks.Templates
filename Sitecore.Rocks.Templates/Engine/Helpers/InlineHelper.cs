using HandlebarsDotNet;

namespace Sitecore.Rocks.Templates.Engine.Helpers
{
    public abstract class InlineHelper: Helper
    {
        public abstract HandlebarsHelper HelperFunction { get; }
    }
}
