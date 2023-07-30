module GomukoBoard
open GomukoTypes

type Board(initialBoard) =
    class
        let mutable board : Area = []
        let defaultCells =
            [for i in 1 .. 15 ->
                ([for j in 1 .. 15 -> Empty(j,i)])]
        do
            match initialBoard with
            | Some(b) ->
                board <- b
            | None ->
                board <- defaultCells
        member this.captureCell position =
            ()
        member this.getBoard() = board
        //support function, used for debugging purposes, not for production
        member this.printBoard() =
            board |> List.iter(fun e -> printfn "%A" e)
    end