open System
open Sequoia.Serilog
open Sequoia.Infrastructure

[<Interface>]
type IEnv =
    inherit ILogger

[<EntryPoint>]
let main argv =

    let log = configureLog ()

    let env =
        { new IEnv with
            member __.GetLogger() = log }

    Logging.verbose env "Verbose message"
    Logging.debug env "Debug message"
    Logging.info env "Info message"

    Logging.warninga env "Warning message: {string}, {obj}, {int}, {float}"
    <| [| "data"; obj (); 1; 4.5 |]

    Logging.error env "Error message"
    <| Exception("Test")

    Logging.fatal env "Fatal message"
    <| Exception("Fatal")

    0 // return an integer exit code
