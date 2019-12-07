(module
  (import "js" "memory" (memory 1))

  (func (export "ifStatementTest") (result i32)
    i32.const 99
  )

  (func (export "swapAtIndex") (param $x i32) (param $y i32) (local $x_temp i32)
    ;; move x to temp
    local.get $x
    i32.load8_u
    local.set $x_temp

    ;; move y to x
    local.get $x
    local.get $y
    i32.load8_u
    i32.store8

    ;; move temp to y
    local.get $y
    local.get $x_temp
    i32.store8
  )

  (func (export "swapInPlace") (param $x i32) (param $y i32)
    ;; x = x ^ y
    local.get $x
    local.get $x
    i32.load8_u
    local.get $y
    i32.load8_u
    i32.xor
    i32.store8

    ;; y = x ^ y
    local.get $y
    local.get $x
    i32.load8_u
    local.get $y
    i32.load8_u
    i32.xor
    i32.store8

    ;; x = x ^ y
    local.get $x
    local.get $x
    i32.load8_u
    local.get $y
    i32.load8_u
    i32.xor
    i32.store8
  )
)