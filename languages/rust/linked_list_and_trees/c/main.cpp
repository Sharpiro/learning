#include <stdio.h>
#include "./types.h"
#include "./vec.h"
#include "test.h"
#include "node.h"

void print_debug(const Vec<int> *vec)
{
  printf("[");
  for (int i = 0; i < vec->len; i++)
  {
    printf("%d,", vec->raw[i]);
  }
  puts("]");
}

void test_to_vec()
{
  auto root = get_test_list(5, 1);
  auto vec = root->to_vec();

  assert_eq(vec.len, 5);
  assert_eq(vec.raw[0], 1);
  assert_eq(vec.raw[1], 2);
  assert_eq(vec.raw[2], 3);
  assert_eq(vec.raw[3], 4);
  assert_eq(vec.raw[4], 5);

  puts("test_to_vec succeeded");
}

void test_find()
{
  auto root = get_test_list(5, 1);

  assert_eq(root->find(1)->value, 1);
  assert_eq(root->find(3)->value, 3);
  assert_eq(root->find(0), nullptr);

  puts("test_find succeeded");
}

void test_reverse()
{
  auto root = get_test_list(5, 1);
  print_node(root);
  puts("");

  root = root->reverse();
  print_node(root);
  auto vec = root->to_vec();
  print_debug(&vec);
  assert_eq(vec.len, 5);
  assert_eq(vec.raw[0], 5);
  assert_eq(vec.raw[1], 4);
  assert_eq(vec.raw[2], 3);
  assert_eq(vec.raw[3], 2);
  assert_eq(vec.raw[4], 1);

  puts("test_to_vec_ref succeeded");
}

int main()
{
  test_to_vec();
  test_find();
  test_reverse();
}
