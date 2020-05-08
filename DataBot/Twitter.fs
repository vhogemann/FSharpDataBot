module Twitter
open System
open CoreTweet
type FeedReader() =
    let rnd = Random()
    let twitter = 
        Tokens.Create(
            "brUhckoCTFQZhA9TfwBSKScXR",
            "HfbIFtoPMZHGtlrHxUiLQJ72wljPXXAUgP4JotRNYt7kGM5blZ",
            "1258776964963332096-Pjwy8v76pAmVe65vqjzwR9pyIr9lnA",
            "9aopKNDx0Rak9LHZhENjXbovCw3EuCf9QScvJHVIGkFvv")

    let rec postReply (tweetId:int64) (replies:string list) = async {
        match replies with
        | [] -> return ()
        | status :: tail ->
            let! response = twitter.Statuses.UpdateAsync(status, Nullable(tweetId)) |> Async.AwaitTask
            return! postReply (response.Id) tail
    }

    let reply (mentions: Status seq) = 
            mentions
            |> Seq.filter (fun mention -> not (isNull mention.Text))
            |> Seq.map (fun mention ->
                let replies = 
                    mention.Text
                    |> Command.Parse
                    |> Plot.Line 10 5
                    |> Seq.toList
                postReply mention.Id replies
            )
            |> Async.Parallel
            |> Async.Ignore

    member __.Start () = async {
            printfn "fetching tweets"
            let! mentions = twitter.Statuses.MentionsTimelineAsync(10) |> Async.AwaitTask
            if not(isNull mentions) then do! reply mentions
            //do! Async.Sleep 10000
    }