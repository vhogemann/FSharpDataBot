module Data
open System
open FSharp.Data
open System.Globalization
open System.Text.RegularExpressions



let private data = WorldBankData.GetDataContext()
let private _c = data.Countries

type WBCountry = WorldBankData.ServiceTypes.Country
type WBIndicator = Runtime.WorldBank.Indicator

let regions = 
    CultureInfo.GetCultures(CultureTypes.SpecificCultures)
    |> Array.map (fun culture -> RegionInfo(culture.LCID))

let (|CoutryMatcher|_|) (key:string) =
    let maybeRegion = 
        regions 
        |> Array.tryFind (fun region -> 
            region.ThreeLetterISORegionName.ToLowerInvariant().Equals(key) 
            || region.ThreeLetterWindowsRegionName.ToLowerInvariant().Equals(key)
            || region.TwoLetterISORegionName.ToLowerInvariant().Equals(key)
            || region.DisplayName.ToLowerInvariant().Equals(key)
        )
    
    match maybeRegion with
    | Some region ->
        _c |> Seq.tryFind (fun country -> country.Name.Equals(region.DisplayName)) 
    | _ -> None
    

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
    |_ -> None

let (|YearMatcher|_|) (token:string) =
    let m = Regex.Match( token,  @"(19[0-9]{2}|20[0-9]{2})" )
    if m.Success then
        let year = m.Groups.[1].Value
        Convert.ToInt32 year |> Some
    else
        None