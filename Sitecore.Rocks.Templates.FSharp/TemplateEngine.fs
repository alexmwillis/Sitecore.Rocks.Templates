module Sitecore.Rocks.Templates.FSharp.TemplateEngine

    open HandlebarsDotNet
    open System.IO

    let compile = fun (source:TextReader) ->
        
        Handlebars.Compile(source)

    let compile = fun (source:string) ->
        
        Handlebars.Compile(source)

    let render = fun data -> (fun source -> compile source)(data)

    let registerTemplate = fun name partialTemplate ->
        
        Handlebars.RegisterTemplate(name, partialTemplate)
        |> ignore

    let registerPartial = fun name partialSource ->
        
        use reader = new StringReader (partialSource)
                    
        let partialTemplate = compile(reader)

        registerTemplate name partialTemplate
        |> ignore
        
        