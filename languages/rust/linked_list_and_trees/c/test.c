#include "test.h"

void panic(char *message)
{
  fprintf(stderr, "ERROR: %s\n", message);
  exit(1);
}

void assert_eq(int a, int b)
{
  if (a != b)
  {
    panic("not equal");
  }
}