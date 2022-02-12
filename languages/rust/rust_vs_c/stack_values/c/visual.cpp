#include <stdio.h>

typedef unsigned short u16;

void print_arr(u16 *arr, int len)
{
  for (int i = 0; i < len; i++)
  {
    printf("%d\n", arr[i]);
  }
}

int main()
{
  u16 arr_temp[3] = {1, 2, 3};
  u16 *arr = arr_temp;
  print_arr(arr, 3);
  auto a_ptr = &arr;
  auto a_ptr_ptr = &a_ptr;

  auto first_deref = *a_ptr_ptr;
  auto second_deref = *first_deref;
  auto third_deref = *second_deref;
  third_deref = 99;

  print_arr(arr, 3);
}
