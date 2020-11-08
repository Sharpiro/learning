/** @type {HTMLCanvasElement} */
const canvasElement = document.getElementById("temp");
const canvasContext = canvasElement.getContext("2d");
const canvasImageData = canvasContext.createImageData(
  canvasElement.width,
  canvasElement.height
);

(async function () {
  const wasm = new Uint8Array(await (await fetch("simple.wasm")).arrayBuffer());
  const wasmModule = await WebAssembly.compile(wasm);
  wasmInstance = await WebAssembly.instantiate(wasmModule, importObject);
  // console.log(new Uint8Array(importObject.js.memory.buffer)[0]);
  // wasmInstance.exports.updateBuffer();
  // console.log(new Uint8Array(importObject.js.memory.buffer)[0]);
})();

/** @type {WebAssembly.Instance} */
let wasmInstance;
const importObject = {
  js: {
    memory: new WebAssembly.Memory({ initial: 16, maximum: 16 })
  }
};
const canvasBuffer = canvasImageData.data;
const wasmBuffer = new Uint8Array(importObject.js.memory.buffer);
console.log(importObject.js.memory.buffer);
let iVal = 0;
updateBufferJS(iVal);
canvasContext.putImageData(canvasImageData, 0, 0);

window.update = () => {
  console.log("updating...");
  wasmInstance.exports.updateBuffer();
  // const colorDiff = new Uint8Array(importObject.js.memory.buffer)[0];
  const colorDiff = wasmBuffer[0];
  console.log(colorDiff);
  iVal += colorDiff;
  updateBufferJS(iVal);
  console.log(canvasBuffer);
  canvasContext.putImageData(canvasImageData, 0, 0);
};

function updateBufferJS(iVal) {
  for (let i = 0; i < canvasBuffer.length; i += 4) {
    canvasBuffer[i] = iVal;
    canvasBuffer[i + 1] = iVal;
    canvasBuffer[i + 2] = iVal;
    canvasBuffer[i + 3] = 255;
  }
}