module Twitter
open System
open System.IO
open MentionsRepository
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

type FeedReader() =
    do 
        Auth.SetUserCredentials(
            twitterApiKey,
            twitterApiSecretKey,
            twitterAccessToken,
            twitterAccessTokenSecret) |> ignore
        TweetinviConfig.CurrentThreadSettings.TweetMode <- TweetMode.Extended
    let botUser = User.GetAuthenticatedUser().ScreenName

    let rec postReply (userHandle:string) (tweetId:int64) (replies:MemoryStream list) = async {
        match replies with
        | [] -> return ()
        | stream :: tail ->
            let media = ResizeArray [ Upload.UploadBinary(stream.ToArray()) ]
            let options = PublishTweetOptionalParameters()
            options.Medias <- media
            options.InReplyToTweetId <- Nullable(tweetId)
            let! response = TweetAsync.PublishTweet(sprintf "@%s" userHandle , options) |>Async.AwaitTask
            return! postReply botUser (response.Id) tail
    }

    let reply (mentions: IMention seq) = 
            mentions
            |> Seq.filter (fun mention -> not (isNull mention.Text))
            |> Seq.map (fun mention ->
                let commands = mention.Text |> Command.Parse
                let replies = 
                    seq { 
                        for command in commands do
                            yield Graph.Line command
                    } |> Seq.toList
                postReply mention.CreatedBy.ScreenName mention.Id replies
            )
            |> Async.Parallel
            |> Async.Ignore

    member __.Start () = async {
            printfn "fetching tweets"
            let mayBeLastMention = GetLatestMention() |> Seq.tryHead
            let mentions = 
                match mayBeLastMention with
                | Some mention -> 
                    let parameters = MentionsTimelineParameters()
                    parameters.SinceId <- mention.Id
                    parameters.MaximumNumberOfTweetsToRetrieve <- 100
                    Timeline.GetMentionsTimeline(parameters)
                | None -> Timeline.GetMentionsTimeline()
            if not(isNull mentions) then do!
                mentions
                |> Seq.map (fun mention -> { Id = mention.Id; TimeStamp = mention.CreatedAt })
                |> SaveMentions
                |> ignore
                reply mentions
    }