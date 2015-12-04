module Sitecore.Rocks.Templates.FSharp.Utils

    open HandlebarsDotNet

    let CastAsOptional<'T> (o:obj) = 
        match o with
          | :? 'T as res -> Some res 
          | _ -> None

    let CastAs<'T> (o:obj) (def:'T) = 
        match o with
          | :? 'T as res -> res 
          | _ -> def