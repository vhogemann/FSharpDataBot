module Twitter
open System
open Tweetinvi
open Tweetinvi.Models
open Tweetinvi.Parameters

let getEnv = Environment.GetEnvironmentVariable

let twitterApiKey = 
    "TWITTER_API_KEY" |> getEnv
let twitterApiSecretKey = 
    "TWITTER_API_SECRET_KEY" |> getEnv
let twitterAccessToken = 
    "TWITTER_ACCESS_TOKEN" |> getEnv
let twitterAccessTokenSecret = 
    "TWITTER_ACCESS_TOKEN_SECRET" |> getEnv
let lockFilePath = 
    "FS_DATA_BOT_LAST_REPLY" |> getEnv

type ReplyCache() =
    let mutable lastReply = 0L
    member __.GetLastReply():int64 = lastReply
    member __.SetLastReply(messageId:int64) = lastReply <- messageId

type FeedReader() =
    do 
        Auth.SetUserCredentials(
            twitterApiKey,
            twitterApiSecretKey,
            twitterAccessToken,
            twitterAccessTokenSecret) |> ignore
        TweetinviConfig.CurrentThreadSettings.TweetMode <- TweetMode.Extended
    let botUser = User.GetAuthenticatedUser().ScreenName
    let cache = ReplyCache()

    let rec postReply (userHandle:string) (tweetId:int64) (replies:Drawing.Bitmap list) = async {
        match replies with
        | [] -> return ()
        | status :: tail ->
            let! response = TweetAsync.PublishTweetInReplyTo(sprintf "@%s" userHandle , tweetId) |>Async.AwaitTask
            return! postReply botUser (response.Id) tail
    }

    let reply (mentions: IMention seq) = 
            mentions
            |> Seq.filter (fun mention -> not (isNull mention.Text))
            |> Seq.map (fun mention ->
                let command = mention.Text |> Command.Parse
                let replies = 
                    seq { 
                        for indicator in command.Indicator do
                            let startYear =
                                match command.Year with
                                | [] -> None
                                | years -> years |> List.min |> Some
                            let endYear =
                                match command.Year with
                                | [] -> None
                                | years -> years |> List.max |> Some
                            yield Graph.Line startYear endYear indicator (command.Country)
                    } |> Seq.toList
                postReply mention.CreatedBy.ScreenName mention.Id replies
            )
            |> Async.Parallel
            |> Async.Ignore

    member __.Start () = async {
            printfn "fetching tweets"
            let parameters = MentionsTimelineParameters()
            parameters.SinceId <- 10L
            let mentions = Timeline.GetMentionsTimeline()
            let last = mentions |> Seq.last
            if not(isNull mentions) then do! reply mentions
    }