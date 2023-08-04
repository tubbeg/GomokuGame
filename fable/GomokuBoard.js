import { class_type } from "./fable_modules/fable-library.4.1.4/Reflection.js";
import { printf, toConsoleError } from "./fable_modules/fable-library.4.1.4/String.js";
import { map, collect, delay, toList } from "./fable_modules/fable-library.4.1.4/Seq.js";
import { Dimension, Cell } from "./GomokuTypes.js";
import { rangeDouble } from "./fable_modules/fable-library.4.1.4/Range.js";
import { tail, head, isEmpty, empty, cons, where, tryFind, map as map_1 } from "./fable_modules/fable-library.4.1.4/List.js";
import { equals, equalArrays } from "./fable_modules/fable-library.4.1.4/Util.js";

export class Board {
    constructor() {
        this.defaultSize = [15, 15];
        this.defaultTarget = 5;
    }
}

export function Board_$reflection() {
    return class_type("GomokuBoard.Board", void 0, Board);
}

export function Board_$ctor() {
    return new Board();
}

export function Board__captureCell(this$, t, position, color, board) {
    if (Board__verifyCapture(this$, position[0], position[1], board)) {
        const update = Board__updateBoard(this$, board, position[0], position[1], color);
        if (Board__testTarget(this$, board, (t != null) ? t : this$.defaultTarget, color)) {
            return [color, update];
        }
        else {
            return [void 0, update];
        }
    }
    else {
        const tupledArg = position;
        toConsoleError(printf("Error! Cell is not OK! Position: %A"))([tupledArg[0], tupledArg[1]]);
        return [void 0, board];
    }
}

export function Board__getNewBoard_76F2226C(this$, size) {
    return Board__generateBoard_76F2226C(this$, size);
}

export function Board__resetToDefaultBoard(this$) {
    return Board__generateBoard_76F2226C(this$, void 0);
}

export function Board__generateBoard_76F2226C(this$, size) {
    const patternInput = (size != null) ? [size[0], size[1]] : this$.defaultSize;
    return toList(delay(() => collect((i) => map((j) => (new Cell(1, [[j, i]])), rangeDouble(1, 1, patternInput[1])), rangeDouble(1, 1, patternInput[0]))));
}

export function Board__updateBoard(this$, area, position_, position__1, color) {
    return map_1((e) => {
        const c = e;
        let matchResult, pos_1, c_1;
        if (c.tag === 1) {
            if (equalArrays(c.fields[0], [position_, position__1])) {
                matchResult = 0;
                pos_1 = c.fields[0];
            }
            else {
                matchResult = 1;
                c_1 = c;
            }
        }
        else {
            matchResult = 1;
            c_1 = c;
        }
        switch (matchResult) {
            case 0:
                return new Cell(0, [color, pos_1]);
            default:
                return c_1;
        }
    }, area);
}

export function Board__verifyCapture(this$, position_, position__1, area) {
    if (tryFind((e) => {
        const c = e;
        let matchResult, pos_1;
        if (c.tag === 1) {
            if (equalArrays(c.fields[0], [position_, position__1])) {
                matchResult = 0;
                pos_1 = c.fields[0];
            }
            else {
                matchResult = 1;
            }
        }
        else {
            matchResult = 1;
        }
        switch (matchResult) {
            case 0:
                return true;
            default:
                return false;
        }
    }, area) != null) {
        return true;
    }
    else {
        return false;
    }
}

export function Board__filterEmptyCells(this$, color, list) {
    return where((e) => {
        let matchResult, c_1;
        if (e.tag === 0) {
            if (equals(e.fields[0], color)) {
                matchResult = 0;
                c_1 = e.fields[0];
            }
            else {
                matchResult = 1;
            }
        }
        else {
            matchResult = 1;
        }
        switch (matchResult) {
            case 0:
                return true;
            default:
                return false;
        }
    }, list);
}

export function Board__convertCellToTuple_Z4505FFB0(this$, list) {
    const convertListToTuple = (lst) => {
        let matchResult, p, rem;
        if (!isEmpty(lst)) {
            if (head(lst).tag === 0) {
                matchResult = 0;
                p = head(lst).fields[1];
                rem = tail(lst);
            }
            else {
                matchResult = 1;
            }
        }
        else {
            matchResult = 1;
        }
        switch (matchResult) {
            case 0:
                return cons(p, convertListToTuple(rem));
            default:
                return empty();
        }
    };
    return convertListToTuple(list);
}

export function Board__hasReachedTarget(this$, target_, target__1, target__2, current_, current__1, current__2) {
    const target = [target_, target__1, target__2];
    const current = [current_, current__1, current__2];
    if (((current[0] === (target[0] - 1)) ? true : (current[1] === (target[1] - 1))) ? true : (current[2] === (target[2] - 1))) {
        return true;
    }
    else {
        return false;
    }
}

export function Board__matchesXYX(this$, tuple_, tuple__1, lastCell_, lastCell__1, dimension) {
    const tuple = [tuple_, tuple__1];
    const lastCell = [lastCell_, lastCell__1];
    const b = tuple[1] | 0;
    const a = tuple[0] | 0;
    const matchValue = [lastCell, dimension];
    let matchResult, c;
    if (dimension.tag === 0) {
        if (a === (lastCell[0] + 1)) {
            matchResult = 0;
        }
        else if (b === (lastCell[1] + 1)) {
            matchResult = 1;
        }
        else if ((a === (lastCell[0] + 1)) && (b === (lastCell[1] + 1))) {
            matchResult = 2;
        }
        else {
            matchResult = 3;
            c = matchValue;
        }
    }
    else {
        matchResult = 3;
        c = matchValue;
    }
    switch (matchResult) {
        case 0:
            return true;
        case 1:
            return true;
        case 2:
            return true;
        default:
            return false;
    }
}

export function Board__testTargetsInARow(this$, t, list) {
    const resetCurrent = [1, 1, 1];
    const targetsInRow = (target_mut, last_mut, list_1_mut, current_mut) => {
        let targ_1, curr, pos, pos_1, pos_2;
        targetsInRow:
        while (true) {
            const target = target_mut, last = last_mut, list_1 = list_1_mut, current = current_mut;
            const addToCurrent = (tupledArg, dim) => {
                const cx = tupledArg[0] | 0;
                const cy = tupledArg[1] | 0;
                const cz = tupledArg[2] | 0;
                switch (dim.tag) {
                    case 1:
                        return [cx, cy + 1, cz];
                    case 2:
                        return [cx, cy, cz + 1];
                    default:
                        return [cx + 1, cy, cz];
                }
            };
            if ((targ_1 = target, (curr = current, Board__hasReachedTarget(this$, targ_1[0], targ_1[1], targ_1[2], curr[0], curr[1], curr[2])))) {
                return true;
            }
            else if (!isEmpty(list_1)) {
                if ((pos = head(list_1), Board__matchesXYX(this$, pos[0], pos[1], last[0], last[1], new Dimension(0, [])))) {
                    target_mut = target;
                    last_mut = head(list_1);
                    list_1_mut = tail(list_1);
                    current_mut = addToCurrent(current, new Dimension(0, []));
                    continue targetsInRow;
                }
                else if ((pos_1 = head(list_1), Board__matchesXYX(this$, pos_1[0], pos_1[1], last[0], last[1], new Dimension(1, [])))) {
                    target_mut = target;
                    last_mut = head(list_1);
                    list_1_mut = tail(list_1);
                    current_mut = addToCurrent(current, new Dimension(1, []));
                    continue targetsInRow;
                }
                else if ((pos_2 = head(list_1), Board__matchesXYX(this$, pos_2[0], pos_2[1], last[0], last[1], new Dimension(2, [])))) {
                    target_mut = target;
                    last_mut = head(list_1);
                    list_1_mut = tail(list_1);
                    current_mut = addToCurrent(current, new Dimension(2, []));
                    continue targetsInRow;
                }
                else {
                    target_mut = target;
                    last_mut = head(list_1);
                    list_1_mut = tail(list_1);
                    current_mut = resetCurrent;
                    continue targetsInRow;
                }
            }
            else {
                return false;
            }
            break;
        }
    };
    return targetsInRow([t, t, t], [0, 0], list, resetCurrent);
}

export function Board__testTarget(this$, area, target, color) {
    return Board__testTargetsInARow(this$, target, Board__convertCellToTuple_Z4505FFB0(this$, Board__filterEmptyCells(this$, color, area)));
}

