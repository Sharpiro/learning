function TestClassDecorator<T extends { new(...args: any[]): {} }>(constructor: T) {
  return class extends constructor {
    weight = 99

    constructor(...items: any[]) {
      super(...items)
    }
  }
}

@TestClassDecorator
class Example {
  height = 1
}

const e = new Example()
console.log(e)
console.log(e instanceof Example)
console.log((e as any).weight)
