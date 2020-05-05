module App
open System

[<EntryPoint>]
let main argv =
    let indicator = 
        Data.Brazil.GetGdpGrowth()

    let graph = 
        indicator 
        |> Plot.Line 14 7
        |> Plot.AsString

    printfn "%s" graph

    0 // return an integer exit code
