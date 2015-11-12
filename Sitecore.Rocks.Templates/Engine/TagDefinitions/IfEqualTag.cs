using System.Collections.Generic;
using System.IO;
using Mustache;
using Sitecore.Rocks.Templates.Utils;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class IfEqualTag : ContentTagDefinition
    {
        public IfEqualTag() : base("ifEqual")
        {
        }

        protected override IEnumerable<TagParameter> GetParameters()
        {
            yield return new TagParameter("firstString") { IsRequired = true };
            yield return new TagParameter("secondString") { IsRequired = false };
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            yield break;
        }

        protected override IEnumerable<string> GetChildTags()
        {
            return new[] { "elseEqual" };
        }

        public override bool ShouldGeneratePrimaryGroup(Dictionary<string, object> arguments)
        {
            var first = arguments["firstString"];
            var second = arguments["secondString"];

            return IsConditionSatisfied(first, second);
        }

        private static bool IsConditionSatisfied(object first, object second)
        {
            return (first as string) == (second as string);
        }

        //public override IEnumerable<NestedContext> GetChildContext(
        //    TextWriter writer,
        //    Scope keyScope,
        //    Dictionary<string, object> arguments,
        //    Scope contextScope)
        //{
            

            

        //    var context = new NestedContext()
        //    {
        //        KeyScope = keyScope,
        //        Writer = writer,
        //        ContextScope = contextScope
        //    };
        //    yield return context;
        //}
    }
}
