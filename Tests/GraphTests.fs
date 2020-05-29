module GraphTests

open Xunit
open Graph
open System.IO
open Swensen.Unquote
open CovidProvider

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
[<Fact>]
let ``Covid - NewDeaths`` () =
    let json = File.ReadAllText "covidsample.json"
    let parsed = Covid.ParseList(json)
    let country = CovidIndicator("br", "Brazil", parsed)
    using
        (Line "test" (seq { yield country.Name, country.Deaths }) None None false false false)
        (fun line -> using (new FileStream("test.png", FileMode.Create, FileAccess.Write)) line.WriteTo)
    