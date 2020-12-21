namespace Sequoia.Data

open System
open MongoDB.Bson
open Sequoia
open Sequoia.Common

[<RequireQualifiedAccess>]
module CommandRepository =
    
    type CreateClientAsync = Commands.CreateClientCommand -> IoResult<Client>

    let createClientAsync (env: IStorage) : CreateClientAsync =
        fun cmd ->
            let mongoCmd db =
                db |> Mongo.get Collections.clientsCollection
                |> Mongo.insertOne
            async {
                let entity = { Id = ObjectId.GenerateNewId()
                               Name = cmd.Name
                               ApiKey = Guid.NewGuid() |> string }
                let db = env.GetDatabase()
                let! res = mongoCmd db entity
                match res with
                | Ok _ -> return Ok entity
                | Error e -> return  Error e
            }