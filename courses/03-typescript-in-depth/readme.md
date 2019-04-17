# Typescript In-Depth

## Compiler

### Running Code

* `node`
  * `node dist/main.js`
    * will run javascript but not reference back to typescript source files
    * requires `tsc -w` in background
* `ts-node`
  * `ts-node dist/main.js`
    * will run javascript and reference back to typescript source files w/ source-mappings
    * skips typescript compilation (can be useful for testing)
    * requires `tsc -w` in background
    * my favorite
  * `ts-node main.ts`
    * will run typescript, but will fail on compile time errors (tsc) and link to typescript source files

## Tuples

```ts
let array: (number | string)[] = [123, "hello"]
let arrayZero: number | string = array[0]
let tuple: [number, string] = [123, "hello"]
let tupleZero: number = tuple[0]
```

## Type Narrowing

```ts
let data: (number | string)[] = [1, 2, 3, "4"]
for (const item of data) {
    if (typeof item === "number") {
        const number: number = item
        console.log(`${number}- number`);
    } else {
        const string: string = item
        console.log(`${string}- string`);
    }
}
```

## Arrow Functions

Arrow functions capture `this` at function creation, rather than invocation

```ts
class Test {
    brokeData = false
    noArrowData = false
    arrowData = false

    brokeNoArrow() {
        this.brokeData = true
        setTimeout(function () {
            console.log(this.brokeData); // undefined
        }, 1000)
    }

    noArrow() {
        let self = this
        self.noArrowData = true
        setTimeout(function () {
            console.log(self.noArrowData); // true
        }, 2000)
    }

    arrow() {
        this.arrowData = true
        setTimeout(() => {
            console.log(this.arrowData); // true
        }, 3000)
    }
}
```

## Rest and Spread (`...`)

Depending on the context, "`...`" can be considered a rest or spread operator.  If it is used as a parameter, then it is a "Rest Parameter", if it is used elsewhere, it is a "Spread Operator"

### Rest Parameters

Collects elements into an array

```ts
function restFunc(num: number, ...temp: number[]): void {
    console.log(num); // 9
    console.log(temp); // [1, 2, 3]
}
restFunc(9, 1, 2, 3)
restFunc(9, ...[1, 2, 3]) // spread operator used to input to rest parameter
```

### Spread Operator

Expands iterables (arrays, objects, strings) into arguments or elements

```ts
const data1 = [1, 2, 3]
const data2 = [...data1, 4, 5, 6]
const [data3, ...data4] = data2

let tuple: [number, string, boolean] = [123, "hello", true]
const [tuple1, ...tuple2] = tuple
```

## Function Overloads

```ts
function getName(id: string): string
function getName(id: number): string
function getName(id: any): string {
    if (typeof id === "string") {
        return "name1"
    } else if (typeof id === "number") {
        return "name2"
    } else {
        throw new Error("invalid type")
    }
}

console.log(getName("99"))
console.log(getName(12))
console.log(getName(true)) // compile error
```

## Function Type Interfaces

```ts
interface FunctionInterface {
  (a: number, b: string): string
}

interface NormalInterface {
  temp: FunctionInterface
  temp2(a: boolean): boolean
}

const y: FunctionInterface = (x, y) => x + y
const x: NormalInterface = { temp: y, temp2: x => !x }
const z: (a: number, b: string) => string = y
```

## Class Expressions

```ts
abstract class Item { abstract doNothing(): void }
const temp = class extends Item { doNothing = () => console.log }
new temp().doNothing()
```

## Modules and Namespaces

In typescript 1.5 `internal modules` became `namespaces`, and `external modules` became just `modules`.  ES2015 modules were adopted by typescript and makes modules between typescript and javascript more similar.

| Modules                               | bNamespaces                         |   |   |   |
|---------------------------------------|-------------------------------------|---|---|---|
| tool for organizing code              | tool for organizing code            |   |   |   |
| native support in node                | no special loader required          |   |   |   |
| browsers supported with module loader | prevents global namespace pollution |   |   |   |
| supports es2015 module syntax         | work natively with the browser      |   |   |   |

### Module code generation types

* commonjs
  * used by nodejs
* amd (asynchronous module definition)
  * used for browsers w/ requirejs module loader
* umd (universal module definition)
  * combined commonjs and amd formats
  * can be used by node and loaders that support amd
* system
  * supports commonjs, amd, and has its own custom format
* es2015
  * new hotness, used in typescript source

### es2015 modules in browser

All referenced modules must be servable and available via their `.js` extension, despite it being a typescript file.  [open issue](https://github.com/Microsoft/TypeScript/issues/16577)

`main.ts`

```ts
import { doNothing } from "./other.js";
```

`index.html`

```html
<script type="module" src="dist/main.js"></script>
```

### Syntax

```ts
import * as test from "./test"
import { Test } from "./test"
```

### Module Usage

#### Standard

```ts
// other.ts
export class Test { }

// main.ts
import { Test } from "./other"
new Test()
```

#### Aliases

```ts
// other.ts
class Test { }
export { Test as AliasOutTest }

// main.ts
import { AliasOutTest as AliasInTest } from "./other.js";
new AliasInTest()
```

#### Default Exports

```ts
// other.ts
export default function defaultFunc() { }

// main.ts
import defaultFuncAlias from "./other.js";
defaultFuncAlias()
```

#### Behavior Import (not recommended)

Will import nothing, but will cause script to run possibly changing environmental behavior for other modules.  (not recommended)

```ts
import "./other.js";
```
