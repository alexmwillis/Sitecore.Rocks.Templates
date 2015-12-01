module Sitecore.Rocks.Templates.FSharp.TemplateEngine.Helper

    open HandlebarsDotNet
    
    let HelperError = fun helperName -> fun message ->

        HandlebarsException(sprintf "{{%s}} helper %s" helperName message)

    type Helper(helperName:string, fn: (string -> HandlebarsException) -> HandlebarsHelper) = 
        member this.Name = helperName
        member this.Function = fn (HelperError helperName)

    type BlockHelper(helperName:string, fn: (string -> HandlebarsException) -> HandlebarsBlockHelper) =
        member this.Name = helperName
        member this.Function = fn (HelperError helperName)

    let WithFirstArgument = fun (arguments:obj[]) helperError (withFirst:obj -> string) ->

        if arguments.Length = 0
            then raise (helperError "has to few arguments")
        withFirst arguments.[0]

    let GetArgumentAs<'T when 'T : null> = fun (arguments:obj[]) index ->
        
        let Utils.CastAs<'T>(arguments.[index])

    let WhereHelper = new BlockHelper("where", fun helperError -> new HandlebarsBlockHelper(fun output options context arguments ->            

        let list = GetArgumentAs<list<obj>>(arguments, 0)
        let filterKey = GetArgumentAs<list>(arguments, 1, helperError)
        let filterValue = GetOptionalArgumentAs<list>(arguments, 2)
    
        let filter = match filterValue with
                        | "False" -> BooleanFilter(filterKey, false)
                        | "True" -> BooleanFilter(filterKey, true)
                        | "" -> IsNotNullOrWhiteSpaceFilter(filterKey)
                        | _ as str -> RegexFilter(list, new Regex(str))
                        

        options.Template(output, List.filter(filter))
        ))