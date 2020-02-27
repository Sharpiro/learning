import { fetchAndBuild } from "./wabt-functions.js"

/** @typedef { import("./types").WasmFunctions } WasmFunctions; */

const watFileName = "quicksort.wat"
/** @type {Promise<Uint8Array> | undefined} */
let initPromise

/**
 * @param {string} name
 * @param {(funcs: WasmFunctions, other:WebAssembly.Memory) => void} expression
 */
export async function test(name, expression) {
  initPromise = initPromise ? initPromise : fetchAndBuild(watFileName)
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
 * @param {Uint8Array} wasmBinary
 * @returns {Promise<[WasmFunctions, WebAssembly.Memory]>}
 */
async function instantiate(wasmBinary) {
  const memory = new WebAssembly.Memory({ initial: 1 })
  const importObject = { js: { memory: memory } }
  const instantiatedSource = await WebAssembly.instantiate(wasmBinary, importObject)
  /** @type {import("./types").WasmFunctions} */
  const wasmFunctions = (instantiatedSource.instance.exports)
  return [wasmFunctions, memory]
}