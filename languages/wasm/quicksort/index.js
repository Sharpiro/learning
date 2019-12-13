const watFileName = "quicksort.wat"

fetchWat(watFileName).then(wasmText => {
  const wabt = WabtModule();
  const memory = new WebAssembly.Memory({ initial: 1 });
  const memoryView = new Uint8Array(memory.buffer)
  memoryView[0] = 11
  memoryView[1] = 22
  memoryView[2] = 33
  memoryView[3] = 44
  memoryView[4] = 55
  const importObject = { js: { memory: memory } }
  const wasmModule = wabt.parseWat(watFileName, wasmText);
  const { buffer: wasmBinary } = wasmModule.toBinary({});

  WebAssembly.instantiate(wasmBinary, importObject).then(instantiatedSource => {
    /** @type {import("./types").WasmFunctions} */
    const wasmFunctions = instantiatedSource.instance.exports
    // console.log(new Uint8Array(memory.buffer, 0, 5))
    // wasmFunctions.swapAtIndex(1, 3)
    // wasmFunctions.swapInPlace(0, 4)
    console.log(wasmFunctions.isLessThan(1, 2))

    // console.log(new Uint8Array(memory.buffer, 0, 5))
  })
})

/**
 * @param {string} fileName
 */
async function fetchWat(fileName) {
  if (fileName.slice(-4) !== ".wat") {
    throw new Error("invalid file type")
  }
  const response = await fetch(fileName);
  const bytes = await response.text();
  return bytes
}