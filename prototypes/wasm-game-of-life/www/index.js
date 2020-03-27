//@ts-check

import * as wasm from "wasm-game-of-life";

const counter = wasm.get_counter();
console.log(counter[4]);
