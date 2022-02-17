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

void assert_eq(const void *a, const void *b)
{
  if (a != b)
  {
    panic("not equal");
  }
}

void assert_eq(const Vec<int> *a, const Vec<int> *b)
{
  if (a->len != b->len)
  {
    panic("not equal");
  }

  for (int i = 0; i < a->len; i++)
  {
    if (a->raw[i] != b->raw[i])
    {
      panic("not equal");
    }
  }
}
