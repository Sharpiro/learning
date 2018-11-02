#pragma once
#include "test.h"

class Child : public Test
{
  private:
    int _xyz;

  public:
    Child(int x, int y);
    Child();
    ~Child();
};
