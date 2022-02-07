// #include <stdio.h>
// #include <stdlib.h>
// #include "vec.h"
// #include "test.h"
// #include <string.h>

// template <typename T>
// int Vec<T>::temp()
// {
//   return len + 1;
// }

// template <typename T>
// int Vec<T>::temp_static()
// {
//   return 42 + 1;
// }

// template <typename T>
// Vec<T> vec_new()
// {
//   u8 default_capacity = 2;
//   u8 *raw = (u8 *)malloc(default_capacity);
//   Vec<T> vec = {.raw = raw, .capacity = default_capacity};
//   return vec;
// }

// template <typename T>
// void vec_push(Vec<T> *vec, u8 val)
// {
//   if (vec->len == vec->capacity)
//   {
//     int new_capacity = vec->capacity * 2;
//     u8 *new_raw = (u8 *)malloc(new_capacity);
//     memcpy(new_raw, vec->raw, vec->len);
//     free(vec->raw);
//     vec->raw = new_raw;
//     vec->capacity = new_capacity;
//     printf("expanding vec to: %d\n", new_capacity);
//   }

//   // printf("adding %d to vec\n", val);
//   int index = vec->len;
//   vec->raw[index] = val;
//   vec->len++;
// }

// template <typename T>
// void vec_append(Vec<T> *vec1, Vec<T> *vec2)
// {
//   for (int i = 0; i < vec2->len; i++)
//   {
//     vec_push(vec1, vec2->raw[i]);
//   }
// }

// template <typename T>
// void print_vec(Vec<T> *vec)
// {
//   printf("[");
//   for (int i = 0; i < vec->len; i++)
//   {
//     printf("%d,", vec->raw[i]);
//   }
//   puts("]");
// }