#pragma once

#include <stdio.h>
#include <stdlib.h>
#include "./vec.h"

void panic(const char *message);

void assert_eq(int a, int b);

void assert_eq(const void *a, const void *b);

void assert_eq(const Vec<int> *a, const Vec<int> *b);

// #define ASSERT_ARR_EQ(a, b)         \
//   {                                 \
//     if (a.len != b.len)             \
//     {                               \
//       panic("not equal");           \
//     }                               \
//                                     \
//     for (int i = 0; i < a.len; i++) \
//     {                               \
//       if (a.raw[i] != b.raw[i])     \
//       {                             \
//         panic("not equal");         \
//       }                             \
//     }                               \
//   }
