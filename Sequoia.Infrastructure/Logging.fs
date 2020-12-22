namespace Sequoia.Infrastructure

[<Interface>]
type ILog =
    abstract Debug: string -> unit
    abstract Info: string -> unit
    abstract Warning: string -> unit
    abstract Error: string -> unit

[<Interface>]
type ILogger =
    abstract GetLogger: unit -> ILog

[<RequireQualifiedAccess>]
module Logging =

    let debug (env: #ILogger) = env.GetLogger().Debug
    let info (env: #ILogger) = env.GetLogger().Info
    let warning (env: #ILogger) = env.GetLogger().Warning
    let error (env: #ILogger) = env.GetLogger().Error
