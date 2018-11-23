#include <iostream>
#include <string>

int main()
{
    // std::cout << "enter your name:" << std::endl;
    // std::string name;
    // std::cin >> name;
    // std::cout << "hello " << name << std::endl;
    // auto temp = 1;
    // std::cout << 1'000'000'000 << std::endl;
    // std::cout << 0xff << std::endl;
    // std::cout << 0b1101 << std::endl;

    int x = 5;
    int y = 11;

    std::cout << x << std::endl;
    std::cout << y << std::endl;
    
    x = x ^ y;
    y = x ^ y;
    x = x ^ y;

    std::cout << x << std::endl;
    std::cout << y << std::endl;
    // int x = 12;
    // long long y = 13;
    // std::cout << "int size: " << sizeof(x) << std::endl;
    // std::cout << "long long size: " << sizeof(y) << std::endl;
}
