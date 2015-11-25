using System.Collections.Generic;
using System.IO;
using System.Linq;
using HandlebarsDotNet;

namespace Sitecore.Rocks.Templates.Engine.Helpers
{
    public class WithFirstHelper : BlockHelper
    {
        public override string Name => "withFirst";

        public override HandlebarsBlockHelper BlockHelperFunction
            => (output, options, context, arguments) =>
            {
                var enumerable = GetArgumentAs<IEnumerable<object>>(arguments, 0);

                var first = enumerable.FirstOrDefault();
                if (first != null)
                {
                    options.Template(output, first);
                }
            };
    }
}
