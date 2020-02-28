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
  const initData = await instantiate(wasmBinary)
  console.log(`running '${name}'`)

  try {
    expression(...initData)
  } catch (err) {
    console.error(err)
    console.error(`'${name}' failed`)
  }
}

/**
 * @param {any} condition
 * @param {string=} msg
 * @returns {asserts condition}
 */
export function assertTrue(condition, msg) {
  if (!condition) {
    throw new Error(msg)
  }
}

/**
 * @param {any} condition
 * @param {string=} msg
 * @returns {asserts condition}
 */
export function assertFalse(condition, msg) {
  if (condition) {
    throw new Error(msg)
  }
}

/**
 * @param {ArrayBuffer} wasmBinary
 * @returns {Promise<[WasmFunctions, WebAssembly.Memory]>}
 */
async function instantiate(wasmBinary) {
  const memory = new WebAssembly.Memory({ initial: 1 })
  const importObject = { js: { memory: memory, log: console.log } }
  const instantiatedSource = await WebAssembly.instantiate(wasmBinary, importObject)
  /** @type { import("./types").WasmFunctions } */
  const wasmFunctions = (instantiatedSource.instance.exports)
  return [wasmFunctions, memory]
}