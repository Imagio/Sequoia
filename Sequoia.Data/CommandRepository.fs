namespace Sequoia.Data

open System
open Sequoia
open Sequoia.Common

[<RequireQualifiedAccess>]
module CommandRepository =
    
    type CreateClientAsync = Commands.CreateClientCommand -> IoResult<Client>

    let createClientAsync (env: #IStorage) : CreateClientAsync =
        fun cmd ->
            let mongoCmd db =
                db |> Mongo.get Collections.clientsCollection
                |> Mongo.insertOne
            async {
                let db = env.GetDatabase()
                let entity = { ClientId = db |> Mongo.newId
                               Name = cmd.Name
                               ApiKey = Guid.NewGuid() |> string }
                let! res = mongoCmd db entity
                match res with
                | Ok _ -> return Ok entity
                | Error e -> return  Error e
            }
