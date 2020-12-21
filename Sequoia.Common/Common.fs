namespace Sequoia.Common

[<AutoOpen>]
module Common =
    type AsyncResult<'data, 'error> = Async<Result<'data, 'error>>
    type IoResult<'data> = AsyncResult<'data, exn>
