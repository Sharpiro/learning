const wabt = /** @type { import("wabt") } */ ((/** @type { any } */ (globalThis)).WabtModule())

/**
 * @param {string} fileName
 */
export async function fetchWat(fileName) {
    if (fileName.slice(-4) !== ".wat") {
        throw new Error("invalid file type")
    }
    const response = await fetch(fileName)
    const bytes = await response.text()
    return bytes
}

/**
 * @param {string} fileName
 */
export async function fetchWasm(fileName) {
    if (fileName.slice(-5) !== ".wasm") {
        throw new Error("invalid file type")
    }
    const response = await fetch(fileName)
    const bytes = await response.arrayBuffer()
    return bytes
}

/**
 * @param {string} filename
 * @param {string} watData
 * @returns {ArrayBuffer}
 */
export function build(filename, watData) {
    console.log("building")
    const wasmModule = wabt.parseWat(filename, watData)
    wasmModule.resolveNames()

    const { buffer: wasmBinary } = wasmModule.toBinary({})
    return wasmBinary
}

/**
 * @param { string } watFileName
 * @returns { Promise<ArrayBuffer> }
 */
export async function fetchAndBuild(watFileName) {
    const fetchAndBuildPromise = new Promise((res, rej) => {
        fetchWat(watFileName)
            .then(watData => {
                const wasmBinary = build(watFileName, watData)
                res(wasmBinary)
            })
            .catch(err => rej(err))
    })
    return fetchAndBuildPromise
}
