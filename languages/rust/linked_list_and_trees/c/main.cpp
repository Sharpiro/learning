#include <stdio.h>
// #include "./types.h"
#include "./vec.h"
// #include "test.h"
// #include <stdbool.h>
// #include <unistd.h>
// #include "node.h"
// #include "temp.h"

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

int main()
{
  // int x = divide(100, 21);
  // double y = divide(100.0, 21.0);
  // printf("%d\n", x);

  // Vec vec = {};
  Vec vec = vec_new();
  int temp = vec.temp();
  int temp_static = Vec::temp_static();
  printf("%d\n", temp);
  printf("%d\n", temp_static);
  vec_push(&vec, 1);
  vec_push(&vec, 2);
  print_vec(&vec);
  // test_to_vec();
  // Node root = get_test_list(5, 1);
  // print_list(&root);
  // puts("tests succeeded");
}
