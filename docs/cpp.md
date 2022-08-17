# CPP

## Lambdas

```cpp
  auto x = 9;
  // x must be explicitly captured
  auto add_val = [x](int value)
  {
    return x + value;
  };
  assert(add_val(1) == 10);
```

## Closures

### Capture by value

This captures by value, and copies the data.
This is less intuitive compared to Javascript which auto-captures the pointer behind the scenes.

```cpp
  int x = 0;
  auto adder = [x](int y)
  {
    return x + y;
  };
  x = 5;
  assert(adder(1) == 1);
```

### Capture by reference

Capturing by reference is more intuitive and similar to Javascript.

```cpp
  int x = 0;
  auto adder = [&x](int y)
  {
    return x + y;
  };
  x = 5;
  assert(adder(1) == 6);
```
