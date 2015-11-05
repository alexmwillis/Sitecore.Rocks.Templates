using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class WithFirstTag : ContentTagDefinition
    {
        public WithFirstTag()
            : base("withFirst")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            yield return new TagParameter("collection") { IsRequired = true };
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            yield return new TagParameter("context") { IsRequired = true };
        }

        public override IEnumerable<NestedContext> GetChildContext(
            TextWriter writer,
            Scope keyScope,
            Dictionary<string, object> arguments,
            Scope contextScope)
        {
            var enumerable = arguments["collection"] as IEnumerable<object>;

            if (enumerable == null) yield break;

            var context = new NestedContext()
            {
                KeyScope = keyScope.CreateChildScope(enumerable.First()),
                Writer = writer,
                ContextScope = contextScope.CreateChildScope()
            };
            yield return context;
        }
    }
}
