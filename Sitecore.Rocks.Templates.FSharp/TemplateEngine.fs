module Sitecore.Rocks.Templates.FSharp.TemplateEngine

    open HandlebarsDotNet
    open System.IO
    open System
    open Sitecore.Rocks.Templates.Extensions
    module Helper = Sitecore.Rocks.Templates.FSharp.TemplateEngine.Helper
    module Utils = Sitecore.Rocks.Templates.FSharp.TemplateEngine.Utils

    let Compile = fun (source:string) ->
            
        Handlebars.Compile(source)
            
    let Render = fun data -> (fun (source) -> Compile source)(data)

    let private RegisterTemplate = fun name partialTemplate ->
        
        Handlebars.RegisterTemplate(name, partialTemplate)        
        Handlebars

    let RegisterPartial = fun name partialSource ->
        
        use reader = new StringReader(partialSource)                    
        let partialTemplate = Handlebars.Compile(reader)
        RegisterTemplate name partialTemplate  

    let Init =
    
        let helpers = [
        
            new Helper.Helper("camelCase", fun helperError -> new HandlebarsHelper(fun output context arguments ->            

                output.Write(Helper.WithFirstArgument arguments helperError (fun a -> Utils.CastAs<string>(a).ToCamelCase()))
            ));

            new Helper.Helper("pascalCase", fun helperError -> new HandlebarsHelper(fun output context arguments ->            

                output.Write(Helper.WithFirstArgument arguments helperError (fun a -> Utils.CastAs<string>(a).ToPascalCase()))
            ));

            new Helper.Helper("literal", fun helperError -> new HandlebarsHelper(fun output context arguments ->            

                output.Write(Helper.WithFirstArgument arguments helperError (fun a -> Utils.CastAs<string>(a).ToLiteral()))
            ))]      

        for helper in helpers do
            Handlebars.RegisterHelper(helper.Name, helper.Function)

    Init
    