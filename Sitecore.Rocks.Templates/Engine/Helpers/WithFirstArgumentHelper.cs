using System;
using HandlebarsDotNet;

namespace Sitecore.Rocks.Templates.Engine.Helpers
{
    public class WithFirstArgumentHelper: InlineHelper
    {
        private readonly Func<string, string> _withFirstArgument;

        public WithFirstArgumentHelper(string name, Func<string, string> withFirstArgument)
        {
            Name = name;
            _withFirstArgument = withFirstArgument;
        }

        public override string Name { get; }

        public override HandlebarsHelper HelperFunction => (output, context, arguments) =>
        {
            output.Write(_withFirstArgument(GetArgumentAs<string>(arguments, 0)));
        };
    }
}
