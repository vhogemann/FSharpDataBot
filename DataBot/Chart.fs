﻿module Chart
open System

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

    // how far the value lenngth is from being a multiple of the width?
    let distance = width - (length % width)
    // how often do we need to insert an extra item
    let interval = length / distance

    let expanded = seq {
        for i in 1..length do
            yield values.[i - 1]
            if ((i <> 1) && (i <> length) && (i % distance = 0)) then 
                yield values.[i - 1]
    }

    let chunk = ((length + distance) / width)

    expanded
    |> Seq.toList
    |> List.chunkBySize chunk
    |> List.map(List.average)
    
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
    | x when x < 10.0 ** 6.0 -> sprintf "%.0fK" (x/10.0 ** 3.0)
    | x when x < 10.0 ** 9.0 -> sprintf "%.0fM" (x/10.0 ** 6.0)
    | x when x < 10.0 ** 12.0 -> sprintf "%.1fB" (x/10.0 ** 9.0)
    | x when x < 10.0 ** 15.0 -> sprintf "%.1fT" (x/10.0 ** 12.0)
    | x -> sprintf "%.1f" x

let Line (height:int) (width:int) (indicator:(int*float)seq) =
    let (_, max) =
        indicator
        |> Seq.maxBy (fun (_, value) -> value)

    let scale = max / ( float height)
    
    let barFun value =
        let index = (value / scale |> Convert.ToInt32) - 1   
        plotColumn height index |> List.rev |> List.toArray

    let scales = 
        seq { for i in 1 .. height do yield float(i) * scale }
        |> Seq.map ( fun x -> sprintf "- %s" (numberFormat x) )
        |> Seq.rev |> Seq.toArray


    let tmp =
        indicator
        |> Seq.map ( fun (_, v) -> v)
        |> Seq.toList |> resize width
        |> Seq.map barFun
    [scales] |> Seq.append tmp |> Seq.transpose

open System.Text
let AsString (data: string seq seq) =
    let foldFun (acc:StringBuilder) (row:string seq) =
        let rowString = row |> String.concat ""
        acc.Append(rowString).Append("\n")
    let fold =
        data
        |> Seq.fold foldFun (new StringBuilder())
    fold.ToString()
   