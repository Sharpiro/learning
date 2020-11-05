(module
  (import "js" "memory" (memory 1))

  (func (export "updateBuffer")   
    i32.const 0
    i32.const 25
    i32.store8 
  )
)