using System.Collections.Generic;
using HandlebarsDotNet;

namespace Sitecore.Rocks.Templates.Engine.Helpers
{
    public abstract class Helper
    {
        public abstract string Name { get; }
         
        protected  T GetArgumentAs<T>(IReadOnlyList<object> arguments, int i)
            where T : class
        {
            if (arguments.Count <= i)
            {
                ThrowHelperException("has to few arguments");
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

        public void ThrowHelperException(string message)
        {
            throw new HandlebarsException($"{{{Name}}} helper");
        }
    }
}
