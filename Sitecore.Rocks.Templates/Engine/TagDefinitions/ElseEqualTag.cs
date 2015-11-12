using System.Collections.Generic;
using Mustache;

namespace Sitecore.Rocks.Templates.Engine.TagDefinitions
{
    public class ElseEqualTag : ContentTagDefinition
    {
        public ElseEqualTag() : base("elseEqual")
        {
        }

        public override IEnumerable<TagParameter> GetChildContextParameters()
        {
            yield break;
        }

        protected override IEnumerable<string> GetClosingTags()
        {
            return new[] {"ifEqual"};
        }

        public override bool ShouldCreateSecondaryGroup(TagDefinition definition)
        {
            return true;
        }
    }
}
