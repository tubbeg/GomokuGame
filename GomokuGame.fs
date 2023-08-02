module GomokuGame
open GomokuBoard
open GomokuTypes

type GomokuGame(config) =
    class

        let mutable playerState : option<PlayerStates> = None
        let mutable board : Board = new Board(None, None)

        let mutable gameState = Uninitialized

        do
            match config with
            | Some(a,s) ->
                board <- new Board(a,s)
            | _ ->
                board <- new Board(None,None)

        member this.init playerOne =
            playerState <- Some(playerOne)
            gameState <- Running
            playerState, gameState, board.getBoard()

        member this.resetGame playerOne config =
            playerState <- Some(playerOne)
            gameState <- Running
            match config with
            | Some(a,s) ->
                board <- new Board(a,s)
            | _ ->
                board <- new Board(None,None)
            playerState, gameState, board.getBoard()

        member this.update position =
            match gameState with
            | Running ->
                match playerState with
                | Some(Turn(c)) ->
                    let result = board.captureCell position c
                    match result with
                    | Some(Red) ->
                        gameState <- RedWin
                        playerState, gameState, board.getBoard()
                    | Some(Blue) ->
                        gameState <- BlueWin
                        playerState, gameState, board.getBoard()
                    | None -> 
                        printfn "Running with response: %A" result
                        let newTurn =
                            match c with
                            | Red -> Blue
                            | _ -> Red
                        playerState <- Some(Turn(newTurn))
                        gameState <- Running
                        playerState, gameState, board.getBoard()
                | _ ->
                    eprintfn "Error. Uninitialized game!"
                    gameState <- Uninitialized
                    playerState, gameState, board.getBoard()
            | Uninitialized ->
                eprintfn "Game is uninitalized!"
                playerState, gameState, board.getBoard()
            | _ ->
                eprintfn "Player with %A has already won the game!" gameState
                playerState, gameState, board.getBoard()

        member this.getCurrentTurn() = playerState

        member this.getGameState() = gameState

        member this.getBoard() = board.getBoard()

    end