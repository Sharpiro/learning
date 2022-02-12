#include <stdio.h>
#include <iostream>

struct MyStruct
{
  int x;
  int y;
};

void mod_obj_pointer(MyStruct *my_struct)
{
  *my_struct = MyStruct{.x = 3, .y = 4};
  printf("data addr: %p, ptr addr: %p, x: %d, y: %d\n", my_struct, &my_struct, my_struct->x, my_struct->y);
}

void mod_obj_ref(MyStruct &my_struct)
{
  my_struct = MyStruct{.x = 3, .y = 4};
  printf("data addr: %p, ref addr: %p, x: %d, y: %d\n", &my_struct, &my_struct, my_struct.x, my_struct.y);
}

void mod_num_pointer(int *num)
{
  *num = 2;
  printf("data addr: %p, ptr addr: %p, x: %d\n", num, &num, *num);
}

void mod_num_ref(int &num)
{
  num = 2;
  printf("data addr: %p, ptr addr: %p, val: %d\n", &num, &num, num);
}

int main()
{
  puts("---mod_pointer struct---");
  auto my_struct = MyStruct{.x = 1, .y = 2};
  auto *my_ptr = &my_struct;
  printf("data addr: %p, ptr addr: %p, x: %d, y: %d\n", &my_struct, &my_ptr, my_ptr->x, my_ptr->y);
  mod_obj_pointer(my_ptr);
  printf("data addr: %p, ptr addr: %p, x: %d, y: %d\n", &my_struct, &my_ptr, my_ptr->x, my_ptr->y);

  puts("\n---mod_ref struct---");
  auto my_struct_2 = MyStruct{.x = 1, .y = 2};
  auto &my_ref = my_struct_2;
  printf("data addr: %p, ref addr: %p, x: %d, y: %d\n", &my_struct_2, &my_ref, my_ref.x, my_ref.y);
  mod_obj_ref(my_ref);
  printf("data addr: %p, ref addr: %p, x: %d, y: %d\n", &my_struct_2, &my_ref, my_ref.x, my_ref.y);

  puts("\n---mod_pointer number---");
  int x = 1;
  int *x_ptr = &x;
  printf("data addr: %p, ptr addr: %p, val: %d\n", &x, &x_ptr, x);
  mod_num_pointer(&x);
  printf("data addr: %p, ptr addr: %p, val: %d\n", &x, &x_ptr, x);

  puts("\n---mod_ref number---");
  int y = 1;
  int &y_ref = y;
  printf("data addr: %p, ptr addr: %p, val: %d\n", &y, &y_ref, y);
  mod_num_ref(y_ref);
  printf("data addr: %p, ptr addr: %p, val: %d\n", &y, &y_ref, y);
}
