module Command

open Data
open System
open System.Text.RegularExpressions

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

[<StructuralComparison>]
[<StructuralEquality>]
type IndicatorType =
    | Gdp
    | GdpPerCapita
    | GdpGrowth
    | Unenployment
    | YoungLiteracy
    | AdultLiteracy

[<StructuralComparison>]
[<StructuralEquality>]
type Token = 
    | Country of country : ICountry
    | Year of year : int
    | Indicator of name : IndicatorType
    | Unknown

type GraphPeriod =
    | Year of year:int
    | Period of int * int

type FoldState = {
    Country: ICountry list
    Indicator: IndicatorType list
    Year: int list
}

type Command = {
    Countries: ICountry list
    Indicator: IndicatorType
    StartYear: int option
    EndYear: int option
}

let private extract = function
    // Countries
    | "brazil" :: t | "brasil" :: t | "br" :: t | "bra" :: t -> 
        Country(Data.Brazil), t
    | "argentina" :: t | "arg" :: t | "ar" :: t -> 
        Country(Data.Argentina), t
    | "usa" :: t | "us" :: t | "eua" :: t -> 
        Country(Data.USA), t
    | "china" :: t | "cn" :: t | "chn" :: t -> 
        Country(Data.China), t

    // Indicators
    | "gdp" :: "growth" :: t ->
        Indicator(GdpGrowth), t
    | "gdp/capita" :: t | "pib/capita" :: t -> 
        Indicator(GdpPerCapita), t 
    | "gdp" :: t | "pib" :: t -> 
        Indicator(Gdp), t

    | Regex @"(19[0-9]{2})" [ year ] :: t
    | Regex @"(20[0-9]{2})" [ year ] :: t ->
        let yearNumber = Convert.ToInt32 year
        Token.Year(yearNumber), t
    | _ :: t -> Unknown, t
    | [] -> Unknown, []

let private parse (tokens: string list) =
    let rec parseRec (tokens: string list) (acc: Token list) =
        let extracted = extract tokens 
        match extracted with
        | (token, []) -> acc @ [token]
        | (token, tail) -> parseRec tail acc @ [token]
    parseRec tokens []

let Parse (commandLine:string) : Command list = 
    let args = commandLine.Split(" ") |> Set.ofArray |> Set.toList
    let tokens = parse(args) |> Set.ofList |> Set.toList
    
    let folder (state: FoldState) (token:Token) =
        match token with
        | Token.Country c ->  { state with Country = state.Country @ [c]  }
        | Token.Indicator i -> { state with Indicator = state.Indicator @ [i] }
        | Token.Year y -> { state with Year = state.Year @ [y] }
        | _ -> state

    let state = 
        tokens
        |> List.fold folder { Country = []; Indicator = []; Year = [] }
    
    let startYear =
        match state.Year with
        | [] -> None
        | years -> years |> List.min |> Some
    let endYear =
        match state.Year with
        | [] -> None
        | years -> years |> List.max |> Some

    seq {
        for indicator in state.Indicator do
            yield { Countries = state.Country; Indicator = indicator; StartYear = startYear; EndYear = endYear }
    } |> Seq.toList
