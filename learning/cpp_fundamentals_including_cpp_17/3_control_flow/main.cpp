#include <iostream>

using std::cout;
using std::endl;
using std::string;

struct Temp
{
    int value;
};

Temp getStruct()
{
    return Temp{};
}

// block scope switch statement
int main()
{
    auto temp = 5 > 3 ? "true" : "false";
    cout << temp << endl;
    switch (Temp temp = getStruct(); temp.value)
    {
    case 0:
        cout << 0 << endl;
        break;
    case 1:
    case 2:
        cout << "1 or 2" << endl;
        break;
    default:
        break;
    }
}
