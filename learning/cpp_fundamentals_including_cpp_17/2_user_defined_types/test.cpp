#include "test.h"
#include <iostream>

Test::Test()
    // this format avoid double initialization of member variables
    : _x(0)
{
    std::cout << "constructing 'Test'" << _x << std::endl;
}

Test::Test(int x)
    // this format avoid double initialization of member variables
    : _x(x)
{
    std::cout << "constructing 'Test'" << _x << std::endl;
}

Test::~Test()
{
    std::cout << "destructing 'Test'" << _x << std::endl;
}

void Test::DoNothing()
{
    std::cout << "doing nothing" << std::endl;
    std::cout << _x << std::endl;
}
