module Sitecore.Rocks.Templates.FSharp.TemplateEngine

    open HandlebarsDotNet
    open System.IO
    open System

    type CompileSourceParamater = String of string | TextReader of TextReader
    type CompileOutput<'o> = StringFunction of Func<'o, string> | TextWriterFunction of Action<TextWriter, 'o>

    let Compile = fun (source:CompileSourceParamater) ->
            
            match source with
                | CompileSourceParamater.String s -> 
                    let a = Handlebars.Compile(s)
                    CompileOutput.StringFunction a
                | TextReader t -> 
                    let a = Handlebars.Compile(t)
                    CompileOutput.TextWriterFunction a
            
    let Render = fun data -> (fun (source:CompileSourceParamater) -> Compile source)(data)

    let RegisterTemplate = fun name partialTemplate ->
        
        Handlebars.RegisterTemplate(name, partialTemplate)
        |> ignore

    let RegisterPartial = fun name partialSource ->
        
        use reader = new StringReader (partialSource)
                    
        let partialTemplate = Handlebars.Compile(reader)

        RegisterTemplate name partialTemplate
        |> ignore
        
        