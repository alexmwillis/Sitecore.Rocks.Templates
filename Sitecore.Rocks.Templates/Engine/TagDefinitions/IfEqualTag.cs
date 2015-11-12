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
            yield return "elseEqual";
        }

        public override bool ShouldGeneratePrimaryGroup(Dictionary<string, object> arguments)
        {
            var first = arguments["firstString"];
            var second = arguments["secondString"];

            return IsConditionSatisfied(first, second);
        }
        
        public override bool ShouldCreateSecondaryGroup(TagDefinition definition)
        {
            return definition.Name == "elseEqual";
        }

        private static bool IsConditionSatisfied(object first, object second)
        {
            return (first as string) == (second as string);
        }
    }
}
