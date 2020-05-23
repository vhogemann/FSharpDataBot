module CovidProvider
open FSharp.Data
open ZedGraph

type Covid = JsonProvider<"covidsample.json", SampleIsList=true>


type CovidIndicator (code:string, name:string, values:Covid.Root[]) =
    member __.Name = name
    member __.Code = code
    member __.Deaths = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Deaths))
    member __.Confirmed = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Confirmed))
    member __.Recovered = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Recovered))

let AsyncFetch code name=
    let baseUrl = sprintf "https://api.covid19api.com/total/dayone/country/%s" code
    async {
        let! json = Http.AsyncRequestString baseUrl
        return CovidIndicator( code, name, Covid.ParseList json)
    }
