using System.IO;
using HandlebarsDotNet;
using Sitecore.Rocks.Templates.Engine.Helpers;
using Sitecore.Rocks.Templates.Extensions;

namespace Sitecore.Rocks.Templates.Engine
{
    public class TemplateEngine : ITemplateEngine
    {
        public TemplateEngine()
        {
            Handlebars.RegisterHelper("camelCase",
                new WithFirstArgumentHelper("camelCase", s => s.ToCamelCase()).HelperFunction);
            Handlebars.RegisterHelper("pascalCase",
                new WithFirstArgumentHelper("pascalCase", s => s.ToPascalCase()).HelperFunction);
            Handlebars.RegisterHelper("literal",
                new WithFirstArgumentHelper("literal", s => s.ToLiteral()).HelperFunction);
            Handlebars.RegisterHelper("where", 
                new WhereHelper().BlockHelperFunction);
            Handlebars.RegisterHelper("withFirst", 
                new WithFirstHelper().BlockHelperFunction);

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
