module Sitecore.Rocks.Templates.FSharp.Helpers

    open HandlebarsDotNet
    open Sitecore.Rocks.Templates.Extensions
    open System.Text.RegularExpressions

    type Helper(helperName:string, fn:HandlebarsHelper) = 
        member this.Name = helperName
        member this.Function = fn

    type BlockHelper(helperName:string, fn:HandlebarsBlockHelper) =
        member this.Name = helperName
        member this.Function = fn

    let WithFirstArgument (arguments:obj[]) (withFirst:obj -> string) =

        withFirst arguments.[0]

    let GetArgumentAs<'T> = fun (arguments:obj[]) index ->
        
        if index < arguments.Length then 
            Utils.CastAs<'T>(arguments.[index])
        else failwith (sprintf "only %i arguments, but %i required" arguments.Length (index + 1))

    let GetArgumentAsOptional<'T> = fun (arguments:obj[]) index ->
        
        if index < arguments.Length then 
            Some (Utils.CastAs<'T>(arguments.[index]))
        else 
            None        

    let GetPropertyValue propertyName o =

        let filterProperty = o.GetType().GetProperty(propertyName)
        if filterProperty <> null then 
            Some (filterProperty.GetValue(o))
        else
            None

    let Filter filterFunction filterKey o =
        match GetPropertyValue filterKey o with
            | Some x -> filterFunction x
            | None -> false

    let BooleanFilter = fun filterKey bool o ->

        let filterFunction (x:obj) =
            match x with            
                | :? bool as res -> res = bool
                | _ -> failwith "filter item is not a boolean"

        Filter filterFunction filterKey o 

    let IsNotNullOrWhiteSpaceFilter = fun filterKey o ->
                 
        let filterFunction (x:obj) =
            match x with
                | :? string as str -> str.Length > 0
                | null -> false 
                | _ -> true

        Filter filterFunction filterKey o 
        

    let RegexFilter = fun filterKey regex o ->

        let (|Match|_|) input =
            let m = Regex.Match(input, regex)
            if (m.Success) then Some input else None

        let filterFunction (x:obj) =
            match x with
                | :? string as res -> 
                match res with
                    | Match regex -> true
                    | _ -> false
                | _ -> failwith "filter item is not a string, so can't be matched to a regular expressions"

        Filter filterFunction filterKey o 

    let Init () =
    
        let helpers = [
        
            new Helper("camelCase", new HandlebarsHelper(fun output context arguments ->            

                output.Write(WithFirstArgument arguments (fun a -> Utils.CastAs<string>(a).ToCamelCase()))
            ));

            new Helper("pascalCase", new HandlebarsHelper(fun output context arguments ->            

                output.Write(WithFirstArgument arguments (fun a -> Utils.CastAs<string>(a).ToPascalCase()))
            ));

            new Helper("literal", new HandlebarsHelper(fun output context arguments ->            

                output.Write(WithFirstArgument arguments (fun a -> Utils.CastAs<string>(a).ToLiteral()))
            ))
        ]

        for helper in helpers do
            Handlebars.RegisterHelper(helper.Name, helper.Function)

        let blockHelpers = [            
            new BlockHelper("where", new HandlebarsBlockHelper(fun output options context arguments ->            

                let seq = GetArgumentAs<seq<obj>> arguments  0
                let filterKey = GetArgumentAs<string> arguments 1
                let filterValue = GetArgumentAsOptional<string> arguments 2
            
                let filter = match filterValue with
                                | Some "false" -> BooleanFilter filterKey false
                                | Some "true" -> BooleanFilter filterKey true
                                | None -> IsNotNullOrWhiteSpaceFilter filterKey
                                | Some str -> RegexFilter filterKey str
              
                options.Template.Invoke(output, Seq.filter filter seq)
            ))
                        
            new BlockHelper("equal", new HandlebarsBlockHelper(fun output options context arguments ->            

                let obj1 = GetArgumentAs<obj> arguments 0
                let obj2 = GetArgumentAs<obj> arguments 1

                if obj1 = obj2 then 
                    options.Template.Invoke(output, arguments)
                else
                    options.Inverse.Invoke(output, arguments)
            ));

            new BlockHelper("withFirst", new HandlebarsBlockHelper(fun output options context arguments ->            

                let seq = GetArgumentAs<seq<obj>> arguments 0

                options.Template.Invoke(output, Seq.head seq)
            ))
        ]

        for blockHelper in blockHelpers do
            Handlebars.RegisterHelper(blockHelper.Name, blockHelper.Function)
    
    Init()