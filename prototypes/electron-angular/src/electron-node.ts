export const require = (<any>globalThis).require as NodeRequireFunction
export const fs = require("fs") as typeof import('fs')
export const { remote } = require("electron")
export const path = require("path") as typeof import('path')
