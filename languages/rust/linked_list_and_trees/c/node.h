#pragma once

#include "types.h"
#include "vec.h"

typedef struct Node
{
  u8 value;
  struct Node *next;
} Node;

Node get_test_list(int size, int current);

Vec to_vec(Node *node);

void print_node(Node *node);
