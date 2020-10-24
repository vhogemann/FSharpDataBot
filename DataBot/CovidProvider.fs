module CovidProvider
open FSharp.Data
open ZedGraph

type Covid = JsonProvider<"covidsample.json", SampleIsList=true>

let totalVsNew (total: (double*double) seq) (newCases: (double*double) list) =
    let totalValues =
        total
        |> Seq.sortBy( fun (x,_) -> x )
        |> Seq.map( fun (_,v) -> v )
   
    let newValues =
        newCases
        |> Seq.sortBy( fun (x,_) -> x )
        |> Seq.map( fun (_,v) -> v )
    Seq.zip totalValues newValues

let folder (state: (double*double) list) (current:double*double): (double*double) list =
    let (_, prev) = state |> List.last    
    let (dt, curr) = current
    state @ [(dt, curr)]

type CovidIndicator (code:string, name:string, values:Covid.Root[]) =
    member __.Name = name
    member __.Code = code
    member __.Deaths = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Deaths))
    member this.NewDeaths = this.Deaths |> Seq.toList |> List.fold folder [(0.0, 0.0)]
    member this.TotalVsNewDeaths = totalVsNew (this.Deaths) (this.NewDeaths)
    member __.Confirmed = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Confirmed))
    member this.NewConfirmed = this.Confirmed |> Seq.toList |> List.fold folder [(0.0, 0.0)]
    member this.TotalVsNewConfirmed = totalVsNew (this.Confirmed) (this.NewConfirmed)
    member __.Recovered = values |> Seq.map (fun value -> (double (value.Date.DateTime |> XDate), double value.Recovered))
    member this.NewRecovered = this.Recovered |> Seq.toList |> List.fold folder [(0.0, 0.0)]
    member this.TotalVsNewRecovered = totalVsNew (this.Recovered) (this.NewRecovered)

let AsyncFetch code name=
    let baseUrl = sprintf "https://api.covid19api.com/total/dayone/country/%s" code
    async {
        let! json = Http.AsyncRequestString baseUrl
        return CovidIndicator( code, name, Covid.ParseList json)
    }
