using System.IO;
using HandlebarsDotNet;
using Sitecore.Rocks.Templates.Engine.Helpers;
using Sitecore.Rocks.Templates.Utils;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngine : ITemplateEngine
    {
        public TemplateEngine()
        {
            Handlebars.RegisterHelper("camelCase", (writer, context, parameters) => {
                writer.Write((parameters[0] as string).CamelCase());
            });
            Handlebars.RegisterHelper("pascalCase", (writer, context, parameters) => {
                writer.Write((parameters[0] as string).PascalCase());
            });
            Handlebars.RegisterHelper("where", WhereHelper.GetHelper);

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
