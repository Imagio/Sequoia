namespace Sequoia.Data

open System
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

[<AutoOpen>]
module Entities =

    [<CLIMutable>]
    type Client =
        { [<BsonId>] ClientId: ObjectId
          Name: string
          ApiKey: string }

    [<CLIMutable>]
    type Log =
        { [<BsonId>] Id: ObjectId
          ClientId: ObjectId
          Timestamp: DateTime
          Message: string }
