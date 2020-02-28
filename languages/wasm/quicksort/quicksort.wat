(module
  (import "js" "memory" (memory 1))
  (import "js" "log" (func $log (param i32)))

  (func (export "isLessThan") (param i32) (param i32) (result i32)
    local.get 0
    local.get 1
    i32.lt_s
    if (result i32)
      i32.const 1
    else
      i32.const 0
    end
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

  (func (export "forLoop") (param $start i32) (param $end i32)
      ;; loop is basically a do while
      loop $myLoop
        ;; do work
        local.get $start
        local.get $start
        i32.store

        ;; increment
        local.get $start
        i32.const 1
        i32.add
        local.set $start

        ;; compare is less than
        local.get $start
        local.get $end
        i32.lt_s
  
        br_if $myLoop
      end
  )

  (func (export "reverse") (param $start i32) (param $end i32)  (local $endIndex i32)
      local.get $end
      i32.const 1
      i32.sub
      local.set $endIndex
    loop $myLoop
      ;; calc store index
      local.get $end
      local.get $start
      i32.add

      ;; calc j
      local.get $endIndex
      local.get $start
      i32.sub

      ;; load data @ j
      i32.load8_u

      ;; store data @ new index
      i32.store8

      ;; call $log
      
      ;; increment
      local.get $start
      i32.const 1
      i32.add
      local.set $start

      ;; compare is less than
      local.get $start
      local.get $end
      i32.lt_s

      br_if $myLoop
    end
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
