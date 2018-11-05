// never put using statements in a header file
// especially 'using namespace' statements
// it will cause anyone who uses your header file to get those namespaces as well
#pragma once

class Test
{
  protected:
    int _x;

  public:
    Test();
    ~Test();
    Test(int x);
    void DoNothing();
};
