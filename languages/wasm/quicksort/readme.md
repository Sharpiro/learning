# wasm quicksort

## general

* when multiple values are popped of the stack, they are provided in order to the function
  * stack converts items from lowest to highest -> left to right
  * example
    * stack
      * 0: 1
      * 1: 2
    * call func (x, y)
      * implicitly pops 2 values off and converts from low to high on stack to left to right in parameters
      * (x: 1, y: 2)

## instructions

### less than signed

```sh
i32.const 10 # lhs
i32.const 5 # rhs
i32.lt_s
```

### store

```sh
i32.const 0 # index
i32.const 10 # value
i32.store # @ index, store value
```
