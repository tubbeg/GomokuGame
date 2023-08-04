module GomokuBoard
open GomokuTypes

//user of Board class has to maintain state
type Board() =
    class
        let defaultSize = 15,15
        let defaultTarget = 5
        let generateBoard (size : option<Size>) =
            let x,y = 
                match size with
                | None -> defaultSize
                | Some(a,b) -> a,b
            [for i in 1 .. x do
                for j in 1 .. y ->
                    Empty(j,i)]

        let updateBoard area position color =
            let cellPattern c =
                match c with
                | Empty(pos) when pos=position ->
                    Piece(color,pos)
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
        
        let filterEmptyCells  color list =
            list |> List.where(fun e ->
                match e with
                | Piece(c,_) when c=color -> true
                | _ -> false
            )

        let convertCellToTuple list : (Position list) =
            let rec convertListToTuple lst =
                match lst with
                | Piece(_, p)::rem -> p::convertListToTuple rem
                | _ -> []
            list |> convertListToTuple

        //when the current number of pieces in a row matches the target then it
        //means the player has won!
        let hasReachedTarget target current =
            match target, current with
            | (x,y,z), (a,b,c) when (a=x-1||b=y-1||c=z-1) -> true
            | _ -> false

        let matchesXYX tuple lastCell dimension =
            let a,b = tuple
            match lastCell, dimension with
            | (x,_), X when (a=x+1) -> true
            | (_,y), X when (b=y+1) -> true
            | (x,y), X when (a=x+1&&b=y+1) -> true
            | c ->
                //printfn "Fail case: %A" c
                false

        let testTargetsInARow t (list : Position list) =
            //printfn "Testing list %A" lis
            let targ = t,t,t
            let resetCurrent = (1,1,1)
            let initPos = (0,0)
            let rec targetsInRow target (last : Position)  (list : Position list) current  =
                let addToCurrent (cx,cy,cz) dim =
                    match dim with
                    | X ->
                        (cx+1,cy,cz)
                    | Y ->
                        (cx,cy+1,cz)
                    | Z ->
                        (cx,cy,cz+1)
                
                //current = current number of pieces in row
                match target, current, list with
                | targ, curr, _ when (hasReachedTarget targ curr) ->
                    true
                | _, curr, pos::rem when (matchesXYX  pos last X) ->
                    addToCurrent curr X |> targetsInRow  target pos rem
                | _,curr, pos::rem when (matchesXYX  pos last Y) ->
                    addToCurrent curr Y |> targetsInRow  target pos rem
                | _,curr, pos::rem when (matchesXYX  pos last Z) ->
                    addToCurrent curr Z |> targetsInRow  target pos rem
                | _,_, i::rem ->
                    targetsInRow target i rem resetCurrent
                | _,_,_ ->
                    false
            resetCurrent |> targetsInRow targ initPos list //target is the same for all dimensions

        //tests if there's a pattern in the pieces positions
        let testTarget (area : Area) target (color : Color) =
            area
            |> filterEmptyCells color
            |> convertCellToTuple
            |> testTargetsInARow target

        member this.captureCell t  position color board =
            
            //1. Verify cell
            match verifyCapture position board with
            | true ->
                //2. Update 
                let update = updateBoard board position color
                let myTarget =
                    match t with
                    | None -> defaultTarget
                    | Some(targ) -> targ
                match testTarget board myTarget color with
                | true -> Some(color), update
                | _ -> None, update
            | false ->
                //3. Optional: Print error 
                eprintfn "Error! Cell is not OK! Position: %A" position
                None, board

        member this.getNewBoard size : Area =
            generateBoard size

        member this.resetToDefaultBoard() : Area =
            generateBoard None

    end