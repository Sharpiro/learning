#pragma once

class Person
{
private:
  int number;
  int complexNumber;

public:
  int GetNumber() const
  {
    // can't use because 'const'
    // number = 99;
    return number;
  }
  int GetComplexNumber() const;
};