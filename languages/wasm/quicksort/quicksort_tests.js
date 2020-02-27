import { test, assertTrue, assertFalse } from "./test_lib.js"

test("isLessThan", funcs => {
  assertTrue(funcs.isLessThan(0, 1))
})
test("isLessThan", funcs => {
  assertFalse(funcs.isLessThan(1, 1))
})
test("isLessThan", funcs => {
  assertFalse(funcs.isLessThan(2, 1))
})
test("isLessThan", funcs => {
  assertFalse(funcs.isLessThan(2, 0))
})

test("swapAtIndex", (funcs, memory) => {
  const memoryView = new Uint8Array(memory.buffer)
  memoryView[0] = 1
  memoryView[1] = 2
  funcs.swapAtIndex(0, 1)
  assertTrue(memoryView[0] === 2)
  assertTrue(memoryView[1] === 1)
})

test("swapAtIndex", (funcs, memory) => {
  const memoryView = new Uint8Array(memory.buffer)
  memoryView[0] = 124
  memoryView[1] = 86
  funcs.swapAtIndex(0, 1)
  assertTrue(memoryView[0] === 86)
  assertTrue(memoryView[1] === 124)
})

test("swapAtIndex", (funcs, memory) => {
  const memoryView = new Uint8Array(memory.buffer)
  memoryView[0] = 255
  memoryView[1] = 5
  funcs.swapAtIndex(0, 1)
  assertTrue(memoryView[0] === 5)
  assertTrue(memoryView[1] === 255)
})

test("loopTest", (funcs, _) => {
  funcs.loopTest(0, 5)
  console.log("done")
})

