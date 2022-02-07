#include "node.h"
#include <stdlib.h>
#include <stdio.h>

Node get_test_list(int size, int current)
{
  Node node = {.value = current};
  if (current < size)
  {
    Node *next_heap = malloc(sizeof(Node));
    *next_heap = get_test_list(size, current + 1);
    node.next = next_heap;
  }
  return node;
}

void print_node(Node *node)
{
  printf("node value: %d\n", node->value);
  if (node->next != NULL)
  {
    print_node(node->next);
  }
}

Vec to_vec(Node *node)
{
  Vec vec = vec_new();
  vec_push(&vec, node->value);
  if (node->next != NULL)
  {
    Vec sub_vec = to_vec(node->next);
    vec_append(&vec, &sub_vec);
  }
  return vec;
}
