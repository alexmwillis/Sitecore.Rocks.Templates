using System.Collections.Generic;
using System.IO;
using System.Linq;
using HandlebarsDotNet;
using Sitecore.Rocks.Templates.Utils;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngine : ITemplateEngine
    {
        public TemplateEngine()
        {
            Handlebars.RegisterHelper("camelCase", (writer, context, parameters) => {
                writer.WriteSafeString((parameters[0] as string).CamelCase());
            });
            Handlebars.RegisterHelper("where", (output, options, context, arguments) =>
            {
                var enumerable = arguments[0] as IEnumerable<object>;
                if (enumerable == null)
                {
                    throw new HandlebarsException("{{where}} helper must be called with an enumerable");
                }
                options.Template(output, enumerable.Take(3));
            });

            //_compiler.RegisterTag(new PascelCaseTag(), false);
            //_compiler.RegisterTag(new WhereTag(), false);
            //_compiler.RegisterTag(new WithFirstTag(), false);
            //_compiler.RegisterTag(new NewGuidTag(), false);
            //_compiler.RegisterTag(new IfEqualTag(), false);
            //_compiler.RegisterTag(new ElseEqualTag(), false);
        }

        public string Render(string source, object data)
        {
            return Handlebars.Compile(source)(data);
        }

        public void RegisterPartial(string name, string partialSource)
        {
            using (var reader = new StringReader(partialSource))
            {
                var partialTemplate = Handlebars.Compile(reader);
                Handlebars.RegisterTemplate(name, partialTemplate);
            }
        }
    }
}
