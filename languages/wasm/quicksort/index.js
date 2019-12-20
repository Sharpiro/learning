/**
 * @param {any} condition
 * @param {string=} msg
 * @returns {asserts condition}
 */
function assert(condition, msg) {
  if (!condition) {
    throw new Error(msg)
  }
}

/**
 * @param {any} str
 */
function yell(str) {
  assert(typeof str === "string")

  return str.toUpperCase()
}

// let xx = 0
// assert(xx)

// let yy = xx
// console.log(yy)

/**
 * @param {string} name
 * @param {() => void} testX
 */
function testTemp(name, testX) {
  testX()
}

testTemp('swapAtIndex', () => {
  expect(0).toBe(0)
  expect(1).toBe(1)
})

// /** @typedef {import("./types").WasmFunctions} WasmFunctions*/
// /** @typedef {typeof globalThis & { WabtModule: import("wabt") }} WabtGlobalThis*/

// const watFileName = "quicksort.wat"

// fetchWat(watFileName).then(wasmText => {
//   const wabt = (/** @type {WabtGlobalThis} */ (globalThis)).WabtModule()
//   const memory = new WebAssembly.Memory({ initial: 1 })
//   const memoryView = new Uint8Array(memory.buffer)
//   memoryView[0] = 11
//   memoryView[1] = 22
//   memoryView[2] = 33
//   memoryView[3] = 44
//   memoryView[4] = 55
//   const importObject = { js: { memory: memory } }
//   const wasmModule = wabt.parseWat(watFileName, wasmText)
//   const { buffer: wasmBinary } = wasmModule.toBinary({})

//   WebAssembly.instantiate(wasmBinary, importObject).then(instantiatedSource => {
//     const wasmFunctions = /**@type {WasmFunctions}*/(instantiatedSource.instance.exports)
//     // console.log(new Uint8Array(memory.buffer, 0, 5))
//     // wasmFunctions.swapAtIndex(1, 3)
//     // wasmFunctions.swapInPlace(0, 4)
//     // console.log(isLessThan)
//     console.log(wasmFunctions.isLessThan(1, 2))

//     // console.log(new Uint8Array(memory.buffer, 0, 5))
//   })
// })

// /**
//  * @param {string} fileName
//  */
// async function fetchWat(fileName) {
//   if (fileName.slice(-4) !== ".wat") {
//     throw new Error("invalid file type")
//   }
//   const response = await fetch(fileName)
//   const bytes = await response.text()
//   return bytes
// }
