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

void test_push()
{
  auto root_cell = get_test_list(3, 1);
  auto old_last_node = root_cell->last();
  auto new_node = root_cell->push(4);
  auto last_node = root_cell->last();

  assert_eq(last_node->value, new_node->value);
  assert_eq(last_node, new_node);
  assert_eq(new_node->value, 4);
}

void test_pop()
{
  auto root_cell = get_test_list(1, 1);
  auto popped = root_cell->pop();

  assert_eq(root_cell->value, 1);
  assert_eq(root_cell->next->value, 2);
  assert_eq(popped->value, 3);
}

void test_reverse()
{
  auto root = get_test_list(5, 1);
  print_node(root);
  puts("");

  root = root->reverse();
  // root->reverse();
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
  // test_to_vec();
  // test_find();
  // test_push();
  test_pop();
  // test_reverse();
}
