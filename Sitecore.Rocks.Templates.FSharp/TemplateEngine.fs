module Sitecore.Rocks.Templates.FSharp.TemplateEngine

    open HandlebarsDotNet
    open System.IO
    open System
    open Sitecore.Rocks.Templates.Extensions

    let CastAs<'T when 'T : null> (o:obj) = 
        match o with
          | :? 'T as res -> res
          | _ -> null

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
        
            new Helper("camelCase", fun helperError -> new HandlebarsHelper(fun output context arguments ->            

                output.Write(WithFirstArgument arguments helperError (fun a -> CastAs<string>(a).ToCamelCase()))
            ));

            new Helper("pascalCase", fun helperError -> new HandlebarsHelper(fun output context arguments ->            

                output.Write(WithFirstArgument arguments helperError (fun a -> CastAs<string>(a).ToPascalCase()))
            ));

            new Helper("literal", fun helperError -> new HandlebarsHelper(fun output context arguments ->            

                output.Write(WithFirstArgument arguments helperError (fun a -> CastAs<string>(a).ToLiteral()))
            ))]      

        for helper in helpers do
            Handlebars.RegisterHelper(helper.Name, helper.Function)

    Init
    