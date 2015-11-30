module Sitecore.Rocks.Templates.FSharp.TemplateEngine.HelperModule

    open HandlebarsDotNet
    
    let HelperError = fun helperName -> fun message ->

        HandlebarsException(sprintf "{{%s}} helper %s" helperName message)

    let private WithFirstArgument = fun (arguments:obj[]) helperError withFirst ->

        if arguments.Length = 0
            then raise (helperError "has to few arguments")
        string (withFirst arguments.[0])

    type Helper(helperName:string, fn: (string -> HandlebarsException) -> HandlebarsHelper) = 
        member this.Name = helperName
        member this.Function = fn (HelperError helperName)
