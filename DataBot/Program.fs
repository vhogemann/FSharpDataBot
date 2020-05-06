module App

[<EntryPoint>]
let main argv =

    let result =
        "china gdp per capita 2000 2020"
        |> Command.Parse
        |> Plot.Line 14 7
    result    
    |> Seq.iter ( 
        fun s -> 
            printfn "%s" s )

    0 // return an integer exit code
