module Graph
open Command
open ZedGraph
open System
open System.IO
open System.Drawing
open FSharp.Data

type Indicator = Runtime.WorldBank.Indicator

let createPane (title: string) =
    let pane = GraphPane()
    pane.Rect <- RectangleF(0.0f, 0.0f, 1200.0f, 675.0f)
    pane.Title.Text <- title
    pane.XAxis.Scale.MinGrace <- 0.0
    pane.XAxis.Scale.MaxGrace <- 0.0
    pane

let pointList (startDate:int option) (endDate:int option) (indicator:Indicator) = 
    let points = PointPairList()
    
    let dateFilter = 
        match startDate, endDate with
        | Some sd, Some ed -> fun (year, _) -> year >= sd && year <= ed
        | Some sd, None -> fun (year, _) -> year >= sd
        | None, Some ed -> fun (year, _) -> year <= ed
        | None, None -> fun (_) -> true

    for (year, value) in (indicator |> Seq.filter dateFilter ) do
        points.Add(float year, value)
    
    points

let addLineToPane  (startDate:int option) (endDate:int option) (pane:GraphPane) (indicator:Indicator)  =
    let points = pointList startDate endDate indicator
    let curve = pane.AddCurve(indicator.Code, points, Color.Blue, SymbolType.None)
    curve.Line.IsSmooth <- true
    curve.Line.IsAntiAlias <- true
    curve.Line.Width <- 3.0f
    ()

let toIndicator (country:Data.ICountry) (indicator:IndicatorType)   : Indicator =
    match indicator with
    | IndicatorType.Gdp -> country.GetGdp()
    | IndicatorType.GdpGrowth -> country.GetGdpGrowth()
    | IndicatorType.GdpPerCapita -> country.GetGdpPerCapita()
    | IndicatorType.AdultLiteracy -> country.GetAdultLiteracy()
    | IndicatorType.YoungLiteracy -> country.GetYouthLiteracy()
    | IndicatorType.Unenployment -> country.GetUnenployment()

let Line (command:Command):MemoryStream =
    let title = 
        let indicator = toIndicator (command.Countries |> Seq.head ) command.Indicator
        indicator.Name
    
    let pane = createPane(title)
    let addLine = addLineToPane (command.StartYear) (command.EndYear) pane

    for country in command.Countries do
        command.Indicator |> toIndicator country |> addLine
    
    use graph = Graphics.FromImage(new Bitmap(1200, 675))
    pane.AxisChange(graph)
    graph.Dispose()
    use bmp = pane.GetImage()
    use stream = new MemoryStream()
    bmp.Save(stream, Imaging.ImageFormat.Png)
    stream


      