const wabtFunctions = require("./wabt-functions")

/** @typedef {import("../types").WasmFunctions} WasmFunctions*/

/** @type {Uint8Array} */
let wasmBinary
/** @type {WasmFunctions} */
let wasmFunctions
const memory = new WebAssembly.Memory({ initial: 1 });
const importObject = { js: { memory: memory } }

beforeAll(() => {
  wasmBinary = wabtFunctions.build("quicksort.wat");
});

beforeEach(async done => {
  wasmFunctions = await wabtFunctions.instantiate(wasmBinary, importObject);
  done();
});

test('isLessThan', () => {
  expect(wasmFunctions.isLessThan(0, 1)).toBeTruthy();
  expect(wasmFunctions.isLessThan(1, 1)).toBeFalsy();
  expect(wasmFunctions.isLessThan(2, 1)).toBeFalsy();
});

test('swapAtIndex', () => {
  const memoryView = new Uint8Array(memory.buffer)
  memoryView[0] = 1
  memoryView[1] = 2
  wasmFunctions.swapAtIndex(0, 1)
  expect(memoryView[0]).toBe(2);
  expect(memoryView[1]).toBe(1);
});
