﻿module Command

open Data
open System
open System.Text.RegularExpressions

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None

type IndicatorType =
    | Gdp
    | GdpPerCapita
    | GdpGrowth
    | Unenployment
    | YoungLiteracy
    | AdultLiteracy

type Token = 
    | Country of country : ICountry
    | Year of year : int
    | Indicator of name : IndicatorType
    | Unknown

type GraphPeriod =
    | Year of year:int
    | Period of int * int

type GraphCommand = {
    Country: ICountry list
    Indicator: IndicatorType list
    Year: int list
}

let private extract = function
    // Countries
    | "brazil" :: t | "brasil" :: t | "br" :: t | "bra" :: t -> 
        Country(Data.Brazil), t
    | "argentina" :: t | "arg" :: t | "ar" :: t -> 
        Country(Data.Argentina), t
    | "usa" :: t | "us" :: t | "eua" :: t | "estados" :: "unidos" :: t | "united" :: "states" :: t -> 
        Country(Data.USA), t
    | "china" :: t | "cn" :: t | "chn" :: t -> 
        Country(Data.China), t

    // Indicators
    | "gdp" :: "growth" :: t | "crescimento" :: "do" :: "pib" :: t | "crescimento" :: "da" :: "economia" :: t ->
        Indicator(GdpGrowth), t
    | "gdp" :: "per" :: "capita" :: t | "pib" :: "per" :: "capita" :: t -> 
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

let Parse (commandLine:string) : GraphCommand = 
    let args = commandLine.Split(" ") |> Array.toList
    let tokens = parse(args)
    
    let folder (state: GraphCommand) (token:Token) =
        match token with
        | Token.Country c ->  { state with Country = state.Country @ [c]  }
        | Token.Indicator i -> { state with Indicator = state.Indicator @ [i] }
        | Token.Year y -> { state with Year = state.Year @ [y] }
        | _ -> state

    tokens
    |> List.fold folder { Country = []; Indicator = []; Year = [] }
    
let Execute (command:GraphCommand) =
    let execute (country:ICountry) indicator yearMin yearMax =
        let data =
            match indicator with
            | IndicatorType.Gdp -> country.GetGdp()
            | IndicatorType.GdpGrowth -> country.GetGdpGrowth()
            | IndicatorType.GdpPerCapita -> country.GetGdpPerCapita()
            | IndicatorType.AdultLiteracy -> country.GetAdultLiteracy()
            | IndicatorType.YoungLiteracy -> country.GetYouthLiteracy()
            | IndicatorType.Unenployment -> country.GetUnenployment()
        
        let periodFilter =
            match yearMin, yearMax with
            | Some min, Some max when min < max ->
                Seq.filter (fun (y, _) -> y >= min && y <= max )
            | _ -> id

        data
        |> periodFilter
        |> Plot.Line 14 7
        |> Plot.AsString

    let maybeStartYear =
        match command.Year with
        | [] -> None
        | years -> years |> List.min |> Some
    let maybeEndYear =
        match command.Year with
        | [] -> None
        | years -> years |> List.max |> Some
    
    seq { 
        for country in command.Country do
            for indicator in command.Indicator do
                yield execute country indicator maybeStartYear maybeEndYear
    }