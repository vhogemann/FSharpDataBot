namespace DataBot.Model

open Countries
open System
open FSharp.Data
open CovidProvider

type WorldBankCountry = WorldBankData.ServiceTypes.Country

type ICountry =
    abstract Name: string
    abstract ISO2: string
    abstract ISO3: string
    abstract Flag: string
    abstract Population: float
    abstract WBCountry: WorldBankCountry
    abstract Covid: CovidIndicator


type Country(countryId: CountryId, covid: CovidIndicator, worldBank: WorldBankCountry) =
    interface ICountry with
        member __.Name = worldBank.Name
        member __.ISO2 = countryId.TwoLetterCode
        member __.ISO3 = countryId.ThreeLetterCode
        member __.Flag = countryId.Emoji.ToString()

        member __.Population =
            let _, population = worldBank.Indicators.``Population, total`` |> Seq.last
            population

        member __.WBCountry = worldBank
        member __.Covid = covid

type IndicatorName = string

type CountryName = string

type Indicator = IndicatorName * CountryName * (double * double) seq

type IndicatorGetter = ICountry -> Indicator

type GraphType = | Line

type AxisType =
    | Linear
    | Log
    | Date

type GraphConfig =
    { AxisType: AxisType
      GraphType: GraphType }

type Command =
    { Countries: ICountry seq
      Indicator: IndicatorGetter
      Start: DateTime
      End: DateTime }

[<RequireQualifiedAccess>]
type Token =
    | Country of ICountry
    | Indicator of IndicatorGetter
    | Year of int
    | ShortCut of IndicatorGetter seq

module Matchers =
    let private wbContext = WorldBankData.GetDataContext()
    let private wbCountries = wbContext.Countries

    let (|CountryMatcher|_|) (token: string) =
        if String.IsNullOrEmpty(token) then
            None
        else
            CountryIdList
            |> List.tryFind (fun it -> it.Name = token || it.TwoLetterCode = token || it.ThreeLetterCode = token)
            |> Option.map (fun countryId ->
                let covid =
                    CovidProvider.AsyncFetch (countryId.ThreeLetterCode) (countryId.Name) |> Async.RunSynchronously
                let maybeWorldBank =
                    wbCountries |> Seq.tryFind (fun it -> it.Code.ToLowerInvariant() = countryId.ThreeLetterCode)
                match maybeWorldBank with
                | None -> None
                | Some worldBank ->
                    Some(Country(countryId, covid, worldBank)))

    let toDoubleDouble = Seq.map (fun (x, y) -> double x, double y)

    let (|ShortcutMatcher|_|) (key: string): IndicatorGetter seq option =
        let template indicatorName cb (country: ICountry) =
            indicatorName, country.Name, (country |> cb)
        match key with
        | "covid" ->
            seq {
                yield template "Covid-19 - Confirmed Cases" (fun c -> c.Covid.Confirmed)
                yield template "Covid-19 - Deaths" (fun c -> c.Covid.Deaths)
                yield template "Covid-19 - Recovered" (fun c -> c.Covid.Recovered)
            }
            |> Some
        | _ -> None

    let (|IndicatorMatcher|_|) (key: string): IndicatorGetter option =
        let template (cb: ICountry -> Runtime.WorldBank.Indicator) (country: ICountry) =
            let indicator = country |> cb
            indicator.Description, country.Name, (indicator |> toDoubleDouble)
        match key with
        | "gdp" ->
            template (fun i -> i.WBCountry.Indicators.``GDP (current US$)``) |> Some
        | "gdp/capita" ->
            template (fun i -> i.WBCountry.Indicators.``GDP per capita (current US$)``) |> Some
        | "literacy"
        | "literacy/young" ->
            template (fun i -> i.WBCountry.Indicators.``Literacy rate, youth total (% of people ages 15-24)``) |> Some
        | "literacy/adult" ->
            template (fun i -> i.WBCountry.Indicators.``Literacy rate, adult total (% of people ages 15 and above)``)
            |> Some
        | "employment" ->
            template
                (fun i -> i.WBCountry.Indicators.``Employment to population ratio, 15+, total (%) (national estimate)``)
            |> Some
        | "unemployment" ->
            template
                (fun i -> i.WBCountry.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)``)
            |> Some
        | "electricity-access" ->
            template (fun i -> i.WBCountry.Indicators.``Access to electricity (% of population)``) |> Some
        | "exchange-rate/usd" ->
            template (fun i -> i.WBCountry.Indicators.``Official exchange rate (LCU per US$, period average)``) |> Some
        | "poverty-gap" ->
            template
                (fun i -> i.WBCountry.Indicators.``Poverty headcount ratio at national poverty lines (% of population)``)
            |> Some
        | "stock-market" ->
            template (fun i -> i.WBCountry.Indicators.``Stocks traded, total value (current US$)``) |> Some
        | "external-balance" ->
            template (fun i -> i.WBCountry.Indicators.``External balance on goods and services (current US$)``) |> Some
        | _ -> None
