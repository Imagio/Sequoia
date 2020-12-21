namespace Sequoia.Data

open Sequoia.Common

[<AutoOpen>]
module Storage =
    
    type ConnectionSettings = {
        Host: string
        Port: int
        Database: string
    }
    
    type Database = MongoDB.Driver.IMongoDatabase
    
    [<Interface>]
    type IStorage =
        abstract GetDatabase : unit -> Database
    
    [<RequireQualifiedAccess>]
    module internal Collections =
        let [<Literal>] internal clientsCollection = "Clients"

    [<RequireQualifiedAccess>]
    module Mongo =
        let connect (c: ConnectionSettings) : Database =
            let connectionString = sprintf "mongodb://%s:%i" c.Host c.Port
            let client = MongoDB.Driver.MongoClient connectionString
            client.GetDatabase c.Database
            
        let get s (db: Database) = db.GetCollection s
        
        let insertOne (collection: MongoDB.Driver.IMongoCollection<'a>) (arg: 'a) : IoResult<unit>  =
            async {
                try
                    do! collection.InsertOneAsync arg |> Async.AwaitTask
                    return Ok ()
                with
                | e -> return Error e
            }