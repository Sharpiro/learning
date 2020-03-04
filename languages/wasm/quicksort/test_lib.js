import { fetchAndBuild, fetchWasm } from "./wabt-functions.js"

/** @typedef { import("./types").WasmFunctions } WasmFunctions; */

/** @type {Promise<ArrayBuffer> | undefined} */
let initPromise

/**
 * @param {string} name
 * @param {(funcs: WasmFunctions, other:WebAssembly.Memory) => void} expression
 */
export async function test(name, expression) {
  initPromise = initPromise ? initPromise : fetchAndBuild("quicksort.wat")
  // initPromise = initPromise ? initPromise : fetchWasm("quicksort.wasm")
  const wasmBinary = await initPromise
  const [funcs, memory] = (await instantiateModule(wasmBinary))
  console.log(`running '${name}'`)

  try {
    expression(/** @type { WasmFunctions } */(funcs), memory)
  } catch (err) {
    console.error(err)
    console.error(`'${name}' failed`)
  }
}

/**
 * @param {any} condition
 * @returns {asserts condition}
 */
export function assertTrue(condition) {
  if (!condition) {
    throw new Error()
  }
}

/**
 * @param {any} condition
 * @returns {asserts condition}
 */
export function assertFalse(condition) {
  if (condition) {
    throw new Error()
  }
}

/**
 * @param {ArrayLike<any>} x
 * @param {ArrayLike<any>} y
 */
export function assertEqual(x, y) {
  if (x === y) return
  if (x.length !== y.length) throw new Error("array length mismatch")

  for (let i = 0; i < x.length; i++) {
    if (x[i] !== y[i]) throw new Error(`array mismatch @ index '${i}'`)
  }
}

/**
 * @param {ArrayBuffer} wasmBinary
 * @returns {Promise<[Record<string, Function>, WebAssembly.Memory]>}
 */
async function instantiateModule(wasmBinary) {
  const memory = new WebAssembly.Memory({ initial: 1 })
  const importObject = { js: { memory: memory, log: console.log } }
  const instantiatedSource = await WebAssembly.instantiate(wasmBinary, importObject)
  /** @type { Record<string, Function> } */
  const wasmFunctions = (instantiatedSource.instance.exports)
  return [wasmFunctions, memory]
}
