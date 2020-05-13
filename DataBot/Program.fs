﻿module App
open System
open ZedGraph
open System.Drawing
open System.Drawing.Imaging



[<EntryPoint>]
let main argv =

    let data = Data.Brazil.GetGdp()
    let points = PointPairList()
    for (year, value) in (data |> Seq.filter (fun (y,_) -> y >= 2000 ) ) do
        points.Add(float year, value)

    let pane = new GraphPane()

    pane.Title.Text <- data.Name

    pane.XAxis.Scale.MinGrace <- 0.0
    pane.XAxis.Scale.MaxGrace <- 0.0

    let line = pane.AddCurve(data.Code, points, Color.Blue, SymbolType.None)
    line.Line.IsAntiAlias <- true
    line.Line.Width <- 1.0F
    let bmp = new Bitmap(10,10)
    let g = Graphics.FromImage(bmp);
    pane.AxisChange(g)

    let image = pane.GetImage()

    image.Save("test.png", ImageFormat.Png)

    0

    //let feedReader = Twitter.FeedReader()
    //let result =
    //    feedReader.Start()
    //    |> Async.Catch
    //    |> Async.RunSynchronously

    //match result with
    //| Choice1Of2 _ -> 0
    //| Choice2Of2 e -> 
    //    printfn "%A" e
    //    1