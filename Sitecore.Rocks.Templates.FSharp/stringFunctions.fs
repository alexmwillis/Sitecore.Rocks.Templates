module Sitecore.Rocks.Templates.FSharp.StringFunctions

    open System
    open System.Globalization

    let private ToTitleCase string =
        CultureInfo.CurrentCulture.TextInfo.ToTitleCase string
        
    let private IsSpecialCharacter char = 
        List.contains char ['-';'_']

    let private RemoveSpecialCharacters string =
        String.filter (IsSpecialCharacter >> not) string

    let private RemoveWhiteSpace string =
        String.filter (fun c -> c <> ' ') string

    let private CharsToString (chars:char list) =
        String.Concat(Array.ofList(chars))

    let rec private RemoveNumberAtStart string =        
        match [for c in string -> c] with
            | head :: tail when Char.IsNumber head -> RemoveNumberAtStart (CharsToString tail)
            | _ as x -> CharsToString x
           
    let private LowerCaseFirstChar (string:string) =
        match [for c in string -> c] with
            | head :: tail -> head.ToString().ToLower() + CharsToString tail
            | [] -> ""

    let ToPascalCase = 
        ToTitleCase >> 
        RemoveWhiteSpace >> 
        RemoveSpecialCharacters >> 
        RemoveNumberAtStart
        
    let ToCamelCase = 
        ToPascalCase >>
        LowerCaseFirstChar

    let CharToLiteral char =
        match char with
            | '\a' -> @"\a"
            | '\b' -> @"\b"
            | '\f' -> @"\f"
            | '\n' -> @"\n"
            | '\r' -> @"\r"
            | '\t' -> @"\t"
            | '\v' -> @"\v"
            | '\\' -> @"\\"
            | '\"' -> @"\"""
            | '\'' -> @"\'"
            | _ as other -> other.ToString()

    let ToLiteral string = 
        [for c in string -> c] |> Seq.fold (fun acc c -> acc + CharToLiteral c) String.Empty


        