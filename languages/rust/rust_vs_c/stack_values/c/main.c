#include <stdio.h>
#include <stdlib.h>
#include <string.h>

char *mod_string(char *str)
{
  str[0] = 'h';
  puts(str);
  return str;
}

// works b/c a static string is created
char *static_char_pointer()
{
  char *hello_world_arr = "Hello world";
  return hello_world_arr;
}

// fails b/c array is on stack and destroyed once function ends
// C gives warning about this
char *stack_char_pointer()
{
  char hello_world_arr[] = "Hello world";
  return hello_world_arr;
}

int main()
{
  char *static_ptr = static_char_pointer();
  puts(static_ptr);
  char *stack_ptr = stack_char_pointer();
  puts(stack_ptr);

  // works b/c array is still in scope and is allocated on stack
  char hello_world_arr[] = "Hello world";
  mod_string(hello_world_arr);
  // fails despite array still being in scope, b/c you can't modify a static string
  // C gives no warning or error
  // C++ gives warning
  char *hello_world_ptr = "Hello world";
  mod_string(hello_world_ptr);
}
