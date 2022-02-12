#include "node.h"
#include <stdlib.h>
#include <stdio.h>

Node *get_test_list(int size, int current)
{
  auto node = new Node{.value = current};
  if (current < size)
  {
    node->next = get_test_list(size, current + 1);
  }
  return node;
}

void print_node(const Node *node)
{
  printf("%p: { value: %d, next: %p }\n", &node, node->value, node->next);
  if (node->next != NULL)
  {
    print_node(node->next);
  }
}

Vec<int> Node::to_vec() const
{
  auto vec = Vec<int>::new_vec();
  vec.push(this->value);
  if (this->next != NULL)
  {
    Vec<int> sub_vec = this->next->to_vec();
    vec.append(&sub_vec);
  }
  return vec;
}

Node *Node::find(int value)
{
  if (this->value == value)
  {
    return this;
  }

  if (this->next != nullptr)
  {
    return this->next->find(value);
  }

  return NULL;
}

Node *Node::last()
{
  if (this->next == nullptr)
  {
    return this;
  }
  else
  {
    return this->next->last();
  }
}

Node *Node::reverse()
{
  auto vec = this->to_vec_ref();
  vec.raw[0]->next = nullptr;
  for (int i = vec.len - 1; i >= 0; i--)
  {
    auto node = vec.raw[i];
    auto prev_index = i - 1;
    if (prev_index >= 0)
    {
      node->next = vec.raw[prev_index];
    }
  }
  return vec.raw[vec.len - 1];
}

Vec<Node *> Node::to_vec_ref()
{
  auto vec = Vec<Node *>::new_vec();
  vec.push(this);

  if (this->next != nullptr)
  {
    auto sub_vec = this->next->to_vec_ref();
    vec.append(&sub_vec);
  }

  return vec;
}
