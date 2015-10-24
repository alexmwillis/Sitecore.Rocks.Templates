using System;
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
                .RemoveSpecialCharacters()
                .WithFirst(RemoveNumber)
                .StringJoin("");
        }

        public static string CamelCase(this string str)
        {
            return TitleCase(str)
                .RemoveSpecialCharacters()
                .WithFirst(RemoveNumber)
                .WithFirst(LowerCase)
                .StringJoin("");
        }

        private static IEnumerable<string> TitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo
                .ToTitleCase(str)
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }

        private static IEnumerable<string> RemoveSpecialCharacters(this IEnumerable<string> strs)
        {
            return strs
                .Select(s => s.Replace("-", "").Replace("_", ""))
                .Where(s => !string.IsNullOrWhiteSpace(s));
        }

        private static IEnumerable<string> WithFirst(this IEnumerable<string> strs, Func<string, string> withFirst)
        {
            var strsArray = strs.ToArray();
            var newFirst = withFirst(strsArray.First());
            return string.IsNullOrWhiteSpace(newFirst)
                ? strsArray.Skip(1)
                : new[] {newFirst}.Concat(strsArray.Skip(1));
        }

        private static string LowerCase(string str)
        {
            return str.ToLowerInvariant();
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
