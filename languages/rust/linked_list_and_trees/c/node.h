#pragma once

#include "types.h"
#include "vec.h"

struct Node
{
  int value;
  struct Node *next;
  Vec<int> to_vec() const;
  Node *find(int value);
  Node *last();
  Vec<Node *> to_vec_ref();
  Node *reverse();
};

Node *get_test_list(int size, int current);

void print_node(Node const &node);
