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

    let commandToReply indicator (countries:Countries.Country seq) startValue endValue = async {
        match indicator with
        | Command.IndicatorFun.WB ifun ->
            let countries =
                countries 
                |> Seq.map Data.toWBCountry 
                |> Seq.filter Option.isSome
                |> Seq.map Option.get
                |> Seq.toList
            
            let title = 
                let country =
                    countries
                    |> Seq.head
                    |> ifun
                country.Name
                
            let indicators =
                countries
                |> Seq.map ( fun country ->
                    let i = ifun country |> Seq.map (fun (x,y) ->  (double x, double y) ) 
                    country.Name, i
                )
            return seq { yield Graph.Line title indicators startValue endValue false false false }
                
        | Command.IndicatorFun.Covid ifun ->
            let! indicators =
                countries
                    |> Seq.map (fun country ->
                        country |> ifun
                    )
                    |> Async.Parallel
            let deaths = indicators |> Seq.map (fun country -> country.Name, country.Deaths)
            let deathsNxT = indicators |> Seq.map (fun country -> country.Name, country.TotalVsNewDeaths)
            let confirmed = indicators |> Seq.map (fun country -> country.Name, country.Confirmed)
            let confirmedNxT = indicators |> Seq.map (fun country -> country.Name, country.TotalVsNewConfirmed)
            let recovered = indicators |> Seq.map (fun country -> country.Name, country.Recovered)
            return seq {
                yield Graph.Line "Covid-19 - Deaths" deaths None None true false true 
                yield Graph.Line "Covid-10 - New vs Total Deaths" deathsNxT None None false true true
                yield Graph.Line "Covid-19 - Confirmed" confirmed None None true false true
                yield Graph.Line "Covid-19 - New vs Total Confirmed" confirmedNxT None None false true true
                yield Graph.Line "Covid-19 - Recovered" recovered None None true false true
            }
    }

    let execute (commands:Command.Command list) = async {
        let! replies =
            commands
            |> Seq.map ( fun command -> async {
                let! lines = commandToReply (command.Indicator) (command.Countries) (command.StartYear) (command.EndYear)
                return lines
                   |> Seq.map (fun line -> Upload.UploadBinary( line.ToArray()))
                   |> Seq.chunkBySize 4
                   |> Seq.map (ResizeArray)
                   |> Seq.map (fun medias -> PublishTweetOptionalParameters(Medias = medias))
            })
            |> Async.Parallel
        return replies |> Seq.concat
    }
        
    let reply (mentions: IMention seq) = async { 
        let! replies =
            mentions
            |> Seq.filter (fun mention -> not (isNull mention.Text))
            |> Seq.map ( fun mention ->

                printfn "replying to %s | %s" mention.CreatedBy.ScreenName mention.Text
                
                let commands =
                    mention.Text.ToLowerInvariant()
                    |> Command.Parse
                    |> List.chunkBySize 4
                commands, mention.Id, mention.CreatedBy.ScreenName
            )
            |> Seq.map (fun (commands, mentionId, userHandle) -> async {
                    let! replies = commands |> List.map execute |> Async.Sequential
                    return 
                        replies
                        |> Seq.concat
                        |> Seq.map (fun reply -> reply, mentionId, userHandle)
            })
            |> Async.Parallel

        return! 
            replies 
            |> Seq.concat
            |> Seq.map(fun (reply, mentionId, userHandle) ->
                reply.InReplyToTweetId <- Nullable(mentionId)
                TweetAsync.PublishTweet(sprintf "@%s" userHandle, reply) |> Async.AwaitTask
            ) 
            |> Async.Parallel
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