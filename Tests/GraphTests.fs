module GraphTests

open Xunit
open Graph
open System.IO
open CovidProvider

[<Fact(Skip = "Breaks CI / CID")>]
let ``Covid - NewDeaths`` () =
    let json = File.ReadAllText "covidsample.json"
    let parsed = Covid.ParseList(json)
    let country = CovidIndicator("br", "Brazil", parsed)
    using
        (Line "test" (seq { yield country.Name, country.Deaths }) None None true false false)
        (fun line -> using (new FileStream("test.png", FileMode.Create, FileAccess.Write)) line.WriteTo)
    