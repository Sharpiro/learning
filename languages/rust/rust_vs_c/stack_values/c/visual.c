#include <stdio.h>

typedef unsigned char u8;

void print_arr(u8 arr[], int len)
{
  for (int i = 0; i < len; i++)
  {
    printf("%d\n", arr[i]);
  }
}

// int main(int arg_len, char *args[])
int main(int arg_len, char **args)
{
  printf("args: %d\n", arg_len - 1);
  for (int i = 1; i < arg_len; i++)
  {
    // print_arr(args[i]);
    printf("%s\n", args[i]);
  }

  // u8 arr[3] = {1, 2, 3};
  // u8 **a_ptr_2 = &arr;
  // u8(*temp)[3] = &arr;

  // print_arr(arr, 3);
  // print_arr(*a_ptr_2, 3);
  // print_arr(*temp, 3);
}
