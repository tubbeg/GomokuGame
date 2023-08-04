
open GomokuTypes
open GomokuBoard
open GomokuGame

let boardPrinter (b : Area) =
    let printChar e =
        match e with
        | Empty((x,_)) when x<15 -> "C"
        | Piece(Red, (x,_)) when x<15  -> "R"
        | Piece(Blue, (x,_)) when x<15 -> "B"
        | Empty((x,_)) when x=15 -> "C" + "\n"
        | Piece(Red, (x,_)) when x=15  -> "R" + "\n"
        | Piece(Blue, (x,_)) when x=15 -> "B" + "\n"
        | _ -> "ERROR"
    let printItem item =
        printf "%s" (printChar item)
    for item in b do printItem item
    b

let victoryPrinter result =
    let color, b = result
    match color with
    | Some(Red) ->   printfn "Red has won!"
    | Some(Blue) -> printfn "Blue has won!"
    | _ -> ()
    b

let boardLogger b =
    printfn "Captured cells:\n"
    b


let test5inRow (b : Board) =
    let newArea = b.getNewBoard None
    printfn "Current board: \n"
    newArea |> boardPrinter |> ignore


    newArea
    |>  b.captureCell None (1, 1) Red |> victoryPrinter
    |>  boardPrinter |> boardLogger
    |>  b.captureCell None (2, 2) Red |> victoryPrinter
    |>  boardPrinter |> boardLogger
    |>  b.captureCell None (3, 3) Blue |> victoryPrinter
    |>  boardPrinter |> boardLogger
    |>  b.captureCell None (4, 4) Red |> victoryPrinter
    |>  boardPrinter |> boardLogger
    |>  b.captureCell None (5, 5) Red |> victoryPrinter
    |>  boardPrinter |> boardLogger


    
    |> b.captureCell None (10, 10) Blue |> victoryPrinter
    |> boardPrinter |> boardLogger
    |> b.captureCell None (10, 11) Red |> victoryPrinter
    |> boardPrinter |> boardLogger
    |> b.captureCell None (10, 12) Blue |> victoryPrinter
    |> boardPrinter |> boardLogger
    |> b.captureCell None (10, 13) Blue |> victoryPrinter
    |> boardPrinter |> boardLogger
    |> b.captureCell None (10, 14) Blue |> victoryPrinter
    |> boardPrinter |> boardLogger


    
    |> b.captureCell None (10, 7) Red |> victoryPrinter
    |> boardPrinter |> boardLogger
    |> b.captureCell None (11, 7) Red |> victoryPrinter
    |> boardPrinter |> boardLogger
    |> b.captureCell None (12, 7) Red |> victoryPrinter
    |> boardPrinter |> boardLogger
    |> b.captureCell None (13, 7) Red |> victoryPrinter
    |> boardPrinter |> boardLogger
    |> b.captureCell None (14, 7) Red |> victoryPrinter
    |> boardPrinter |> boardLogger



    |> ignore
    



let statePrinter result =
    let (playerState : PlayerStates option), (gameState : GameState), board = result
    match playerState with
    | Some(Turn(Red)) -> printf "Red turn."
    | Some(Turn(Blue)) -> printf "Blue turn."
    | _ -> printfn "Error!"
    match gameState with
    | Uninitialized -> printf "Uinitialized game"
    | Running -> printf "Game running."
    | Win(c) -> printf "Color %A has won!" c
    printf "\n"
    board



let testGomokuGame() =

    let game = new GomokuGame(None)
    game.init (Turn Red) None
    |> statePrinter |> boardPrinter
    |> game.update (3,3)
    |> statePrinter |> boardPrinter
    |> game.update (8,8)
    |> statePrinter |> boardPrinter
    |> game.update (3,14)
    |> statePrinter |> boardPrinter
    |> game.update (9,9)
    |> statePrinter |> boardPrinter
    |> game.update (3,5)
    |> statePrinter |> boardPrinter
    |> game.update (10,10)
    |> statePrinter |> boardPrinter
    |> game.update (3,6)
    |> statePrinter |> boardPrinter
    |> game.update (11,11)
    |> statePrinter |> boardPrinter
    |> game.update (3,7)
    |> statePrinter |> boardPrinter
    |> game.update (12,12)
    |> statePrinter |> boardPrinter
    |> ignore
    
[<EntryPoint>]
let main args =
    let myPiece = Piece(Red, (3,3))
    let board = new Board()
    (*board.printBoard()
    board.captureCell (3, 2) Red |> ignore
    board.printBoard()
    board.captureCell (3, 2) Blue |> ignore
    board.printBoard()*)
    test5inRow board

    testGomokuGame()

    0



