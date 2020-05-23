module CovidProvider
open FSharp.Data
open ZedGraph

type Covid = JsonProvider<"covidsample.json", SampleIsList=true>


type CovidIndicator (code:string, values:Covid.Root[]) =
    member __.Deaths = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Deaths))
    member __.Confirmed = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Confirmed))
    member __.Recovered = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Recovered))

let AsyncFetch country status =
    let baseUrl = sprintf "https://api.covid19api.com/live/country/%s/status/%s" country status
    async {
        let! json = Http.AsyncRequestString baseUrl
        return CovidIndicator( country,  Covid.ParseList json)
    }
