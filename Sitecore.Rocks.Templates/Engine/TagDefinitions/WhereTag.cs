using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mustache;
using System.Text.RegularExpressions;
using Sitecore.Rocks.Templates.Extensions;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class WhereTag : ContentTagDefinition
    {
        public WhereTag()
            : base("where")
        {
        }

        protected override bool GetIsContextSensitive()
        {
            return false;
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            yield return new TagParameter("filterKey1") {IsRequired = true};
            yield return new TagParameter("filterValue1") {IsRequired = false};
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            yield return new TagParameter("context") {IsRequired = true};
        }

        public override IEnumerable<NestedContext> GetChildContext(
            TextWriter writer,
            Scope keyScope,
            Dictionary<string, object> arguments,
            Scope contextScope)
        {
            var collection = keyScope.GetCurrentAsEnumerable();
            var filterKey1 = arguments["filterKey1"] as string;
            var filterValue1 = arguments["filterValue1"] as string;

            var regex = filterValue1 != null ? new Regex(filterValue1) : null;

            var filteredCollection = FilterCollectionBy(collection, filterKey1, regex);

            var context = new NestedContext()
            {
                KeyScope = keyScope.CreateChildScope(filteredCollection),
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
            yield return context;
        }

        private static IEnumerable<object> FilterCollectionBy(IEnumerable<object> collection, string property,
            Regex match)
        {
            return (match != null)
                ? collection.Where(IsNotNullOrWhiteSpaceFilter(property)).Where(RegexFilter(property, match))
                : collection.Where(IsNotNullOrWhiteSpaceFilter(property));
        }

        private static Func<object, bool> RegexFilter(string key, Regex match)
        {
            return o => match.IsMatch(GetPropertyValueAsString(key, o));
        }

        private static Func<object, bool> IsNotNullOrWhiteSpaceFilter(string key)
        {
            return o => !string.IsNullOrWhiteSpace(GetPropertyValueAsString(key, o));
        }

        private static string GetPropertyValueAsString(string key, object o)
        {
            return o.GetType().GetProperty(key)?.GetValue(o) as string ?? string.Empty;
        }
    }
}
