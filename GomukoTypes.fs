module GomukoTypes

type Color =
    | Red
    | Blue
and Cell =
    | Piece of Color*Position
    | Empty of Position
and Position =
    int*int
type Area =
    Cell list list