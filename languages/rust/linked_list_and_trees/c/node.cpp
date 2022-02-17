#include "node.h"
#include <stdlib.h>
#include <stdio.h>
#include "test.h"

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
  printf("%p: { value: %d, next: %p }\n", node, node->value, node->next);
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

Node *Node::push(int value)
{
  auto last = this->last();
  auto new_node = new Node{value};
  last->next = new_node;
  return new_node;
}

Node *Node::pop()
{
  auto first_child = this->next;
  if (first_child == nullptr)
  {
    return nullptr;
  }

  if (first_child->next == nullptr)
  {
    this->next = nullptr;
    return first_child;
  }

  return this->next->pop();
}

Node *Node::push_start(int value)
{
  auto new_node = (Node *)malloc(sizeof(Node));
  *new_node = {.value = value, .next = this};

  return new_node;
}

Node *Node::pop_start()
{
  return this->next;
}

Node *Node::reverse()
{
  auto vec = this->to_vec_ref();
  this->next = nullptr;
  for (int i = vec.len - 1; i >= 1; i--)
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
