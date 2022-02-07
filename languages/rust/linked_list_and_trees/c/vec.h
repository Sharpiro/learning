#pragma once

#include "./types.h"

// template <typename T>
struct Vec
{
  u8 *raw;
  // T *raw;
  u8 len;
  u8 capacity;
  int temp();
  static int temp_static();
};

// template <typename T>
Vec vec_new();

// template <typename T>
void vec_push(Vec *vec, u8 val);

// template <typename T>
void print_vec(Vec *vec);
