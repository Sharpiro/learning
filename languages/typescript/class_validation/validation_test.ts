import { validateDefined } from "./validation"

class ImmediateErrorExample {
  name!: string

  constructor(init: any) {
    Object.assign(this, init)
    for (const i of validateDefined(this)) {
      throw new Error("validation error")
    }
  }
}

class Example {
  height = 1
  name!: string
  age!: number
  temp?: boolean

  constructor(init: Omit<Example, "doSomething" | "value" | "height">) {
    Object.assign(this, init)
  }

  public get value(): string {
    return "computed value"
  }

  doSomething() {
    console.log("something is called by func")
  }
}

(function undefinedRequiredTest() {
  const e = new Example({ name: 'Graham', age: undefined as any })
  const results = Array.from(validateDefined(e))
  assert(results.length === 1)
  assert(results[0] === "property 'age' was undefined on class 'Example'")
})();

(function allDefinedTest() {
  const e = new Example({ name: 'Graham', age: 12 })
  const results = Array.from(validateDefined(e))
  assert(results.length === 0)
})();

(function definedOptionalTest() {
  const e = new Example({ name: 'Graham', age: 12, temp: true })
  const results = Array.from(validateDefined(e))
  assert(results.length === 0)
})();

// limitation: validation fails when explicitly providing undefined to an optional
(function undefinedOptionalTest() {
  const e = new Example({ name: 'Graham', age: 12, temp: undefined })
  const results = Array.from(validateDefined(e))
  assert(results.length === 1)
})();

(function immediateErrorTest() {
  assertThrows(() => new ImmediateErrorExample({ name: 'Graham', temp: true }))
})()

function assert(condition: boolean, message?: string) {
  if (!condition) throw new Error(message ?? "assertion error")
}

function assertThrows(expression: Function) {
  try {
    expression()
    throw new Error("expression did not error")
  } catch (_) { }
}
