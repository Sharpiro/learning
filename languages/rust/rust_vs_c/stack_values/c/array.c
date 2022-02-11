#include <stdio.h>

typedef unsigned char u8;

void print_arr(u8 arr[], int len)
{
  putchar('[');
  for (int i = 0; i < len; i++)
  {
    printf("%d,", arr[i]);
  }
  puts("]");
}

// void mod_by_ptr(u8 arr[])
void mod_by_ptr(u8 *arr)
{
  arr[0] = 99;
}

typedef struct
{
  u8 arr[3];
} ArrayContainer;

void mod_by_copy(ArrayContainer arr_container)
{
  arr_container.arr[0] = 99;
}

int main()
{
  puts("---mod_by_ptr---");
  u8 arr[] = {1, 2, 3};
  print_arr(arr, 3);
  mod_by_ptr(arr);
  print_arr(arr, 3);

  puts("---mod_by_copy---");
  ArrayContainer arr_container = {.arr = {1, 2, 3}};
  print_arr(arr_container.arr, 3);
  mod_by_copy(arr_container);
  print_arr(arr_container.arr, 3);
}
