module Sitecore.Rocks.Templates.FSharp.TemplateEngine

    open HandlebarsDotNet
    open System.IO
    
    let Compile (source:string) =
            
        Handlebars.Compile(source)

    let private RegisterTemplate  name partialTemplate =
        
        Handlebars.RegisterTemplate(name, partialTemplate)        
        Handlebars

    let RegisterPartial name partialSource =
        
        use reader = new StringReader(partialSource)                    
        let partialTemplate = Handlebars.Compile(reader)
        RegisterTemplate name partialTemplate  

        |> ignore