#include "child.h"
#include <iostream>

Child::Child()
    // named b/c c++ allows for multiple inheritance
    : Test(),
      _xyz(88)
{
    std::cout << "constructing 'Child'" << _x << std::endl;
}

Child::Child(int x, int y)
    : Test(x),
      _xyz(y)
{
    std::cout << "constructing 'Child'" << _x << std::endl;
}

Child::~Child()
{
    std::cout << "destructing 'Child'" << _x << std::endl;
}
