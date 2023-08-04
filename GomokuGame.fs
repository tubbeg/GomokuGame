module GomokuGame
open GomokuBoard
open GomokuTypes

type GomokuGame(target) =
    class

        let mutable playerState : PlayerStates option = None
        let _board = new Board()

        let mutable _target = 5

        let mutable gameState = Uninitialized


        //constructor
        do
            match target with
            | Some(t) ->
                _target <- t
            | None ->
                ()

        member this.init playerOne size =
            playerState <- Some(playerOne)
            gameState <- Running
            (playerState, gameState, _board.getNewBoard size)

        member this.resetGame playerOne config =
            playerState <- Some(playerOne)
            gameState <- Running
            let newBoard =
                match config with
                | Some(b), _ -> b
                | _, Some(s) -> _board.getNewBoard s
                | _ -> _board.resetToDefaultBoard()
            playerState, gameState, newBoard

        member this.update position board =
            match gameState with
            | Running ->
                match playerState with
                | Some(Turn(c)) ->
                    match _board.captureCell (Some _target)  position c board with
                    | Some(c),b ->
                        gameState <- Win(c)
                        playerState, gameState, b
                    | None,b -> 
                        //printfn "Running with response: %A" result
                        let newTurn =
                            match c with
                            | Red -> Blue
                            | _ -> Red
                        playerState <- Some(Turn(newTurn))
                        gameState <- Running
                        playerState, gameState, b
                | _ ->
                    eprintfn "Error. Uninitialized game!"
                    gameState <- Uninitialized
                    playerState, gameState, []
            | Uninitialized ->
                eprintfn "Game is uninitalized!"
                playerState, gameState, board
            | _ ->
                eprintfn "Player with %A has already won the game!" gameState
                playerState, gameState, board

        member this.getCurrentTurn() = playerState

        member this.getGameState() = gameState

    end