#include <stdio.h>

typedef struct
{
  int x;
} TempStruct;

void print_data(const TempStruct *data)
{
  printf("%p\n", data);
}

TempStruct get_data()
{
  TempStruct data = {.x = 1};
  return data;
}

int main()
{
  // TempStruct temp = get_data();
  // print_data(&get_data());
}
