namespace Sequoia.Serilog

open Sequoia.Infrastructure
open Serilog

[<AutoOpen>]
module Common =
    let configureLog (): ILog =
        let logger =
            LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithMachineName()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel = Events.LogEventLevel.Verbose,
                                 theme = Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
                .WriteTo.File(formatter = Serilog.Formatting.Compact.CompactJsonFormatter(),
                              path = "sequoia.clef",
                              restrictedToMinimumLevel = Events.LogEventLevel.Information,
                              fileSizeLimitBytes = 10000000L,
                              retainedFileCountLimit = 10)
                .CreateLogger()

        { new ILog with
            member __.Verbose s a = logger.Verbose(s, a)
            member __.Debug s a = logger.Debug(s, a)
            member __.Info s a = logger.Information(s, a)
            member __.Warning s a = logger.Warning(s, a)
            member __.Error s e a = logger.Error(e, s, a)
            member __.Fatal s e a = logger.Fatal(e, s, a) }
