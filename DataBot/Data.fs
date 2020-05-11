module Data
open System
open FSharp.Data

let private data = WorldBankData.GetDataContext()

type IEconomy =
    abstract member GetGdp : unit -> Runtime.WorldBank.Indicator
    abstract member GetGdpPerCapita : unit -> Runtime.WorldBank.Indicator
    abstract member GetGdpGrowth : unit -> Runtime.WorldBank.Indicator
    abstract member GetUnenployment : unit -> Runtime.WorldBank.Indicator

type ISocial =
    abstract member GetYouthLiteracy : unit -> Runtime.WorldBank.Indicator
    abstract member GetAdultLiteracy : unit -> Runtime.WorldBank.Indicator

type ICountry =
    inherit IEconomy
    inherit ISocial
    inherit IComparable
    abstract member FlagEmoji : string with get
    abstract member Inner: WorldBankData.ServiceTypes.Country with get

let compareTo (country:WorldBankData.ServiceTypes.Country) (obj:Object) =
    if obj :? ICountry then
        let countryObj = obj :?> ICountry
        country.Code.CompareTo( countryObj.Inner.Code )
    else
        -1

let Brazil =
    let country = data.Countries.Brazil
    { new ICountry with 
        member __.GetGdp() = country.Indicators.``GDP (current US$)``
        member __.GetGdpPerCapita() = country.Indicators.``GDP per capita (current US$)``
        member __.GetGdpGrowth() = country.Indicators.``GDP growth (annual %)``
        member __.GetUnenployment() = country.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)`` 
        member __.GetYouthLiteracy() = country.Indicators.``Literacy rate, youth total (% of people ages 15-24)``
        member __.GetAdultLiteracy() = country.Indicators.``Literacy rate, adult total (% of people ages 15 and above)``
        member __.FlagEmoji with get() = country.Code;
        member __.Inner with get() = country
        member __.CompareTo(obj) = compareTo country obj }

let Argentina =
    let country = data.Countries.Argentina
    { new ICountry with 
        member __.GetGdp() = country.Indicators.``GDP (current US$)``
        member __.GetGdpPerCapita() = country.Indicators.``GDP per capita (current US$)``
        member __.GetGdpGrowth() = country.Indicators.``GDP growth (annual %)``
        member __.GetUnenployment() = country.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)`` 
        member __.GetYouthLiteracy() = country.Indicators.``Literacy rate, youth total (% of people ages 15-24)``
        member __.GetAdultLiteracy() = country.Indicators.``Literacy rate, adult total (% of people ages 15 and above)``
        member __.FlagEmoji with get() = country.Code;
        member __.Inner with get() = country
        member __.CompareTo(obj) = compareTo country obj }

let USA =
    let country = data.Countries.``United States``
    { new ICountry with 
        member __.GetGdp() = country.Indicators.``GDP (current US$)``
        member __.GetGdpPerCapita() = country.Indicators.``GDP per capita (current US$)``
        member __.GetGdpGrowth() = country.Indicators.``GDP growth (annual %)``
        member __.GetUnenployment() = country.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)``
        member __.GetYouthLiteracy() = country.Indicators.``Literacy rate, youth total (% of people ages 15-24)``
        member __.GetAdultLiteracy() = country.Indicators.``Literacy rate, adult total (% of people ages 15 and above)``
        member __.FlagEmoji with get() = country.Code;
        member __.Inner with get() = country
        member __.CompareTo(obj) = compareTo country obj }

let China =
    let country = data.Countries.China
    { new ICountry with 
        member __.GetGdp() = country.Indicators.``GDP (current US$)``
        member __.GetGdpPerCapita() = country.Indicators.``GDP per capita (current US$)``
        member __.GetGdpGrowth() = country.Indicators.``GDP growth (annual %)``
        member __.GetUnenployment() = country.Indicators.``Unemployment, total (% of total labor force) (modeled ILO estimate)``
        member __.GetYouthLiteracy() = country.Indicators.``Literacy rate, youth total (% of people ages 15-24)``
        member __.GetAdultLiteracy() = country.Indicators.``Literacy rate, adult total (% of people ages 15 and above)`` 
        member __.FlagEmoji with get() = country.Code;
        member __.Inner with get() = country
        member __.CompareTo(obj) = compareTo country obj }