using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HandlebarsDotNet;
using Sitecore.Rocks.Templates.Extensions;

namespace Sitecore.Rocks.Templates.Engine.Helpers
{
    public class WhereHelper: BlockHelper
    {
        public override string Name => "where";

        public override HandlebarsBlockHelper BlockHelperFunction
            => (output, options, context, arguments) =>
            {
                var enumerable = GetArgumentAs<IEnumerable<object>>(arguments, 0);
                var filterKey = GetArgumentAs<string>(arguments, 1);
                var filterValue = GetOptionalArgumentAs<string>(arguments, 2);

                if (enumerable == null)
                {
                    throw new HanderbarsHelperException(this, "must be called with an enumerable");
                }

                if (filterKey == null)
                {
                    throw new HanderbarsHelperException(this, "must be called with key");
                }

                Func<object, bool> filter;
                if (filterValue.In(bool.FalseString, bool.TrueString))
                {
                    filter = BooleanFilter(filterKey, Convert.ToBoolean(filterValue));
                }
                else if (filterValue != null)
                {
                    filter = RegexFilter(filterKey, new Regex(filterValue));
                }
                else
                {
                    filter = IsNotNullOrWhiteSpaceFilter(filterKey);
                }

                options.Template(output, enumerable.Where(filter));
            };
        
        private static Func<object, bool> BooleanFilter(string key, bool @bool)
        {
            return o => Convert.ToBoolean(GetPropertyValue(key, o)) == @bool;
        }

        private static Func<object, bool> RegexFilter(string key, Regex match)
        {
            return o => IsNotNullOrWhiteSpaceFilter(key)(o) &&
                        match.IsMatch(GetPropertyValueAsString(key, o));
        }

        private static Func<object, bool> IsNotNullOrWhiteSpaceFilter(string key)
        {
            return o => !string.IsNullOrWhiteSpace(GetPropertyValueAsString(key, o));
        }

        private static string GetPropertyValueAsString(string key, object o)
        {
            return GetPropertyValue(key, o) as string ?? string.Empty;
        }

        private static object GetPropertyValue(string key, object o)
        {
            return o.GetType().GetProperty(key)?.GetValue(o);
        }
    }
}
