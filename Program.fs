﻿
open GomukoTypes
open GomukoBoard
[<EntryPoint>]
let main args =
    let myPiece = Piece(Red, (3,3))
    let board = new Board(None)
    board.printBoard()
    0



