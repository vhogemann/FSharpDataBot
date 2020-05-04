module App
open System
    
[<EntryPoint>]
let main argv =
    let indicator = 
        Data.Brazil.GetGdpPerCapita()

    printfn "%s" indicator.IndicatorCode
    let graph = 
        indicator 
        |> Seq.filter( fun (y,_) -> y >= 2000 && y <= 2020 )
        |> Chart.Line 6 16
        |> Chart.AsString

    0 // return an integer exit code
