module DataTests

open Data
open Xunit
open Swensen.Unquote

[<Fact>]
let ``Country Matcher`` () =

    test 
        <@ 
            match "bra" with
            | CoutryMatcher country ->
                country.Name = "Brazil"
            | _ -> false
        @>
    
    let maybeUsa =
        match "united states" with
            | CoutryMatcher country ->
                Some country;
            | _ -> None
    
    test 
        <@ 
            maybeUsa.IsSome            
        @>

[<Fact>]
let ``Year Matcher`` () =
    test
        <@
            match "2020" with
            | YearMatcher year ->
                year = 2020
            | _ -> false
        @>

    test
        <@
            match "1920" with
            | YearMatcher year ->
                year = 1920
            | _ -> false
        @>

[<Fact>]
let ``Indicator Matcher`` () =
    test
        <@
            match "gdp" with
            | IndicatorMatcher indicator ->
                true
            | _ -> false
        @>
