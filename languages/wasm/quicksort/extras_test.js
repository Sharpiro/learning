// import { test, assertTrue, assertFalse, assertEqual } from "./test_lib.js"

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
