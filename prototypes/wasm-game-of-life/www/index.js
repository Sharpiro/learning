import { memory as wasmMemory } from "wasm-game-of-life/wasm_game_of_life_bg";
import { ProgramIterator } from "wasm-game-of-life";

async function main() {
  const memorySize = 10;
  const iterator = ProgramIterator.new("hello, world", memorySize);
  // for (let v; v = iterator.next();) {
  //   console.log("js_log-", v);
  // }

  const memoryPtr = iterator.get_memory_ptr();
  const memorySlice = new Uint8Array(wasmMemory.buffer, memoryPtr, memorySize);
  console.log(memoryPtr);
  iterator.bump_memory();
  console.log(memoryPtr);
  iterator.bump_memory();
  console.log(memoryPtr);
  // outputPtr = iterator.get_memory_ptr();
  // console.log(outputPtr);
  // console.log(new Uint8Array(wasmMemory.buffer, outputPtr));
  // const outputSlice = new Uint8Array(wasmMemory.buffer, outputPtr, 900);
  // console.log(outputSlice);
  // iterator.bump_output();
  // console.log(outputSlice);
  // outputPtr = iterator.get_output_ptr();
  // console.log(outputPtr)
}

main();
