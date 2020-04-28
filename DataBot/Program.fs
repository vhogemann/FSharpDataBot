// Learn more about F# at http://fsharp.org

open System
open FSharp.Data
// Brasil - GDP
// ________________________ -
// __**____________________
// _*__*___________________
// *____*__________________
// ________________________
// __**____________________
// _*__*___________________
// *____*__________________
// 2010 - 2020


let shrink width values =
    let ratio = float ( Array.length values ) / float width
    
    let merge ratio values =
        let rec groupRec ratio values acc =
            match ratio with
            | r when r < 1.0 -> acc @ [( Seq.tryHead values, r )]
            | r -> groupRec (r - 1.0) (Seq.tail values) (acc @ [(Seq.tryHead values, r)])
    
        let combine = function
            | (None, _) -> 0.0
            | (Some value, ratio) -> value * ratio

        let sum = groupRec ratio values [] |> Seq.map combine |> Seq.sum
        
        sum / ratio
    
    let shrinkRec width values acc =
        ignore


    merge ratio values   

let graphBar height value =
    let rec bar index (acc:string list) value =
        match index, value with
        | i, _ when i >= height -> acc
        | i, v when i = v -> bar (index+1) (acc @ ["*"]) value
        | _, _ -> bar (index + 1) (acc @ [" "]) value

    bar 0 [] value


let numberFormat = function
    | x when x < 10.0 ** 3.0 -> string x
    | x when x < 10.0 ** 6.0 -> sprintf "%.0fK" (x/10.0 ** 3.0)
    | x when x < 10.0 ** 9.0 -> sprintf "%.0fM" (x/10.0 ** 6.0)
    | x when x < 10.0 ** 12.0 -> sprintf "%.1fB" (x/10.0 ** 9.0)
    | x when x < 10.0 ** 15.0 -> sprintf "%.1fT" (x/10.0 ** 12.0)
    | x -> sprintf "%.2G" x

let buildGraph (height:int) (indicator:Runtime.WorldBank.Indicator) =
    let width = (indicator |> Seq.length)
    let (_, max) =
        indicator
        |> Seq.maxBy (fun (_, value) -> value)

    let scale = max / ( float height)
    
    let barFun value =
        let index = (value / scale |> Convert.ToInt32) - 1   
        graphBar height index |> List.toArray

    let scales = 
        seq { for i in 0 .. (height-1) do yield float(i) * scale }
        |> Seq.map ( fun x -> sprintf "- %s" (numberFormat x) )
        |> Seq.toArray
    
    let tmp =
        indicator
        |> Seq.map ( fun (_, v) -> barFun v)
        |> Seq.toArray

    let values = [|scales|] |> Array.append tmp

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
           
    
    let graph = indicator |> buildGraph 8
    
    let width = (Array2D.length1 graph) - 1
    let heigh = (Array2D.length2 graph) - 1

    printfn "Brazil - %s" indicator.Name

    for y in [0..heigh] do
        for x in [0..width] do
            printf "%s" graph.[x,y]
        printfn ""

    
    0 // return an integer exit code
