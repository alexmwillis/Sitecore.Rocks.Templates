using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Sitecore.Rocks.Templates.Extensions
{
    public static class StringExtensions
    {
        public static string PascalCase(this string str)
        {
            return TitleCase(str)
                .RemoveNumberFirst()
                .StringJoin("");
        }

        public static string CamelCase(this string str)
        {
            return TitleCase(str)
                .RemoveNumberFirst()
                .LowerCaseFirst()
                .StringJoin("");
        }

        private static string[] TitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo
                .ToTitleCase(str)
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }

        private static string[] LowerCaseFirst(this string[] strs)
        {
            return new[] {strs.First().ToLowerInvariant()}.Concat(strs.Skip(1)).ToArray();
        }

        private static string[] RemoveNumberFirst(this string[] strs)
        {
            return new[] {RemoveNumber(strs.First())}
                .Concat(strs.Skip(1))
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }

        private static string RemoveNumber(string str)
        {
            return char.IsNumber(str[0])
                ? new string(str
                    .Reverse()
                    .TakeWhile(c => !char.IsNumber(c))
                    .Reverse()
                    .ToArray())
                : str;
        }

        private static string StringJoin(this IEnumerable<string> strs, string separator)
        {
            return string.Join(separator, strs);
        }
    }
}
