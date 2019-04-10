#include "test.h"
#include "child.h"
#include "scopedEnums.h"
#include <iostream>

// not recommended
using namespace std;

// reccomended
using std::cin;
using std::cout;
using std::string;

int main()
{
    Child test2;

    {
        Child test = Child(50, 50);
    }

    Child test = Child(100, 100);
    // Test *test3 = new Test(500);

    // test.DoNothing();
    // test2.DoNothing();
    // test3->DoNothing();
    // delete test3;

    auto temp = FileError::notfound;

    string x;
}
