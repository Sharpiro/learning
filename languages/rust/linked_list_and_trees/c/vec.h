#pragma once

#include <stdlib.h>
#include <string.h>
// #include "./types.h"

template <typename T>
struct Vec
{
  // u8 *raw;
  T *raw;
  int len;
  int capacity;
  void push(T val);
  void append(Vec<T> *other_vec);
  void print_debug();
  static Vec<T> new_vec();
};

template <typename T>
void Vec<T>::push(T val)
{
  if (this->len == this->capacity)
  {
    int new_capacity = this->capacity * 2;
    T *new_raw = (T *)malloc(new_capacity);
    memcpy(new_raw, this->raw, this->len);
    free(this->raw);
    this->raw = new_raw;
    this->capacity = new_capacity;
    // printf("expanding vec to: %d\n", new_capacity);
  }

  // printf("adding %d to vec\n", val);
  int index = this->len;
  this->raw[index] = val;
  this->len++;
}

template <typename T>
Vec<T> Vec<T>::new_vec()
{
  int default_capacity = 2;
  T *raw = (T *)malloc(default_capacity);
  Vec<T> vec = {.raw = raw, .capacity = default_capacity};
  return vec;
}

template <typename T>
void Vec<T>::append(Vec<T> *other_vec)
{
  for (int i = 0; i < other_vec->len; i++)
  {
    this->push(other_vec->raw[i]);
  }
}

template <typename T>
void Vec<T>::print_debug()
{
  printf("[");
  for (int i = 0; i < this->len; i++)
  {
    printf("%d,", this->raw[i]);
  }
  puts("]");
}
