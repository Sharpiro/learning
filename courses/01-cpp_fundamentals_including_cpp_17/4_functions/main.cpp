#include <iostream>
#include "person.h"

int main()
{
    auto x = 2;
    Person person = Person();
    int number = person.GetNumber();
    std::cout << number << std::endl;

    int complexNumber = person.GetComplexNumber();
    std::cout << complexNumber << std::endl;
}