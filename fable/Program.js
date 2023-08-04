import { disposeSafe, getEnumerator } from "./fable_modules/fable-library.4.1.4/Util.js";
import { printf, toConsole } from "./fable_modules/fable-library.4.1.4/String.js";
import { Board_$ctor, Board__captureCell, Board__getNewBoard_76F2226C } from "./GomokuBoard.js";
import { PlayerStates, Color } from "./GomokuTypes.js";
import { GomokuGame__init, GomokuGame__update, GomokuGame_$ctor_71136F3F } from "./GomokuGame.js";

export function boardPrinter(b) {
    const enumerator = getEnumerator(b);
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            let arg;
            const e = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            let matchResult, x_6, x_7, x_8, x_9, x_10, x_11;
            if (e.tag === 0) {
                if (e.fields[0].tag === 1) {
                    if (e.fields[1][0] < 15) {
                        matchResult = 2;
                        x_8 = e.fields[1][0];
                    }
                    else if (e.fields[1][0] === 15) {
                        matchResult = 5;
                        x_11 = e.fields[1][0];
                    }
                    else {
                        matchResult = 6;
                    }
                }
                else if (e.fields[1][0] < 15) {
                    matchResult = 1;
                    x_7 = e.fields[1][0];
                }
                else if (e.fields[1][0] === 15) {
                    matchResult = 4;
                    x_10 = e.fields[1][0];
                }
                else {
                    matchResult = 6;
                }
            }
            else if (e.fields[0][0] < 15) {
                matchResult = 0;
                x_6 = e.fields[0][0];
            }
            else if (e.fields[0][0] === 15) {
                matchResult = 3;
                x_9 = e.fields[0][0];
            }
            else {
                matchResult = 6;
            }
            switch (matchResult) {
                case 0: {
                    arg = "C";
                    break;
                }
                case 1: {
                    arg = "R";
                    break;
                }
                case 2: {
                    arg = "B";
                    break;
                }
                case 3: {
                    arg = "C\n";
                    break;
                }
                case 4: {
                    arg = "R\n";
                    break;
                }
                case 5: {
                    arg = "B\n";
                    break;
                }
                default:
                    arg = "ERROR";
            }
            toConsole(printf("%s"))(arg);
        }
    }
    finally {
        disposeSafe(enumerator);
    }
    return b;
}

export function victoryPrinter(result_, result__1) {
    const result = [result_, result__1];
    const color = result[0];
    if (color != null) {
        if (color.tag === 1) {
            toConsole(printf("Blue has won!"));
        }
        else {
            toConsole(printf("Red has won!"));
        }
    }
    return result[1];
}

export function boardLogger(b) {
    toConsole(printf("Captured cells:\n"));
    return b;
}

export function test5inRow(b) {
    let tupledArg_14, tupledArg_13, tupledArg_12, tupledArg_11, tupledArg_10, tupledArg_9, tupledArg_8, tupledArg_7, tupledArg_6, tupledArg_5, tupledArg_4, tupledArg_3, tupledArg_2, tupledArg_1, tupledArg;
    const newArea = Board__getNewBoard_76F2226C(b, void 0);
    toConsole(printf("Current board: \n"));
    boardPrinter(newArea);
    boardLogger(boardPrinter((tupledArg_14 = Board__captureCell(b, void 0, [14, 7], new Color(0, []), boardLogger(boardPrinter((tupledArg_13 = Board__captureCell(b, void 0, [13, 7], new Color(0, []), boardLogger(boardPrinter((tupledArg_12 = Board__captureCell(b, void 0, [12, 7], new Color(0, []), boardLogger(boardPrinter((tupledArg_11 = Board__captureCell(b, void 0, [11, 7], new Color(0, []), boardLogger(boardPrinter((tupledArg_10 = Board__captureCell(b, void 0, [10, 7], new Color(0, []), boardLogger(boardPrinter((tupledArg_9 = Board__captureCell(b, void 0, [10, 14], new Color(1, []), boardLogger(boardPrinter((tupledArg_8 = Board__captureCell(b, void 0, [10, 13], new Color(1, []), boardLogger(boardPrinter((tupledArg_7 = Board__captureCell(b, void 0, [10, 12], new Color(1, []), boardLogger(boardPrinter((tupledArg_6 = Board__captureCell(b, void 0, [10, 11], new Color(0, []), boardLogger(boardPrinter((tupledArg_5 = Board__captureCell(b, void 0, [10, 10], new Color(1, []), boardLogger(boardPrinter((tupledArg_4 = Board__captureCell(b, void 0, [5, 5], new Color(0, []), boardLogger(boardPrinter((tupledArg_3 = Board__captureCell(b, void 0, [4, 4], new Color(0, []), boardLogger(boardPrinter((tupledArg_2 = Board__captureCell(b, void 0, [3, 3], new Color(1, []), boardLogger(boardPrinter((tupledArg_1 = Board__captureCell(b, void 0, [2, 2], new Color(0, []), boardLogger(boardPrinter((tupledArg = Board__captureCell(b, void 0, [1, 1], new Color(0, []), newArea), victoryPrinter(tupledArg[0], tupledArg[1]))))), victoryPrinter(tupledArg_1[0], tupledArg_1[1]))))), victoryPrinter(tupledArg_2[0], tupledArg_2[1]))))), victoryPrinter(tupledArg_3[0], tupledArg_3[1]))))), victoryPrinter(tupledArg_4[0], tupledArg_4[1]))))), victoryPrinter(tupledArg_5[0], tupledArg_5[1]))))), victoryPrinter(tupledArg_6[0], tupledArg_6[1]))))), victoryPrinter(tupledArg_7[0], tupledArg_7[1]))))), victoryPrinter(tupledArg_8[0], tupledArg_8[1]))))), victoryPrinter(tupledArg_9[0], tupledArg_9[1]))))), victoryPrinter(tupledArg_10[0], tupledArg_10[1]))))), victoryPrinter(tupledArg_11[0], tupledArg_11[1]))))), victoryPrinter(tupledArg_12[0], tupledArg_12[1]))))), victoryPrinter(tupledArg_13[0], tupledArg_13[1]))))), victoryPrinter(tupledArg_14[0], tupledArg_14[1]))));
}

export function statePrinter(result_, result__1, result__2) {
    const result = [result_, result__1, result__2];
    const playerState = result[0];
    const gameState = result[1];
    if (playerState != null) {
        if (playerState.fields[0].tag === 1) {
            toConsole(printf("Blue turn."));
        }
        else {
            toConsole(printf("Red turn."));
        }
    }
    else {
        toConsole(printf("Error!"));
    }
    switch (gameState.tag) {
        case 1: {
            toConsole(printf("Game running."));
            break;
        }
        case 0: {
            toConsole(printf("Color %A has won!"))(gameState.fields[0]);
            break;
        }
        default:
            toConsole(printf("Uinitialized game"));
    }
    toConsole(printf("\n"));
    return result[2];
}

export function testGomokuGame() {
    let tupledArg_10, tupledArg_9, tupledArg_8, tupledArg_7, tupledArg_6, tupledArg_5, tupledArg_4, tupledArg_3, tupledArg_2, tupledArg_1, tupledArg;
    const game = GomokuGame_$ctor_71136F3F(void 0);
    boardPrinter((tupledArg_10 = GomokuGame__update(game, [12, 12], boardPrinter((tupledArg_9 = GomokuGame__update(game, [3, 7], boardPrinter((tupledArg_8 = GomokuGame__update(game, [11, 11], boardPrinter((tupledArg_7 = GomokuGame__update(game, [3, 6], boardPrinter((tupledArg_6 = GomokuGame__update(game, [10, 10], boardPrinter((tupledArg_5 = GomokuGame__update(game, [3, 5], boardPrinter((tupledArg_4 = GomokuGame__update(game, [9, 9], boardPrinter((tupledArg_3 = GomokuGame__update(game, [3, 14], boardPrinter((tupledArg_2 = GomokuGame__update(game, [8, 8], boardPrinter((tupledArg_1 = GomokuGame__update(game, [3, 3], boardPrinter((tupledArg = GomokuGame__init(game, new PlayerStates(new Color(0, [])), void 0), statePrinter(tupledArg[0], tupledArg[1], tupledArg[2])))), statePrinter(tupledArg_1[0], tupledArg_1[1], tupledArg_1[2])))), statePrinter(tupledArg_2[0], tupledArg_2[1], tupledArg_2[2])))), statePrinter(tupledArg_3[0], tupledArg_3[1], tupledArg_3[2])))), statePrinter(tupledArg_4[0], tupledArg_4[1], tupledArg_4[2])))), statePrinter(tupledArg_5[0], tupledArg_5[1], tupledArg_5[2])))), statePrinter(tupledArg_6[0], tupledArg_6[1], tupledArg_6[2])))), statePrinter(tupledArg_7[0], tupledArg_7[1], tupledArg_7[2])))), statePrinter(tupledArg_8[0], tupledArg_8[1], tupledArg_8[2])))), statePrinter(tupledArg_9[0], tupledArg_9[1], tupledArg_9[2])))), statePrinter(tupledArg_10[0], tupledArg_10[1], tupledArg_10[2])));
}

(function (args) {
    test5inRow(Board_$ctor());
    testGomokuGame();
    return 0;
})(typeof process === 'object' ? process.argv.slice(2) : []);

