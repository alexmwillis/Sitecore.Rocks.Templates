using System.Collections;
using System.Collections.Generic;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public static class ScopeExtensions
    {
        public static IEnumerable<object> GetCurrentAsEnumerable(this Scope contextScope)
        {
            var enumerable = GetCurrentAsType<IEnumerable>(contextScope);
            if (enumerable == null) yield break;
            foreach (var item in enumerable)
            {
                yield return item;
            }
        }

        public static T GetCurrentAsType<T>(this Scope contextScope) where T : class
        {
            object @this;
            contextScope.TryFind("this", out @this);
            return @this as T;
        }
    }
}
