using System;
using System.Linq;

namespace Sitecore.Rocks.Templates.Extensions
{
    public static class StringExtensions
    {
        public static bool In(this string str, params string[] args)
        {
            return args.Contains(str, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
