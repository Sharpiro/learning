# C Lang

## create statically linked library

```sh
gcc -c -o out.o out.c
ar rcs libout.a out.o
```

## create a shared (dynamically linked) library

```sh
gcc -shared -o libhello.so -fPIC hello.c
```

## compile program with linked library

### library will be resolved in standard linker directory

```sh
gcc main.c -L lib -l adder -l multiplier
```

### library will be resolved relative to working directory

```sh
gcc main.c lib/libadder.a lib/libmultiplier.so
```

## view shared object dependencies

```sh
ldd main.out
```

## array parameter decay

<https://www.reddit.com/r/C_Programming/comments/t61f7o/comment/hz8mqy4>

- C priority for declarators and expressions
  1. parens
  2. postfix
  3. prefix

# 1 dimension

```c
// both are pointer to int or pointer to first element of array of int
// both degrade to `int *a`
void test2(int *a)
void test3(int a[])
```

# 2 dimension

```c
// pointer to pointer of int or pointer to first element of first array of int
// cannot use a[0][0], would error or segfault b/c *a is actually a value, not a memory address
void test1(int **a);
// array of int pointers
// degrades to double int pointer to int like above
void test2(int *a[SIZE]);
// array of int array of size SIZE
// degrades to pointer to int array of size SIZE
void test3(int a[][SIZE]);
// no degrade, same as test3
void test4(int (*a)[SIZE]);
// degrades to double char pointer
// argv[0][0] works and gets the first char of the first c string
int main(int argc, char *argv[])
// same as above
// argv[0][0] likely works b/c c strings can be delimitted by 0x00
// (char **) is handled differently than (int **) b/c c strings have delimiter
int main(int argc, char **argv)
```

## common options

- `-o` - output
- `-g` - debug
- `-c` - compile only, don't link
- `-shared` - create shared library
- `-fPIC` - format position independant code 
  - required for formatting shared libraries
- `-Wall` - show all gcc warnings
- `-I` - add path to header files for compiler
- `-L` - add library path for linker
- `-l` - specify a library for the linker
  - `-l adder` - will link with a library named `libadder.a/so`
  - `-l:libadder.a` - will link with a library named `libadder.a`
