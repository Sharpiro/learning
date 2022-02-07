#include <stdio.h>
#include <iostream>

struct MyStruct
{
  int x;
  int y;
};

// void mod_member(MyStruct &my_struct)
// {
//   my_struct.x = 2;
// }

void mod_obj(MyStruct &my_struct)
{
  my_struct = MyStruct{.x = 3, 4};
  printf("addr: %p, x: %d, y: %d\n", &my_struct, my_struct.x, my_struct.y);
  // printf("addr: %p, x: %d, y: %d\n", my_struct.x, my_struct.y);
}

void mod_obj_pointer(MyStruct *my_struct)
{
  *my_struct = MyStruct{.x = 3, .y = 4};
  printf("addr: %p, x: %d, y: %d\n", &my_struct, my_struct->x, my_struct->y);
}

int main()
{
  auto my_struct = MyStruct{.x = 1, .y = 2};
  // printf("x: %d, y: %d\n", my_struct.x, my_struct.y);
  printf("addr: %p, x: %d, y: %d\n", &my_struct, my_struct.x, my_struct.y);
  // std::cout << &my_struct << std::endl;
  mod_obj(my_struct);
  // std::cout << my_struct.x << std::endl;
  printf("addr: %p, x: %d, y: %d\n", &my_struct, my_struct.x, my_struct.y);

  // puts("hi cpp");
  // std::cout << "hi cpp iostream\n";
}
