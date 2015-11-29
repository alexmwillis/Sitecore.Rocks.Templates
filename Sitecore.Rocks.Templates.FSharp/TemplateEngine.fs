module Sitecore.Rocks.Templates.FSharp.TemplateEngine

    open HandlebarsDotNet
    open System.IO
    open System

    let Compile = fun (source:string) ->
            
        Handlebars.Compile(source)
            
    let Render = fun data -> (fun (source) -> Compile source)(data)

    let private RegisterTemplate = fun name partialTemplate ->
        
        Handlebars.RegisterTemplate(name, partialTemplate)
        |> ignore

    let RegisterPartial = fun name partialSource ->
        
        use reader = new StringReader(partialSource)
                    
        let partialTemplate = Handlebars.Compile(reader)

        RegisterTemplate name partialTemplate
        |> ignore

    let HelperError = fun helperName -> fun message ->
        HandlebarsException(sprintf "{{%s}} helper %s" helperName message)

    let private WithFirstArgument = fun (arguments:obj[]) helperName withFirst ->

        if arguments.Length = 0
            then raise (HelperError helperName "has to few arguments")
        withFirst arguments.[0]    

    let private RegisterHelper = fun name (helperFunction:HandlebarsHelper) ->

        Handlebars.RegisterHelper(name, helperFunction)
        |> ignore         

    let Init =
    
        let helper = new HandlebarsHelper(fun output context arguments ->            

            output.Write(WithFirstArgument arguments "camelCase" (fun a -> a)) |> ignore
        )

        RegisterHelper "camelCase" helper
    