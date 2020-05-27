module Command

open Data
open System.Text.RegularExpressions

type IndicatorFun = 
| WB of (WBCountry -> WBIndicator)
| Covid of (Countries.Country->Async<CovidProvider.CovidIndicator>)

type Token =
| Country of Countries.Country
| Indicator of IndicatorFun
| Year of int
| Unknown

type FoldState = {
    Country : Countries.Country list
    Indicator : IndicatorFun list
    Year : int list
}

type Command = {
    Countries : Countries.Country list
    Indicator : IndicatorFun 
    StartYear : int option
    EndYear : int option
}

let parse (token:string) = 
    match token with
    | IndicatorMatcher indicator ->
        Token.Indicator (WB indicator) 
    | CountryMatcher country -> 
        Token.Country country
    | YearMatcher year -> 
        Token.Year year
    | CovidMatcher covid ->
        Token.Indicator (Covid covid)
    | _ -> 
        Token.Unknown

let Parse (commandLine:string)  = 
    let tokens = 
        Regex.Split(commandLine, "\s") 
        |> Set.ofArray 
        |> Set.toList
        |> List.map parse
    
    let folder (state: FoldState) (token:Token) =
        match token with
        | Token.Country c -> { state with Country = state.Country @ [c]  }
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
