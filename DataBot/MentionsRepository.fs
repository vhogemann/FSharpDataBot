module MentionsRepository
open System
open Microsoft.EntityFrameworkCore

type [<CLIMutable>] MentionEntry = {
    Id: int64
    TimeStamp: DateTime
}

type MentionContext () =
    inherit DbContext()
    [<DefaultValue>]
    val mutable mentions: DbSet<MentionEntry>
    member public this.Mentions with  get() = this.mentions and set m = this.mentions <- m
    override __.OnConfiguring optionsBuilder =
        optionsBuilder.UseSqlite("Data Source=mentions.db") |> ignore

let db = new MentionContext()

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