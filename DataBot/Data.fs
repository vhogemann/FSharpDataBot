﻿module Data
open System
open FSharp.Data
open System.Text.RegularExpressions

let private data = WorldBankData.GetDataContext()
let private _c = data.Countries

type WBCountry = WorldBankData.ServiceTypes.Country
type WBIndicator = Runtime.WorldBank.Indicator

let (|CoutryMatcher|_|) (key:string) =
    Countries.List 
    |> List.tryFind (fun region -> 
        region.Name = key
        || region.ThreeLetterCode = key
        || region.TwoLetterCode = key
    )

let toWBCountry (region:Countries.Country) =
    _c |> Seq.tryFind (fun country -> country.Code = region.ThreeLetterCode.ToUpper()) 
    

let (|CovidMatcher|_|) (key: string) =
    match key with
    | "covid" -> Some (fun (country:Countries.Country) -> 
        CovidProvider.AsyncFetch (country.TwoLetterCode) (country.ThreeLetterCode.ToUpper()))
    |_ -> None

let (|IndicatorMatcher|_|) (key: string) =
    match key with
    | "gdp" -> 
        Some (fun (c:WBCountry) -> c.Indicators.``GDP (current US$)``)
    | "gdp/capita" -> 
        Some (fun (c:WBCountry) -> c.Indicators.``GDP per capita (current US$)``)
    | "literacy" | "literacy/young" ->
        Some (fun (c:WBCountry) -> c.Indicators.``Literacy rate, youth total (% of people ages 15-24)``)
    | "literacy/adult" -> 
        Some (fun (c:WBCountry) -> c.Indicators.``Literacy rate, adult total (% of people ages 15 and above)``)
    | "employment" -> 
        Some (fun (c:WBCountry) -> c.Indicators.``Employers, total (% of total employment) (modeled ILO estimate)``)
    | "unenployment" -> 
        Some (fun (c:WBCountry) -> c.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)``)
    | "electricity-access" ->
        Some (fun (c:WBCountry) -> c.Indicators.``Access to electricity (% of population)``)
    |_ -> None

let (|YearMatcher|_|) (token:string) =
    let m = Regex.Match( token,  @"(19[0-9]{2}|20[0-9]{2})" )
    if m.Success then
        let year = m.Groups.[1].Value
        Convert.ToInt32 year |> Some
    else
        None