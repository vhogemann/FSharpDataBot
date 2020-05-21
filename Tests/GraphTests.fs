module GraphTests

open Xunit
open Graph
open Swensen.Unquote

[<Fact>]
let ``String to Color Hex`` () =
    let color = stringToColorHex "test"
    test 
        <@ 
            color <> null &&
            color.StartsWith "#" &&
            color.Length = 7
        @>

[<Fact(Skip = "TODO")>]
let ``String to Color`` () =
    let str = "test"
    let hex = str |> stringToColorHex 
    let color = str |> StringToColor
    let name = color.Name.ToUpper()
    test
        <@
            hex.IndexOf (name) > 0
        @>