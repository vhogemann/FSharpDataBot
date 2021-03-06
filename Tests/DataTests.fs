module DataTests

open Data
open Xunit
open Swensen.Unquote

[<Fact>]
let ``Country Matcher`` () =

    test 
        <@ 
            match "brazil" with
            | CountryMatcher country ->
                country.TwoLetterCode = "br"
            | _ -> false
        @>
        
    test
        <@
            match "brasil" with
            | CountryMatcher country ->
                country.TwoLetterCode = "br"
            | _ -> false
        @>
    
    // Lots of people use UK instead of the correct GB code
    test
        <@
            match "uk" with
            | CountryMatcher country ->
                country.TwoLetterCode = "gb"
            | _ -> false
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
