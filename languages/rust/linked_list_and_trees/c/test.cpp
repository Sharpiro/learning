#include "test.h"

void panic(const char *message)
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

void assert_eq(void *a, void *b)
{
  if (a != b)
  {
    panic("not equal");
  }
}