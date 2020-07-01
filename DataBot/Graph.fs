module Graph
open ZedGraph
open System
open System.IO
open System.Drawing
open FSharp.Data

type Indicator = Runtime.WorldBank.Indicator
type ValueSeq<'X,'Y> = ('X*'Y) seq

let stringToColorHex (str:string):string =
    if( String.IsNullOrEmpty str ) then
        "#eeeeee"
    else
        let hash = str.GetHashCode() &&& 0x00FFFFFF
        sprintf "#%X" hash

let StringToColor (str:string):Color = 
    str |> stringToColorHex |> ColorTranslator.FromHtml
    

let createPane (title: string) =
    let pane = GraphPane()
    pane.Rect <- RectangleF(0.0f, 0.0f, 1200.0f, 675.0f)
    pane.Title.Text <- title
    pane.XAxis.Scale.MinGrace <- 0.0
    pane.XAxis.Scale.MaxGrace <- 0.0
    pane

let pointList (startValue:int option) (endValue:int option) indicator = 
    let points = PointPairList()
    
    let dateFilter = 
        match startValue, endValue with
        | Some sd, Some ed -> fun (value, _) -> value >= double sd && value <= double ed
        | Some sd, None -> fun (value, _) -> value >= double sd
        | None, Some ed -> fun (value, _) -> value <= double ed
        | None, None -> fun (_) -> true

    for (year, value) in (indicator |> Seq.filter dateFilter ) do
        points.Add(year, value)
    
    points

let addLineToPane  (startDate:int option) (endDate:int option) (pane:GraphPane) code indicator  =
    let points = pointList startDate endDate indicator
    let color = code |> StringToColor
    let curve = pane.AddCurve(code, points, color, SymbolType.None)
    curve.Line.IsSmooth <- true
    curve.Line.IsAntiAlias <- true
    curve.Line.Width <- 3.0f
    ()

let paneToStream (pane:GraphPane) =
    use graph = Graphics.FromImage(new Bitmap(1200, 675))
    pane.AxisChange(graph)
    graph.Dispose()
    use bmp = pane.GetImage()
    let stream = new MemoryStream()
    bmp.Save(stream, Imaging.ImageFormat.Png)
    stream

let Line title indicators startValue endValue isDate isLogX isLogY =
    let pane = createPane(title)
    pane.XAxis.MinorGrid.IsVisible <- true
    pane.XAxis.MajorGrid.IsVisible <- true
    pane.XAxis.MajorGrid.DashOff <- 0.0f
    pane.XAxis.MajorGrid.Color <- Color.Gray
    pane.YAxis.MajorGrid.IsVisible <- true
    pane.YAxis.MinorGrid.IsVisible <- true
    pane.YAxis.MajorGrid.DashOff <- 0.0f
    pane.YAxis.MajorGrid.Color <-Color.Gray
    if isDate then pane.XAxis.Type <- AxisType.Date
    if isLogY then pane.YAxis.Type <- AxisType.Log
    if isLogX then pane.XAxis.Type <- AxisType.Log
    let addLine = addLineToPane startValue endValue pane
    for legend, indicator in indicators do
        addLine legend indicator
    pane |> paneToStream



      