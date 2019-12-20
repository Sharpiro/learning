/**
 * @param {string} watFileName
 * @returns {Uint8Array}
 */
function build(watFileName) {
    const { readFileSync } = require("fs")
    const wabt = require("wabt")()
    const watData = readFileSync(watFileName, { encoding: "utf8" })
    const wasmModule = wabt.parseWat(watFileName, watData)
    const { buffer: wasmBinary } = wasmModule.toBinary({})
    return wasmBinary
}

/**
 * @param {BufferSource} wasmBinary
 * @param {any} importObject
 */
async function instantiate(wasmBinary, importObject) {
    const instantiatedSource = await WebAssembly.instantiate(wasmBinary, importObject)

    /** @type {import("../types").WasmFunctions} */
    const wasmFunctions = (instantiatedSource.instance.exports)
    return wasmFunctions
};

module.exports = { build, instantiate }
