module TemplateEngine.Engine2

    open HandlebarsDotNet
    
    let compile = fun (source:string) ->
        
        Handlebars.Compile(source)    

    let render = fun data -> (fun source -> compile source)(data)

        