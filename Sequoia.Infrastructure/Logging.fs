namespace Sequoia.Infrastructure

[<Interface>]
type ILog =
    abstract Verbose: string -> array<obj> -> unit
    abstract Debug: string -> array<obj> -> unit
    abstract Info: string -> array<obj> -> unit
    abstract Warning: string -> array<obj> -> unit
    abstract Error: string -> exn -> array<obj> -> unit
    abstract Fatal: string -> exn -> array<obj> -> unit

[<Interface>]
type ILogger =
    abstract GetLogger: unit -> ILog

[<RequireQualifiedAccess>]
module Logging =
    let verbosea (env: #ILogger) = env.GetLogger().Verbose
    let verbose (env: #ILogger) msg = verbosea env msg [||]

    let debuga (env: #ILogger) = env.GetLogger().Debug
    let debug (env: #ILogger) msg = debuga env msg [||]

    let infoa (env: #ILogger) = env.GetLogger().Info
    let info (env: #ILogger) msg = infoa env msg [||]

    let warninga (env: #ILogger) = env.GetLogger().Warning
    let warning (env: #ILogger) msg = warninga env msg [||]

    let errora (env: #ILogger) = env.GetLogger().Error
    let error (env: #ILogger) msg exc = errora env msg exc [||]

    let fatala (env: #ILogger) = env.GetLogger().Fatal
    let fatal (env: #ILogger) msg exc = fatala env msg exc [||]
