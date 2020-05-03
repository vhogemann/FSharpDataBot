module Data
open System
open FSharp.Data

let private data = WorldBankData.GetDataContext()

type ICountry =
    abstract member GetGdp : unit -> Runtime.WorldBank.Indicator
    abstract member GetGdpPerCapita : unit -> Runtime.WorldBank.Indicator
    abstract member GetUnenployment : unit -> Runtime.WorldBank.Indicator

let Brazil =
    let country = data.Countries.Brazil
    { new ICountry with 
    member __.GetGdp() = country.Indicators.``GDP (current US$)``
    member __.GetGdpPerCapita() = country.Indicators.``GDP per capita (current US$)``
    member __.GetUnenployment() = country.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)`` }

let Argentina =
    let country = data.Countries.Argentina
    { new ICountry with 
    member __.GetGdp() = country.Indicators.``GDP (current US$)``
    member __.GetGdpPerCapita() = country.Indicators.``GDP per capita (current US$)``
    member __.GetUnenployment() = country.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)`` }

let USA =
    let country = data.Countries.``United States``
    { new ICountry with 
    member __.GetGdp() = country.Indicators.``GDP (current US$)``
    member __.GetGdpPerCapita() = country.Indicators.``GDP per capita (current US$)``
    member __.GetUnenployment() = country.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)`` }

let China =
    let country = data.Countries.China
    { new ICountry with 
    member __.GetGdp() = country.Indicators.``GDP (current US$)``
    member __.GetGdpPerCapita() = country.Indicators.``GDP per capita (current US$)``
    member __.GetUnenployment() = country.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)`` }