module Sitecore.Rocks.Templates.FSharp.Utils

    open HandlebarsDotNet

    let CastAs<'T> (o:obj) = 
        match o with
          | :? 'T as res -> Some res 
          | _ ->  None