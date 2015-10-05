using System.Linq;
using Sitecore.Rocks.Templates.Data;

namespace Sitecore.Rocks.Templates.Formatters
{
    public static class Formatter
    {
        private static string RemoveWhiteSpace(this string str)
        {
            return str.Replace(" ", string.Empty);
        }

        public static string GetFieldSource(ISitecoreField field)
        {
            return $@"
    [SitecoreField(""{field.Name}"")]
    public string {field.Name.RemoveWhiteSpace()} {{ get; set; }}
";
        }

        public static string GetModelSource(ISitecoreItem item)
        {
            var itemName = item.Name.RemoveWhiteSpace();
            var templateKey = item.TemplateId;

            var fields = item.Fields
                .Where(f => !f.IsStandardField);

            var fieldSource = fields
                .Select(GetFieldSource)
                .Aggregate((i, j) => $"{i}{j}");

            return $@"
[SitecoreTemplate({templateKey})]
public class {itemName}Model : SitecoreItemModel
{{{fieldSource}}}";
        }
    }
}
