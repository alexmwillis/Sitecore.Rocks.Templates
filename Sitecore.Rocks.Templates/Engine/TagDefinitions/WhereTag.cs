using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Mustache;

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
            var collection = GetCurrentAsType<IEnumerable<object>>(keyScope);
            var filterKey1 = arguments["filterKey1"] as string;
            var filterValue1 = arguments["filterValue1"] as string;

            IEnumerable<object> filteredCollection = FilterCollectionBy(collection, filterKey1, filterValue1);

            NestedContext context = new NestedContext()
            {
                KeyScope = keyScope.CreateChildScope(filteredCollection),
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
            yield return context;
        }

        private static T GetCurrentAsType<T>(Scope contextScope) where T : class
        {
            object @this;
            contextScope.TryFind("this", out @this);
            return @this as T;
        }

        private static IEnumerable<object> FilterCollectionBy(IEnumerable<object> collection, string key, string value)
        {
            return collection.Where(Filter(key));
        }

        private static Func<object, bool> Filter(string key)
        {
            return (o) => o.GetType().GetProperty(key)?.GetValue(o) != null;
        }
    }
}
