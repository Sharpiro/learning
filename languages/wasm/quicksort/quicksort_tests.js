import { test, assertTrue, assertFalse, assertEqual } from "./test_lib.js"

// test("isLessThan", funcs => {
//   assertTrue(funcs.isLessThan(0, 1))
// })
// test("isLessThan", funcs => {
//   assertFalse(funcs.isLessThan(1, 1))
// })
// test("isLessThan", funcs => {
//   assertFalse(funcs.isLessThan(2, 1))
// })
// test("isLessThan", funcs => {
//   assertFalse(funcs.isLessThan(2, 0))
// })

// test("forLoop", (funcs, memory) => {
//   const memoryView = new Uint8Array(memory.buffer)
//   funcs.forLoop(0, 5)
//   assertTrue(memoryView[0] === 0)
//   assertTrue(memoryView[1] === 1)
//   assertTrue(memoryView[2] === 2)
//   assertTrue(memoryView[3] === 3)
//   assertTrue(memoryView[4] === 4)
// })

// test("reverseTest", (funcs, memory) => {
//   const memoryView = new Uint8Array(memory.buffer)
//   const initial = [1, 2, 3, 4, 5, 6, 7]
//   memoryView.set(initial)

//   funcs.reverse(0, initial.length)

//   assertEqual(initial.reverse(), memoryView.slice(0, initial.length))
// })

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

test("partitionOnceTest", (funcs, memory) => {
  const memoryView = new Uint8Array(memory.buffer)
  const initial = [3, 1, 4, 2]
  const expected = [1, 2, 4, 3]
  memoryView.set(initial)

  const nextPivot = funcs.partition(0, initial.length)

  const actual = memoryView.slice(0, initial.length)
  assertTrue(1 === nextPivot)
  assertEqual(expected, actual)
})

test("quicksortTest", (funcs, memory) => {
  const memoryView = new Uint8Array(memory.buffer)
  const expected = [1, 2, 3, 4]
  const initial = [3, 1, 4, 2]
  memoryView.set(initial)

funcs.quicksort(0, initial.length)

  const actual = memoryView.slice(0, initial.length)
  console.log(actual)
  assertEqual(expected, actual)
})
