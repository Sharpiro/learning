#pragma once

#include <stdio.h>
#include <stdlib.h>

void panic(char *message);

void assert_eq(int a, int b);

void assert_eq(void *a, void *b);
