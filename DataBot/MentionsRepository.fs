module MentionsRepository
open System
open Microsoft.EntityFrameworkCore

type [<CLIMutable>] MentionEntry = {
    Id: int64
    TimeStamp: DateTime
}

type SqliteMentionContext (useSqlite, accountEndpoint, accountKey, databaseName) =
    inherit DbContext()
    [<DefaultValue>]
    val mutable mentions: DbSet<MentionEntry>
    member public this.Mentions with  get() = this.mentions and set m = this.mentions <- m
    override _.OnConfiguring optionsBuilder =
        if useSqlite then
            optionsBuilder.UseSqlite("Data Source=mentions.db") |> ignore
        else
            optionsBuilder.UseCosmos(accountEndpoint, accountKey, databaseName) |> ignore


let db =
    let getEnv = Environment.GetEnvironmentVariable
    let useSQLite =
        match "USE_SQL_LITE" |> getEnv with
        | null -> false
        | s -> s |> bool.Parse
    let accountEndpoint = "COSMOS_DB_ACCOUNT_ENDPOINT" |> getEnv
    let accountKey = "COSMOS_DB_ACCOUNT_KEY" |> getEnv
    let databaseName = "COSMOS_DB_DATABASE_NAME" |> getEnv
    
    new SqliteMentionContext(useSQLite, accountEndpoint, accountKey, databaseName)
    
db.Database.EnsureCreated() |> ignore

let SaveMentions mentions =
    for mention in mentions do
        db.Mentions.Add(mention) |> ignore
    db.SaveChangesAsync() |> Async.AwaitTask

let GetLatestMention () =
    query {
        for mention in db.Mentions do
            sortByDescending mention.TimeStamp
            select mention
            take 1
    }