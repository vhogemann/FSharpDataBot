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

type Reply =
| Tweet of PublishTweetOptionalParameters
| Error of string

type FeedReader() =
    do 
        Auth.SetUserCredentials(
            twitterApiKey,
            twitterApiSecretKey,
            twitterAccessToken,
            twitterAccessTokenSecret) |> ignore
        TweetinviConfig.CurrentThreadSettings.TweetMode <- TweetMode.Extended
    let botUser = User.GetAuthenticatedUser().ScreenName

    let execute (commands:Command.Command list) = async {
        let! images =
            commands
            |> Seq.map ( fun command -> async {
                let line = Graph.Line command
                let upload = Upload.UploadBinary( line.ToArray() )
                return upload
            })
            |> Async.Parallel
        return PublishTweetOptionalParameters(Medias = ResizeArray(images))
    }
        
    let reply (mentions: IMention seq) = async { 
        let! replies =
            mentions
            |> Seq.filter (fun mention -> not (isNull mention.Text))
            |> Seq.map ( fun mention ->
                let commands =
                    mention.Text.ToLowerInvariant()
                    |> Command.Parse
                    |> List.chunkBySize 4
                commands, mention.Id, mention.CreatedBy.ScreenName
            )
            |> Seq.map (fun (commands, mentionId, userHandle) ->
                    commands
                    |> List.map ( fun command -> async {
                        let! reply = command |> execute
                        reply.InReplyToTweetId <- Nullable(mentionId)
                        return TweetAsync.PublishTweet(sprintf "@%s" userHandle, reply)
                    })
                    |> Async.Parallel
            )
            |> Async.Parallel
        return replies |> Seq.collect id
    }
                
    let start () = async {
        printfn "fetching tweets"
        let mayBeLastMention = GetLatestMention() |> Seq.tryHead
        let maybeMentions = 
            match mayBeLastMention with
            | Some mention -> 
                let parameters = MentionsTimelineParameters()
                parameters.SinceId <- mention.Id
                parameters.MaximumNumberOfTweetsToRetrieve <- 100
                Timeline.GetMentionsTimeline(parameters) |> Option.ofObj
            | None -> Timeline.GetMentionsTimeline() |> Option.ofObj
        
        match maybeMentions with
        | Some mentions ->
            do! mentions
                |> Seq.map (fun mention -> { Id = mention.Id; TimeStamp = mention.CreatedAt })
                |> SaveMentions
                |> Async.Ignore
            return! reply mentions |> Async.Ignore
        | None ->
            return ()
    }

    member __.Start () = async {
            while true do
                do! start()
                do! Async.Sleep (1000 * 60)
    }