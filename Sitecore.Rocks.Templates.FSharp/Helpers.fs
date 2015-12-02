
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
        
        Utils.CastAs<'T>(arguments.[index])

    let BooleanFilter = fun filterKey bool o ->

        let filterItem = o.GetType().GetProperty(filterKey).GetValue(o)
        match filterItem with
            | :? bool as res -> res = bool
            | _ -> failwith "filter item is not a boolean"

    let IsNotNullOrWhiteSpaceFilter = fun filterKey o ->
        
        let filterItem = o.GetType().GetProperty(filterKey).GetValue(o)
        match filterItem with
            | :? string as res -> res.Length > 0
            | null -> false 
            | _ -> true

    let RegexFilter = fun filterKey regex o ->

        let (|Match|_|) input =
            let m = Regex.Match(input, regex)
            if (m.Success) then Some input else None

        let filterItem = o.GetType().GetProperty(filterKey).GetValue(o)
        match filterItem with
            | :? string as res -> 
            match res with
                | Match regex -> true
                | _ -> false
            | _ -> failwith "filter item is not a string, so can't be matched to a regular expressions"

    let WhereHelper = new BlockHelper("where", new HandlebarsBlockHelper(fun output options context arguments ->            

            let list = GetArgumentAs<list<obj>> arguments  0
            let filterKey = GetArgumentAs<string> arguments 1
            let filterValue = GetArgumentAs<string> arguments 2
            
            let filter = match filterValue with
                            | "False" -> BooleanFilter filterKey false
                            | "True" -> BooleanFilter filterKey true
                            | "" -> IsNotNullOrWhiteSpaceFilter filterKey
                            | _ as str -> RegexFilter filterKey str
              
            options.Template.Invoke(output, List.filter(filter))
        ))

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
            ))]      

        for helper in helpers do
            Handlebars.RegisterHelper(helper.Name, helper.Function)

        let blockHelpers = [            
            WhereHelper
        ]

        for blockHelper in blockHelpers do
            Handlebars.RegisterHelper(blockHelper.Name, blockHelper.Function)
    
    Init()