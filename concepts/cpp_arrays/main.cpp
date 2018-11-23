// various ways to declare an array
void arrayDeclarations()
{
    double *p;
    double balance[50];
    p = balance;

    int temp1[1]{'a'};
    int temp2[1] = {'a'};
    
    int *baz[5] = {};
    char *myPtrArray[] = {0};
    int myarray[]{1, 2, 3, 4};
}

// returning stack array (bad)
int *get()
{
    int myarray[]{1, 2, 3, 4};
    return myarray;
}

// returning stack array (bad)
int *get2()
{
    int myarray[]{99, 99, 99, 99};
    return myarray;
}

// returning heap array
int *get3()
{
    int *tester = new int[4];
    tester[0] = 1;
    return tester;
}

// returning heap array
int *get4()
{
    int *tester = new int[4];
    tester[0] = 99;
    return tester;
}

// c++ cannot return an array from a function
// instead return a pointer to the beginning of the array
int *fn2(int arr[])
{
    return arr;
}

int main()
{
    // calling 'get2' screws up 'data1' b/c of stack alloction
    auto data1 = get();
    auto data2 = get2();

    // no problem here b/c arrays were created on heap
    auto data3 = get3();
    auto data4 = get4();
}
