module App
open System
open System.Windows.Forms.DataVisualization.Charting

[<EntryPoint>]
let main argv =

    let data = Data.Brazil.GetGdp()

    let chart = new Chart();
    chart.Palette <- ChartColorPalette.EarthTones
    
    let series = chart.Series.Add "TEST"
    
    series.ChartType <- SeriesChartType.Spline
    
    for (year, value) in data do
        series.Points.AddXY(string year, value) |> ignore
    
    let ca = new ChartArea()
    ca.AxisX <- new Axis()
    ca.AxisY <- new Axis()

    ca.BorderDashStyle <- ChartDashStyle.Solid

    chart.ChartAreas.Add ca

    chart.SaveImage("test.png", Drawing.Imaging.ImageFormat.Png)



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