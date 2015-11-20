using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sitecore.Rocks.Templates.Utils
{
    public static class StringExtensions
    {
        public static bool In(this string str, params string[] args)
        {
            return args.Contains(str, StringComparer.InvariantCultureIgnoreCase);
        }

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

        public static string ToLiteralOld(this string str)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("JScript"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(str), writer, null);
                    return writer.ToString();
                }
            }
        }
        
        public static string ToLiteral(this string str)
        {
            return Regex.Replace(str, @"[\a\b\f\n\r\t\v\\'\\""]", Match);
        }
        
        private static string Match(Match match)
        {
            var map = new[]
            {
                new[] {"\a", @"\a"},
                new[] {"\b", @"\b"},
                new[] {"\f", @"\f"},
                new[] {"\n", @"\n"},
                new[] {"\r", @"\r"},
                new[] {"\t", @"\t"},
                new[] {"\v", @"\v"},
                new[] {"\\", @"\\"},
                new[] {"\0", @"\0"},
                new[] {"\"", "\\\""},
                new[] {"'", @"\'"}
            }.ToDictionary(i => i[0], j => j[1]);

            string mapped;
            if (map.TryGetValue(match.ToString(), out mapped))
            {
                return mapped;
            }
            
            throw new NotSupportedException();
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
