module GomokuTypes

type Color =
    | Red
    | Blue
and PlayerStates = Turn of Color
and Cell =
    | Piece of Color*Position
    | Empty of Position
and GameState =
    | Win of Color
    | Running
    | Uninitialized
and Position =
    int*int
type Size =
    int*int
type Area = Cell list
type Dimension =
    | X
    | Y
    | Z