﻿module App

[<EntryPoint>]
let main argv =

    let feedReader = Twitter.FeedReader()
    let result =
        feedReader.Start()
        |> Async.Catch
        |> Async.RunSynchronously

    match result with
    | Choice1Of2 _ -> 0
    | Choice2Of2 e -> 
        printfn "%A" e
        1
