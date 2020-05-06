module App
open System

[<EntryPoint>]
let main argv =

    let result =
        "brazil usa gdp per capita 2000 2020"
        |> Command.Parse
        |> Command.Execute
        |> Seq.toList
    
    result    
    |> Seq.iter ( fun s -> printfn "%s" s )

    0 // return an integer exit code
