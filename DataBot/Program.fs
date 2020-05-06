module App
open System

[<EntryPoint>]
let main argv =

    let result =
        "brazil gdp per capita 2000 2020"
        |> Command.Parse
        |> Command.Execute
        |> Seq.map (Plot.line 14 7 >> Plot.AsString)
        |> Seq.toList
    result    
    |> Seq.iter ( fun s -> printfn "%s" s )

    0 // return an integer exit code
