module Sitecore.Rocks.Templates.FSharp.Utils

    let CastAs<'T> (o:obj) = 
        match o with
          | :? 'T as res -> res 
          | _ -> failwith (sprintf "unable to cast %s to type %s" (o.GetType().ToString()) (typeof<'T>.ToString()))