using System.Collections.Generic;
using HandlebarsDotNet;

namespace Sitecore.Rocks.Templates.Engine.Helpers
{
    public class HanderbarsHelperException : HandlebarsException
    {
        public HanderbarsHelperException(Helper helper, string message)
            : base($"{{{helper.Name}}} helper {{{message}}}")
        {
        }
    }

    public abstract class Helper
    {
        public abstract string Name { get; }
        
        protected T GetArgumentAs<T>(IReadOnlyList<object> arguments, int i)
            where T : class
        {
            if (arguments.Count <= i)
            {
                throw new HanderbarsHelperException(this, "has to few arguments");
            }
            return arguments[i] as T;
        }

        protected T GetOptionalArgumentAs<T>(IReadOnlyList<object> arguments, int i)
            where T : class
        {
            return (arguments.Count > i)
                ? arguments[i] as T
                : null;
        }
    }
}
