module Twitter
open System
open CoreTweet

type FeedReader() =
    
    let twitter = Tokens.Create("consumerKey","consumerSecret","accessToken","accessSecret")
    
    let getMentions () = async {
        return! twitter.Statuses.MentionsTimelineAsync() |> Async.AwaitTask
    }

    let reply(tweetId:Nullable<int64>, reply: string list) = async {
        return! twitter.Statuses.UpdateAsync("", tweetId) |> Async.AwaitTask
    }

    member __.Start () = async {
        return ()
    }
