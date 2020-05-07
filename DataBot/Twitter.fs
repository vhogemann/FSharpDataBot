module Twitter
open System
open CoreTweet

type FeedReader() =
    
    let token = OAuth2.GetTokenAsync("consumerkey","consumersecret")
    
    member __.Start () = async {
        return ()
    }
