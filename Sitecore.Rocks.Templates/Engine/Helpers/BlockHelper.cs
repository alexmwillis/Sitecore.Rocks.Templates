using HandlebarsDotNet;

namespace Sitecore.Rocks.Templates.Engine.Helpers
{
    public abstract class BlockHelper: Helper
    {
        public abstract HandlebarsBlockHelper BlockHelperFunction { get; }
    }
}
