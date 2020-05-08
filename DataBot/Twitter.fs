module Twitter
open System
open CoreTweet
type FeedReader() =
    let rnd = Random()
    let twitter = Tokens.Create("consumerKey","consumerSecret","accessToken","accessSecret")

    let rec postReply (tweetId:int64) (replies:string list) = async {
        match replies with
        | [] -> return ()
        | status :: tail ->
            let! response = twitter.Statuses.UpdateAsync(status, Nullable(tweetId)) |> Async.AwaitTask
            return! postReply (response.Id) tail
    }

    let start (mentions: Status seq) = async {
        let tasks =
            mentions
            |> Seq.map (fun mention ->
                let replies = 
                    mention.FullText
                    |> Command.Parse
                    |> Plot.Line 14 7
                    |> Seq.toList
                postReply mention.Id replies
            )
            |> Async.Parallel
        return! tasks
    }

    member __.Start () = async {
        let! mentions = twitter.Statuses.MentionsTimelineAsync(10) |> Async.AwaitTask
        return! start mentions
    }
