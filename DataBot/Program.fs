module App
open System
    
[<EntryPoint>]
let main argv =
    let indicator = 
        Data.USA.GetGdpPerCapita()

    printfn "%s" indicator.IndicatorCode
    let graph = 
        indicator 
        //|> Seq.filter( fun (y,_) -> y > 1980 )
        |> Chart.Line 8 24

    printfn "%s - %s" indicator.Code indicator.Name

    graph
    |> Seq.iter ( fun line -> 
        line |> Seq.iter (printf "%s")
        printfn "" )

    0 // return an integer exit code
