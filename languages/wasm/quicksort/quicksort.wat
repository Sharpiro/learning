(module
  (import "js" "memory" (memory 1))
  (import "js" "log" (func $log (param i32)))

  (func $quicksort (export "quicksort") (param $start i32) (param $end i32) (local $pivotIndex i32)
    local.get $start
    local.get $end
    i32.const 1
    i32.sub
    i32.lt_s
    if
      local.get $start
      local.get $end
      call $partition
      local.set $pivotIndex

      local.get $start
      local.get $pivotIndex
      call $quicksort

      local.get $pivotIndex
      i32.const 1
      i32.add
      local.get $end
      call $quicksort
    end
  )

  (func $partition (export "partition") (param $start i32) (param $end i32) (result i32) (local $swapIndex i32) (local $highIndex i32)
    local.get $start
    local.set $swapIndex

    local.get $end
    i32.const 1
    i32.sub
    local.set $highIndex

    loop $partitionLoop
      local.get $start
      i32.load8_s

      local.get $highIndex
      i32.load8_s

      i32.lt_s
      ;; todo: try returning swap index?
      if
        local.get $start
        local.get $swapIndex
        call $swapAtIndex
        local.get $swapIndex
        i32.const 1
        i32.add
        local.set $swapIndex
      end

      ;; todo abstract away looping?
      ;; increment
      local.get $start
      i32.const 1
      i32.add
      local.set $start

      ;; compare is less than
      local.get $start
      local.get $highIndex
      i32.lt_u
      br_if $partitionLoop
    end

    local.get $swapIndex
    local.get $highIndex
    call $swapAtIndex

    local.get $swapIndex
  )

  (func $swapAtIndex (export "swapAtIndex") (param $x i32) (param $y i32)
    ;; prepare y value to be stored at x index
    local.get $x
    local.get $y
    i32.load8_u

    ;; prepare x value to be stored at y index
    local.get $y
    local.get $x
    i32.load8_u

    ;; store x value at y index
    i32.store8
    ;; store y value at x index
    i32.store8
  )
)
