namespace Sequoia.Data

open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

[<AutoOpen>]
module Entities =

    [<CLIMutable>]
    type Client =
        { [<BsonId>] Id: ObjectId
          Name: string
          ApiKey: string }

