import { memory as wasmMemory } from "wasm-game-of-life/wasm_game_of_life_bg";
import { ProgramIterator } from "wasm-game-of-life";

function delay() {
  return new Promise((res) => {
    setTimeout(() => {
      res();
    }, 3000);
  });
}

async function main() {
  const iterator = ProgramIterator.new("hello, world");;
  // for (let v; v = iterator.next();) {
  //   // if (v === "o") {
  //   //   await delay();
  //   // }
  //   console.log("js_log-", v);
  // }

  const memoryPtr = iterator.get_memory_ptr();
  const memory = new Uint8Array(wasmMemory.buffer, memoryPtr, 9);
  console.log(iterator.get_output());
  console.log(memoryPtr);
  console.log(memory);
  iterator.bump_memory();
  console.log(memory);
}

main();


// console.log("counter", counter);
// console.log(new Uint32Array(memory.buffer));
// console.log(new Uint8Array(memory.buffer, 1114120));
// // 1114120

// console.log(new Uint8Array(memory.buffer).some(i => i === 34));
// console.log(new Uint32Array(memory.buffer).some(i => i === 13));
