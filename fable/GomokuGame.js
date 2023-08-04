import { Board__captureCell, Board__resetToDefaultBoard, Board__getNewBoard_76F2226C, Board_$ctor } from "./GomokuBoard.js";
import { PlayerStates, Color, GameState } from "./GomokuTypes.js";
import { class_type } from "./fable_modules/fable-library.4.1.4/Reflection.js";
import { value } from "./fable_modules/fable-library.4.1.4/Option.js";
import { printf, toConsoleError } from "./fable_modules/fable-library.4.1.4/String.js";
import { empty } from "./fable_modules/fable-library.4.1.4/List.js";

export class GomokuGame {
    constructor(target) {
        this.playerState = void 0;
        this._board = Board_$ctor();
        this._target = 5;
        this.gameState = (new GameState(2, []));
        if (target == null) {
        }
        else {
            const t = target | 0;
            this._target = (t | 0);
        }
    }
}

export function GomokuGame_$reflection() {
    return class_type("GomokuGame.GomokuGame", void 0, GomokuGame);
}

export function GomokuGame_$ctor_71136F3F(target) {
    return new GomokuGame(target);
}

export function GomokuGame__init(this$, playerOne, size) {
    this$.playerState = playerOne;
    this$.gameState = (new GameState(1, []));
    return [this$.playerState, this$.gameState, Board__getNewBoard_76F2226C(this$._board, size)];
}

export function GomokuGame__resetGame(this$, playerOne, config) {
    this$.playerState = playerOne;
    this$.gameState = (new GameState(1, []));
    const newBoard = (config[0] != null) ? config[0] : ((config[1] != null) ? Board__getNewBoard_76F2226C(this$._board, value(config[1])) : Board__resetToDefaultBoard(this$._board));
    return [this$.playerState, this$.gameState, newBoard];
}

export function GomokuGame__update(this$, position, board) {
    const matchValue = this$.gameState;
    switch (matchValue.tag) {
        case 1: {
            const matchValue_1 = this$.playerState;
            if (matchValue_1 != null) {
                const c = matchValue_1.fields[0];
                const matchValue_2 = Board__captureCell(this$._board, this$._target, position, c, board);
                if (matchValue_2[0] == null) {
                    const newTurn = (c.tag === 0) ? (new Color(1, [])) : (new Color(0, []));
                    this$.playerState = (new PlayerStates(newTurn));
                    this$.gameState = (new GameState(1, []));
                    return [this$.playerState, this$.gameState, matchValue_2[1]];
                }
                else {
                    const c_1 = matchValue_2[0];
                    this$.gameState = (new GameState(0, [c_1]));
                    return [this$.playerState, this$.gameState, matchValue_2[1]];
                }
            }
            else {
                toConsoleError(printf("Error. Uninitialized game!"));
                this$.gameState = (new GameState(2, []));
                return [this$.playerState, this$.gameState, empty()];
            }
        }
        case 2: {
            toConsoleError(printf("Game is uninitalized!"));
            return [this$.playerState, this$.gameState, board];
        }
        default: {
            const arg_4 = this$.gameState;
            toConsoleError(printf("Player with %A has already won the game!"))(arg_4);
            return [this$.playerState, this$.gameState, board];
        }
    }
}

export function GomokuGame__getCurrentTurn(this$) {
    return this$.playerState;
}

export function GomokuGame__getGameState(this$) {
    return this$.gameState;
}

