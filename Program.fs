
open GomokuTypes
open GomokuBoard
open GomokuGame


let victoryPrinter result =
    match result with
    | Some(Red) -> printfn "Red has won!"
    | Some(Blue) -> printfn "Blue has won!"
    | _ -> ()


let test5inRow (b : Board) =
    b.resetBoard()
    printfn "Current board: \n"
    b.printBoard(None, None)



    (*b.captureCell (1, 1) Red |> victoryPrinter
    b.captureCell (2, 1) Red |> victoryPrinter
    b.captureCell (3, 1) Red |> victoryPrinter
    //b.captureCell (4, 1) Red |> ignore
    b.captureCell (12, 12) Red |> victoryPrinter

    b.captureCell (9, 4) Blue |> victoryPrinter
    b.captureCell (10, 4) Blue |> victoryPrinter
    b.captureCell (11, 4) Blue |> victoryPrinter
    b.captureCell (12, 4) Blue |> victoryPrinter*)
    //b.captureCell (13, 4) Blue |> ignore

    printfn "Captured cells: \n"

    b.printBoard(None, None)
    

    //b.captureCell (13,4) Blue |> victoryPrinter
    

    //b.captureCell (9, 4) Blue |> victoryPrinter
    b.captureCell (1, 1) Red |> victoryPrinter
    b.captureCell (2, 2) Red |> victoryPrinter
    b.captureCell (3, 3) Red |> victoryPrinter
    b.captureCell (4, 4) Red |> victoryPrinter
    b.captureCell (5, 5) Red |> victoryPrinter
    

    
    b.captureCell (8, 10) Blue |> victoryPrinter
    b.captureCell (8, 9) Blue |> victoryPrinter
    b.captureCell (8, 11) Blue |> victoryPrinter
    //b.captureCell (8, 12) Blue |> victoryPrinter
    b.captureCell (8, 13) Blue |> victoryPrinter


    b.captureCell (9, 13) Blue |> victoryPrinter
    b.captureCell (10, 13) Blue |> victoryPrinter
    b.captureCell (11, 13) Blue |> victoryPrinter
    //b.captureCell (12, 13) Blue |> victoryPrinter
    b.captureCell (13, 13) Blue |> victoryPrinter
    
    

    printfn "Captured cells: \n"
    
    b.printBoard(None, None)
    //match b.verifyVictory() with
    //| false, true, _ -> printfn "red has won!"
    //| true, false, _ -> printfn "blue has won!"
    //| _ -> printfn "Something wrong"


let statePrinter state =
    let p, g, b = state
    match p with
    | Some(Turn(Red)) -> printf "Red turn."
    | Some(Turn(Blue)) -> printf "Blue turn."
    | _ -> printfn "Error!"
    match g with
    | Uninitialized -> printf "Uinitialized game"
    | Running -> printf "Game running."
    | RedWin -> printf "Red wins!"
    | BlueWin -> printf "Blue wins!"
    printf "\n"
    b

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

let testGomokuGame() =

    let game = new GomokuGame(None)
    game.init(Turn(Red)) |> statePrinter |> boardPrinter


    game.update(3,1) |> statePrinter |> boardPrinter
    game.update(12,3) |> statePrinter |> boardPrinter

    game.update(3,2) |> statePrinter |> boardPrinter
    game.update(11,3) |> statePrinter |> boardPrinter

    game.update(3,3) |> statePrinter |> boardPrinter
    game.update(10,3) |> statePrinter |> boardPrinter



    game.update(3,4) |> statePrinter |> boardPrinter
    game.update(9,3) |> statePrinter |> boardPrinter


    game.update(3,5) |> statePrinter |> boardPrinter
    game.update(8,3) |> statePrinter |> boardPrinter
    
    

    ()


[<EntryPoint>]
let main args =
    let myPiece = Piece(Red, (3,3))
    let board = new Board(None, None)
    (*board.printBoard()
    board.captureCell (3, 2) Red |> ignore
    board.printBoard()
    board.captureCell (3, 2) Blue |> ignore
    board.printBoard()*)
    test5inRow board

    //testGomokuGame()

    0



