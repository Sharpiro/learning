// todo: why does require not try and find types?
const require = (<any>globalThis).require as NodeRequireFunction
// export const fs = require("fs")
export const fs = require("fs") as typeof import('fs')
