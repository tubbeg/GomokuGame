module GomokuBoard
open GomokuTypes

type Board(initialBoard : option<Area>, boardSize : option<Size>) =
    class
        let defaultSize = 15,15
        let defaultTarget = 5
        let mutable board : Area = []

        let mutable size = (0,0)

        let defaultCells =
            let x,y = defaultSize
            [for i in 1 .. x do
                for j in 1 .. y ->
                    Empty(j,i)]

        let updateBoard area position color =
            let cellPattern c =
                match c with
                | Empty(pos) when pos=position -> Piece(color,pos)
                | c -> c
            area |> List.map(fun e -> cellPattern e)

        let verifyCapture position area =
            let cellPattern p c =
                match c with
                | Empty(pos) when pos=p -> true
                | _ -> false
            let myCell =
                area |> List.tryFind(fun e -> (cellPattern position e))
            match myCell with
            | Some(_) ->
                true
            | _ -> false

        let testOneDimListHorizontal list target =
            let rec recursiveTest2 l red blue lastPos target =
                match l, red, blue, lastPos with
                | _, t,_,_ when t=target ->
                    Some(Red)
                | _, _,t,_ when t=target ->
                    Some(Blue)
                | Piece(Red, (x,y))::remainder, red, _, (a,_) when a=(x-1) ->
                    //update last known cell (x,y)
                    recursiveTest2 remainder (red+1) 0 (x,y) target
                | Piece(Blue, (x,y))::remainder, _, blue, (a,_) when a=(x-1) ->
                    //update last known cell (x,y)
                    recursiveTest2 remainder 0 (blue+1) (x,y) target
                | Empty(_)::remainder, red, blue, (a,b) ->
                    //ignore empty cell positions (a,b) instead of (x,y)
                    recursiveTest2 remainder red blue (a,b) target
                | _ ->
                    //printfn "Fail case: %A" c
                    None
            recursiveTest2 list 0 0 (0,0) target

        let testOneDimListVertical list target =
            let rec recursiveTest2 l red blue lastPiecePos target =
                match l, red, blue, lastPiecePos with
                | _, t,_,_ when t=target ->
                    Some(Red)
                | _, _,t,_ when t=target ->
                    Some(Blue)
                | Piece(Red, (x,y))::remainder, red, _, (_,b) when b=(y-1) ->
                    //update last known cell (x,y)
                    recursiveTest2 remainder (red+1) 0 (x,y) target
                | Piece(Blue, (x,y))::remainder, _, blue, (_,b) when b=(y-1) ->
                    //update last known cell (x,y)
                    recursiveTest2 remainder 0 (blue+1) (x,y) target
                | Empty(_)::remainder, red, blue, (a,b) ->
                    //ignore empty cell positions (a,b) instead of (x,y)
                    recursiveTest2 remainder red blue (a,b) target
                | _ ->
                    //printfn "Fail case: %A" c
                    None
            recursiveTest2 list 0 0 (0,0) target

        let testOneDimListZigZack list target =
            let rec recursiveTest2 l red blue lastPos target =
                match l, red, blue, lastPos with
                | _, t,_,_ when t=target ->
                    Some(Red)
                | _, _,t,_ when t=target ->
                    Some(Blue)
                | Piece(Red, (x,y))::remainder, red, _, (a,b) when (a=x-1 && b=y-1) ->
                    //update last known cell (x,y)
                    recursiveTest2 remainder (red+1) 0 (x,y) target
                | Piece(Blue, (x,y))::remainder, _, blue, (a,b) when (a=x-1 && b=y-1) ->
                    //update last known cell (x,y)
                    recursiveTest2 remainder 0 (blue+1) (x,y) target
                | Empty(pos)::remainder, red, blue, (a,b) ->
                    //ignore empty cell positions (a,b) instead of (x,y)
                    recursiveTest2 remainder red blue (a,b) target
                | _ ->
                    //printfn "Fail case: %A" c
                    None
            recursiveTest2 list 0 0 (0,0) target

        let testFor5inARow area target =
            let myTarget =
                match target with
                | None -> defaultTarget
                | Some(t) -> t
            //all of these could be tested in a single recursive function,
            //this solution is ineffiecent, but it's easy to understand
            let ver = testOneDimListVertical area myTarget
            let hor = testOneDimListHorizontal area myTarget
            let zig = testOneDimListZigZack area myTarget
            ver,hor,zig
        //constructor
        do
            match initialBoard, boardSize with
            | Some(b), Some(s) ->
                board <- b
                size <- s
            | _ ->
                board <- defaultCells
                size <- defaultSize
        member this.captureCell position color =
            
            //1. Verify cell
            match verifyCapture position board with
            | true ->
                //2. Update board
                board <- updateBoard board position color
                testFor5inARow board None
            | false ->
                //3. Optional: Print error 
                eprintfn "Error! Cell is not OK!"
                None, None, None

        member this.getBoard() =
            board

        member this.resetBoard() =
            board <- defaultCells
        member this.setBoard b =
            board <- b


        //support function, used for debugging purposes, not for production
        member this.printBoard (area : option<Area>, s : option<Size>) =
            let myBoard, (a,_) =
                match area, s with
                | Some(a), Some(boardSize) -> a, boardSize
                | _ -> board, size
            let printChar e =
                match e with
                | Empty((x,_)) when x<a -> "C"
                | Piece(Red, (x,_)) when x<a  -> "R"
                | Piece(Blue, (x,_)) when x<a -> "B"
                | Empty((x,_)) when x=a -> "C" + "\n"
                | Piece(Red, (x,_)) when x=a  -> "R" + "\n"
                | Piece(Blue, (x,_)) when x=a -> "B" + "\n"
                | _ -> "ERROR"
            let printItem item =
                printf "%s" (printChar item)
            for item in myBoard do printItem item
    end