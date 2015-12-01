module Sitecore.Rocks.Templates.FSharp.TemplateEngine.Utils

    let CastAs<'T when 'T : null> (o:obj) = 
        match o with
          | :? 'T as res -> res
          | _ -> null

