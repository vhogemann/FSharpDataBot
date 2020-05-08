module Twitter
open System
open Tweetinvi
open Tweetinvi.Models
open Tweetinvi.Parameters

type FeedReader() = 
    do 
        Auth.SetUserCredentials(
            "",
            "",
            "",
            "") |> ignore
        TweetinviConfig.CurrentThreadSettings.TweetMode <- TweetMode.Extended
    let botUser = User.GetAuthenticatedUser().ScreenName

    let rec postReply (userHandle:string) (tweetId:int64) (replies:string list) = async {
        match replies with
        | [] -> return ()
        | status :: tail ->
            let! response = TweetAsync.PublishTweetInReplyTo(sprintf "@%s\n%s" userHandle status , tweetId) |>Async.AwaitTask
            return! postReply botUser (response.Id) tail
    }

    let reply (mentions: IMention seq) = 
            mentions
            |> Seq.filter (fun mention -> not (isNull mention.Text))
            |> Seq.map (fun mention ->
                let replies = 
                    mention.Text
                    |> Command.Parse
                    |> Plot.Line 10 5
                    |> Seq.toList
                postReply mention.CreatedBy.ScreenName mention.Id replies
            )
            |> Async.Parallel
            |> Async.Ignore

    member __.Start () = async {
            printfn "fetching tweets"
            let parameters = MentionsTimelineParameters()
            parameters.SinceId <- 10L
            let mentions = Timeline.GetMentionsTimeline()
            if not(isNull mentions) then do! reply mentions            //do! Async.Sleep 10000
    }