import { memory } from "wasm-game-of-life/wasm_game_of_life_bg";
import { Counter } from "wasm-game-of-life";

function delay() {
  return new Promise((res) => {
    setTimeout(() => {
      res();
    }, 3000);
  });
}

async function main() {
  const iterator = Counter.new("hello, world");;

  for (let v; v = iterator.next();) {
    if (v === "o") {
      await delay();
    }
    console.log("js_log-", v);
  }
}

main();


// console.log("counter", counter);
// console.log(new Uint32Array(memory.buffer));
// console.log(new Uint8Array(memory.buffer, 1114120));
// // 1114120

// console.log(new Uint8Array(memory.buffer).some(i => i === 34));
// console.log(new Uint32Array(memory.buffer).some(i => i === 13));
