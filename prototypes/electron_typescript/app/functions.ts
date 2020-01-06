// const { remote, } = require("electron")
const fs = require("fs")

export function helloFunctions(x?: number) {
  // const appPath = remote.app.getAppPath()
  console.log("hello functions")
  console.log(fs)
  console.log(x)
}
