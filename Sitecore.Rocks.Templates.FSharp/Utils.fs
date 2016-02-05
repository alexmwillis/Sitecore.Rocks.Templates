module Sitecore.Rocks.Templates.FSharp.Utils
    
    let CastAs<'T> (o:obj) = 
        match o with
          | :? 'T as res -> Some res 
          | _ -> None 