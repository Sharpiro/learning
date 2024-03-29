#pragma once

#include <stdlib.h>
#include <string.h>
#include <stdio.h>

template <typename T>
struct Vec
{
  T *raw;
  int len;
  int capacity;
  void push(T val);
  void append(const Vec<T> *other_vec);
  static Vec<T> new_vec();
  static Vec<T> from(int size, int *init);

  ~Vec()
  {
    free(this->raw);
    printf("freed vec of size: %d\n", this->len);
  }
};

template <typename T>
void Vec<T>::push(T val)
{
  if (this->len == this->capacity)
  {
    int new_capacity = this->capacity * 2;
    T *new_raw = (T *)malloc(new_capacity * sizeof(T));
    memcpy(new_raw, this->raw, this->len * sizeof(T));
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
  T *raw = (T *)malloc(default_capacity * sizeof(T));
  Vec<T> vec = {.raw = raw, .capacity = default_capacity};
  return vec;
}

template <typename T>
Vec<T> Vec<T>::from(int size, int *init)
{
  // todo: what happens if array param is stack pointer
  // it blows up, need to malloc
  T *raw = (T *)malloc(size * sizeof(T));
  memcpy(raw, init, size * sizeof(T));
  Vec<T> vec = {
      .raw = raw,
      .len = size,
      .capacity = size,
  };
  return vec;
}

template <typename T>
void Vec<T>::append(const Vec<T> *other_vec)
{
  for (int i = 0; i < other_vec->len; i++)
  {
    auto value = other_vec->raw[i];
    this->push(value);
  }
}

#define VEC(SIZE, VAR_NAME, ...)          \
  int ARRAY_##VAR_NAME[] = {__VA_ARGS__}; \
  Vec<int> VAR_NAME = Vec<int>::from(SIZE, ARRAY_##VAR_NAME);
