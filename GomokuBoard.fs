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


        //converts the area into tuple list
        let transformAreaToTuple (l : Area) color =
            let rec transformListCellToTuple  (lst) =
                match lst with
                | Piece(_, pos)::rem ->
                    pos::transformListCellToTuple rem
                | _ -> []
            
            let filterCells color lst  =
                let pattern (e : Cell) c =
                    match e with
                    | Piece(color,_) when c=color -> true
                    | _ -> false
                lst |> List.filter(fun e -> pattern e color)

            l |> filterCells color |> transformListCellToTuple

        //tests if there's a pattern in the pieces positions
        let testTarget area target color =

            let testTargetsInARow t (list : Position list) =
                //printfn "Testing list %A" lis
                let rec targetsInRow target (current : int*int*int) (last : Position) (list : Position list)  =
                    let hasReachedTarget target current =
                        match target, current with
                        | (x,y,z), (a,b,c) when (a=x||b=y||z=c) -> true
                        | _ -> false
                    let lastX,lastY = last
                    match target, current, list with
                    | targ, curr, _ when (hasReachedTarget targ curr) -> true
                    | _,(cx,cy,cz), (a,b)::rem when (a=lastX+1) ->
                        targetsInRow target (cx+1, cy, cz) (a,b) rem
                    | _,(cx,cy,cz), (a,b)::rem when (b=lastY+1) ->
                        targetsInRow target (cx, cy+1, cz) (a,b) rem
                    | _,(cx,cy,cz), (a,b)::rem when (a=lastX+1&&b=lastY+1) ->
                        targetsInRow target (cx, cy, cz+1) (a,b) rem
                    | _,_, i::rem ->
                        targetsInRow target (1,1,1) i rem
                    | _,_,_ -> false
                let current = 1,1,1
                let last = 0,0
                let target = t,t,t
                targetsInRow target current last list

            let result = transformAreaToTuple area color |> testTargetsInARow target
            match result with
            | true ->
                Some(color)
            | false ->
                None

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
                testTarget board defaultTarget color
            | false ->
                //3. Optional: Print error 
                eprintfn "Error! Cell is not OK! Position: %A" position
                None

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