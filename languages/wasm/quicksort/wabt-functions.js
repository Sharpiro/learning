//@ts-ignore
import { WabtModule } from "https://cdn.jsdelivr.net/gh/sharpiro/wabt.js@9ea629b1c31f80c70efdf9d01b489274beb0ddb1/index.js"

/**
 * @param {string} fileName
 */
export async function fetchWat(fileName) {
    if (fileName.slice(-4) !== ".wat") {
        throw new Error("invalid file type")
    }
    const response = await fetch(fileName)
    const text = await response.text()
    return text
}

/**
 * @param {string} fileName
 */
export async function fetchWatDeno(fileName) {
    if (fileName.slice(-4) !== ".wat") {
        throw new Error("invalid file type")
    }
    const response = await window.Deno.readFile(fileName)
    const decoder = new TextDecoder()
    const text = decoder.decode(response)
    return text
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
    const wasmModule = WabtModule().parseWat(filename, watData)
    wasmModule.resolveNames()

    const { buffer: wasmBinary } = wasmModule.toBinary({})
    return wasmBinary
}

/**
 * @param { string } watFileName
 * @returns { Promise<ArrayBuffer> }
 */
export async function fetchAndBuild(watFileName) {
    const fetchFunc = window.Deno ? fetchWatDeno : fetchWat
    const fetchAndBuildPromise = new Promise((res, rej) => {
        fetchFunc(watFileName)
            .then(watData => {
                const wasmBinary = build(watFileName, watData)
                res(wasmBinary)
            })
            .catch(err => rej(err))
    })
    return fetchAndBuildPromise
}
