namespace Sequoia.Data

open MongoDB.Bson
open MongoDB.Driver
open Sequoia.Common

type ConnectionSettings =
    { Host: string
      Port: int
      Database: string }

type Database =
    private { Db: IMongoDatabase }

type DataCollection<'a> =
    private { Data: IMongoCollection<'a> }

[<Interface>]
type IStorage =
    abstract GetDatabase : unit -> Database

[<AutoOpen>]
module Storage =

    [<RequireQualifiedAccess>]
    module internal Collections =
        let [<Literal>] internal clientsCollection = "Clients"

    [<RequireQualifiedAccess>]
    module Mongo =
        let connect (c: ConnectionSettings) : Database =
            let connectionString = sprintf "mongodb://%s:%i" c.Host c.Port
            let client = MongoDB.Driver.MongoClient connectionString
            let db = client.GetDatabase c.Database
            { Db = db }

        let get s (db: Database) : DataCollection<_> =
            let collection = db.Db.GetCollection s
            { Data = collection }

        let newId (_: Database) = ObjectId.GenerateNewId()

        let insertOne (collection: DataCollection<'a>) (arg: 'a) : IoResult<unit>  =
            async {
                try
                    let dataCollection = collection.Data
                    do! dataCollection.InsertOneAsync arg |> Async.AwaitTask
                    return Ok ()
                with
                | e -> return Error e
            }
            