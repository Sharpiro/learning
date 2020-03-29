import { memory } from "wasm-game-of-life/wasm_game_of_life_bg";
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
  console.log(iterator.get_list());
  console.log(iterator.get_list());
  console.log(iterator.get_list());
  console.log(iterator.get_list());
  console.log(iterator.get_list());
  console.log(iterator.get_list());
  console.log(iterator.get_list());
  console.log(iterator.get_list());
  console.log(iterator.bump_output());
  console.log(new Uint32Array(memory.buffer).length);
  console.log(new Uint32Array(memory.buffer).byteLength);
  console.log("9000000", Array.from(new Uint32Array(memory.buffer)).map((v, i) => { return { v, i }; }).filter(({ v }) => v === 9000000));
  console.log("9000001", Array.from(new Uint32Array(memory.buffer)).map((v, i) => { return { v, i }; }).filter(({ v }) => v === 9000001));
  console.log(iterator);
  console.log(new Uint8Array(memory.buffer, 1114232));
  console.log(new Uint8Array(memory.buffer, 278552 * 4));

  // for (let v; v = iterator.next();) {
  //   if (v === "o") {
  //     await delay();
  //   }
  //   console.log("js_log-", v);
  // }
}

main();


// console.log("counter", counter);
// console.log(new Uint32Array(memory.buffer));
// console.log(new Uint8Array(memory.buffer, 1114120));
// // 1114120

// console.log(new Uint8Array(memory.buffer).some(i => i === 34));
// console.log(new Uint32Array(memory.buffer).some(i => i === 13));
