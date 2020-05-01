open System
open FSharp.Data


let shrink (width:int) (values: float list) =
    let length = values.Length

    let ratio = (float length) / (float width) |> Math.Ceiling |> Convert.ToInt32

    let fractional = (length % width) > 0


    let rec shrinkRec ratio fractional values (acc: float list list) =
        match values with
        | [] -> acc
        | _ ->
            let cols =
                if (values.Length < ratio ) then values.Length else ratio
            let columns = values |> List.take cols

            let skip = 
                if fractional && (cols - 1) > 0 then
                    cols - 1
                else
                    cols

            shrinkRec ratio fractional (values |> List.skip skip) (acc @ [columns])


    shrinkRec ratio fractional values []
    |> List.map(List.average)
   

let plotColumn height value =
    let rec bar index (acc:string list) value =
        match index, value with
        | i, _ when i >= height -> acc
        | i, v when i = v -> bar (index+1) (acc @ ["*"]) value
        | _, _ -> bar (index + 1) (acc @ ["-"]) value

    bar 0 [] value

let numberFormat = function
    | x when x < 10.0 ** 3.0 -> string x
    | x when x < 10.0 ** 6.0 -> sprintf "%.0fK" (x/10.0 ** 3.0)
    | x when x < 10.0 ** 9.0 -> sprintf "%.0fM" (x/10.0 ** 6.0)
    | x when x < 10.0 ** 12.0 -> sprintf "%.1fB" (x/10.0 ** 9.0)
    | x when x < 10.0 ** 15.0 -> sprintf "%.1fT" (x/10.0 ** 12.0)
    | x -> sprintf "%.2G" x

let buildGraph (height:int) (width:int) (indicator:Runtime.WorldBank.Indicator) =

    let (_, max) =
        indicator
        |> Seq.maxBy (fun (_, value) -> value)

    let scale = max / ( float height)
    
    let barFun value =
        let index = (value / scale |> Convert.ToInt32) - 1   
        plotColumn height index |> List.toArray

    let scales = 
        seq { for i in 0 .. (height-1) do yield float(i) * scale }
        |> Seq.map ( fun x -> sprintf "- %s" (numberFormat x) )
        |> Seq.toArray
    
    let tmp =
        indicator
        |> Seq.map ( fun (_, v) -> v)
        |> Seq.toList |> shrink width
        |> Seq.map barFun
        |> Seq.toArray

    let values = [|scales|] |> Array.append tmp
    let width = (tmp |> Seq.length)

    Array2D.init (width + 1) height (fun x y -> values.[x].[(height - 1) - y])
 


[<EntryPoint>]
let main argv =
    let data = WorldBankData.GetDataContext()
    let indicator =
        data
            .Countries
            .Brazil
            .Indicators
            .``GDP (constant 2010 US$)``


    let graph = indicator |> buildGraph 5 24
     
    let width = (Array2D.length1 graph) - 1
    let heigh = (Array2D.length2 graph) - 1

    printfn "Brazil - %s" indicator.Name

    for y in [0..heigh] do
        for x in [0..width] do
            printf "%s" graph.[x,y]
        printfn ""

    
    0 // return an integer exit code
