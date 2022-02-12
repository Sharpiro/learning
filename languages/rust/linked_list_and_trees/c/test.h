#pragma once

#include <stdio.h>
#include <stdlib.h>

void panic(const char *message);

void assert_eq(int a, int b);

void assert_eq(void *a, void *b);
