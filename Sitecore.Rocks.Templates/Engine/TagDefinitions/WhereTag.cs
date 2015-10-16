using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using Mustache;
using System.Text.RegularExpressions;

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
            var collection = GetCurrentAsEnumerable(keyScope);
            var filterKey1 = arguments["filterKey1"] as string;
            var filterValue1 = arguments["filterValue1"] as string;

            IEnumerable<object> filteredCollection;

            if (filterValue1 != null) 
            {
                filteredCollection = FilterCollectionBy(collection, filterKey1, new Regex(filterValue1));
            }
            else 
            {
                filteredCollection = FilterCollectionBy(collection, filterKey1, null);
            }

            NestedContext context = new NestedContext()
            {
                KeyScope = keyScope.CreateChildScope(filteredCollection),
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
            yield return context;
        }

        private static IEnumerable<object> GetCurrentAsEnumerable(Scope contextScope)
        {
            var enumerable = GetCurrentAsType<IEnumerable>(contextScope);
            if (enumerable == null) yield break;
            foreach (var item in enumerable)
            {
                yield return item;
            }
        }

        private static T GetCurrentAsType<T>(Scope contextScope) where T: class
        {
            object @this;
            contextScope.TryFind("this", out @this);
            return @this as T;
        }

        private static IEnumerable<object> FilterCollectionBy(IEnumerable<object> collection, string property, Regex match = null)
        {
            return (match != null) 
                ? collection.Where(RegexFilter(property, match))
                : collection.Where(IsNullOrWhiteSpaceFilter(property));
        }

        private static Func<object, bool> RegexFilter(string key, Regex match)
        {
            return (o) => 
            {                
                var propertyValue = o.GetType().GetProperty(key)?.GetValue(o) as string;
                return IsNullOrWhiteSpaceFilter(key)(o) && match.IsMatch(propertyValue);
            };
        }        

        private static Func<object, bool> IsNullOrWhiteSpaceFilter(string key)
        {
            return (o) => 
            {
                var propertyValue = o.GetType().GetProperty(key)?.GetValue(o) as string;
                return !string.IsNullOrWhiteSpace(propertyValue);
            };
        }
    }
}
