import { Union } from "./fable_modules/fable-library.4.1.4/Types.js";
import { tuple_type, int32_type, union_type } from "./fable_modules/fable-library.4.1.4/Reflection.js";

export class Color extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Red", "Blue"];
    }
}

export function Color_$reflection() {
    return union_type("GomokuTypes.Color", [], Color, () => [[], []]);
}

export class PlayerStates extends Union {
    constructor(Item) {
        super();
        this.tag = 0;
        this.fields = [Item];
    }
    cases() {
        return ["Turn"];
    }
}

export function PlayerStates_$reflection() {
    return union_type("GomokuTypes.PlayerStates", [], PlayerStates, () => [[["Item", Color_$reflection()]]]);
}

export class Cell extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Piece", "Empty"];
    }
}

export function Cell_$reflection() {
    return union_type("GomokuTypes.Cell", [], Cell, () => [[["Item1", Color_$reflection()], ["Item2", tuple_type(int32_type, int32_type)]], [["Item", tuple_type(int32_type, int32_type)]]]);
}

export class GameState extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Win", "Running", "Uninitialized"];
    }
}

export function GameState_$reflection() {
    return union_type("GomokuTypes.GameState", [], GameState, () => [[["Item", Color_$reflection()]], [], []]);
}

export class Dimension extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["X", "Y", "Z"];
    }
}

export function Dimension_$reflection() {
    return union_type("GomokuTypes.Dimension", [], Dimension, () => [[], [], []]);
}

