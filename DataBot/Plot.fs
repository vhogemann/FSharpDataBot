module Plot
open Command
open System
open System.Text
open FSharp.Data

let private stretch (width:int) (values: float list) =
    let length = values.Length
    // how often do we need to insert an extra item
    let interval = width / length

    let expanded = seq {
        for i in 1..length do
            let current = values.[i - 1]
            yield current
            if ((i <> 1) && (i <> length)) then
                let next = values.[i]
                let slope = (next - current) / float interval
                for j in 1 .. interval do
                    yield current + (float j * slope)
    }
    expanded |> Seq.toList

let private shrink (width:int) (values: float list) =
    let length = values.Length
    let ratio = length / width

    seq {
        for i in 1..width do
            yield seq { 
                let start = ratio * i
                let finish =
                    if (start + ratio < length) then
                        start + ratio
                    else
                        length
                for j in start..finish do
                    yield values.[j-1]
                } 
    }
    |> Seq.map Seq.average
    |> Seq.toList

let resize (width:int) (values:float list) =
    let length = values.Length
    if length > width then
        shrink width values
    elif length < width then
        stretch width values
    else
        values

let private plotColumn height value =
    let rec bar index (acc:string list) value =
        match index, value with
        | i, _ when i >= height -> acc
        | i, v when i = v -> bar (index+1) (acc @ ["🔴"]) value
        | _, _ -> bar (index + 1) (acc @ ["⬜"]) value

    bar 0 [] value

let private numberFormat = function
    | x when x < 10.0 ** 3.0 -> sprintf "%.1f" x
    | x when x < 10.0 ** 6.0 -> sprintf "%.1fK" (x/10.0 ** 3.0)
    | x when x < 10.0 ** 9.0 -> sprintf "%.1fM" (x/10.0 ** 6.0)
    | x when x < 10.0 ** 12.0 -> sprintf "%.1fB" (x/10.0 ** 9.0)
    | x when x < 10.0 ** 15.0 -> sprintf "%.1fT" (x/10.0 ** 12.0)
    | x -> sprintf "%.1f" x

let line (width:int) (height:int) (indicator:(int*float)seq) =
    let (_, max) =
        indicator
        |> Seq.maxBy (fun (_, value) -> value)

    let scale = max / ( float height)
    
    let barFun value =
        let index = (value / scale |> Convert.ToInt32) - 1   
        plotColumn height index |> List.rev |> List.toArray

    let scales = 
        seq { for i in 1 .. height do yield float(i) * scale }
        |> Seq.mapi ( fun (i)(x) -> if i % 2 <> 0 then "" else sprintf " %s" (numberFormat x) )
        |> Seq.rev |> Seq.toArray


    let tmp =
        indicator
        |> Seq.map ( fun (_, v) -> v)
        |> Seq.toList |> resize width
        |> Seq.map barFun
    [scales] |> Seq.append tmp |> Seq.transpose

let graphToString (data: string seq seq) =
    let foldFun (acc:StringBuilder) (row:string seq) =
        let rowString = row |> String.concat ""
        acc.Append(rowString).Append("\n")
    let fold =
        data
        |> Seq.fold foldFun (new StringBuilder())
    fold.ToString()


let Line (width:int) (height:int) (command:GraphCommand) =
    let getIndicator (country:Data.ICountry) (indicator:IndicatorType) : Runtime.WorldBank.Indicator =
        match indicator with
        | IndicatorType.Gdp -> country.GetGdp()
        | IndicatorType.GdpGrowth -> country.GetGdpGrowth()
        | IndicatorType.GdpPerCapita -> country.GetGdpPerCapita()
        | IndicatorType.AdultLiteracy -> country.GetAdultLiteracy()
        | IndicatorType.YoungLiteracy -> country.GetYouthLiteracy()
        | IndicatorType.Unenployment -> country.GetUnenployment()

    let periodFilter =
        let yearMin =
            match command.Year with
            | [] -> None
            | years -> years |> List.min |> Some
        let yearMax =
            match command.Year with
            | [] -> None
            | years -> years |> List.max |> Some
        match yearMin, yearMax with
        | Some min, Some max when min < max ->
            Seq.filter (fun (y, _) -> y >= min && y <= max )
        | _ -> id     

    let generateLabel (country:Data.ICountry) (indicator:Runtime.WorldBank.Indicator) : string =
        let startYear, endYear =
            let years = indicator |> Seq.map( fun (year,_) -> year)
            years |> Seq.max, years |> Seq.min
        let flagEmoji = country.FlagEmoji
        let indicatorName = indicator.Name
        sprintf "%s %i-%i %s" flagEmoji startYear endYear indicatorName

    seq { for country in command.Country do
            for indicatorCmd in command.Indicator do
                let indicator = getIndicator country indicatorCmd
                let data = indicator |> periodFilter
                let label = generateLabel country indicator
                let graph = line width height data |> graphToString
                yield sprintf "%s\n%s" graph label }
   