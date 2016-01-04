using System;

namespace Sitecore.Rocks.Templates.Extensions
{
    public static class GuidExtensions
    {
        public static bool CanParse(string value)
        {
            Guid result;
            return Guid.TryParse(value, out result);
        }
    }
}
