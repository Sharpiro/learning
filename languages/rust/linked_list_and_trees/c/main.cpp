#include <stdio.h>
#include "./types.h"
#include "./vec.h"
// #include "test.h"
// #include <stdbool.h>
// #include <unistd.h>
// #include "node.h"
// #include "temp.h"
#include <string>
#include <cstddef>
#include <concepts>
#include <iostream>

// void test_to_vec()
// {
//   Node root = get_test_list(5, 1);
//   print_node(&root);
//   Vec vec = to_vec(&root);
//   print_vec(&vec);

//   assert_eq(vec.len, 5);
//   assert_eq(vec.raw[0], 1);
//   assert_eq(vec.raw[1], 2);
//   assert_eq(vec.raw[2], 3);
//   assert_eq(vec.raw[3], 4);
//   assert_eq(vec.raw[4], 5);
// }

// template <typename T, typename U>
// concept SomeConcept = std::is_base_of<U, T>::value;
struct SimpleStruct
{
  int x;
};

struct MyStruct
{
  int x;
  double y;
  bool z;
};

void do_something(MyStruct &book)
{
  book = MyStruct{.x = 88};
  // book.x = 99;
  //
}

struct BadStruct
{
  int f;
};

template <typename T>
concept Debug = requires(T a)
{
  {
    a.x
    } -> std::convertible_to<int>;
};

template <Debug T>
void format_debug(T *my_struct)
{
  // std::cout << "hi????\n";
  std::cout << 1;
  // auto temp = my_struct->myFunc();
  const auto [x, y, z] = *my_struct;
  printf("{ x: %d, y: %f, z: %d }\n", x, y, z);
  // printf("{ x: %d, y: %f, z: %d }\n", my_struct->x, my_struct->y, my_struct->z);
}

int main()
{
  // int x = divide(100, 21);
  // double y = divide(100.0, 21.0);
  // printf("%d\n", x);

  // Vec<u8> vec = Vec<u8>::new_vec();
  // vec.push(1);
  // vec.print_debug();

  // auto vec = Vec<MyStruct>::new_vec();
  // auto my_struct = MyStruct{.x = 100, .y = 2.0, .z = true};
  auto my_struct = MyStruct{.x = 99};
  do_something(my_struct);
  // auto my_struct = BadStruct{};
  format_debug(&my_struct);
  // vec.push(my_struct);
  // print_my_struct(&my_struct);
  // vec.print_debug();
  // test_to_vec();
  // Node root = get_test_list(5, 1);
  // print_list(&root);
  // puts("tests succeeded");
}
