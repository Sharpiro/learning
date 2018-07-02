using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        //var array1 = new[] { 1, 2, 3, 4, 5 };
        //var array2 = array1.ToArray();
        //array1[2] = 99;

        //var array1 = new[] { 1, 2, 3, 4, 5 };
        //var array2 = array1.Skip(1).Take(3).ToArray();
        //array1[2] = 99;

        //var array1 = new[] { new Ref(1), new Ref(2), new Ref(3), new Ref(4), new Ref(5) };
        //var array2 = array1.Skip(1).Take(3).ToArray();
        //array1[2].Prop = 99;

        //var array1 = new Span<int>(new[] { 1, 2, 3, 4, 5 });
        //var array2 = array1.Slice(1, 3);
        //array2[2] = 99;

        //ref int x = ref 5;

        string data = "hello world";
        var readonlySpan = data.AsSpan();
        //Span<char> span = stackalloc char[2];
        Span<char> span = new char[readonlySpan.Length];
        ref readonly char pinnable = ref span.GetPinnableReference();
        // pinnable = 'a';
        readonlySpan.CopyTo(span);
        span[0] = 'z';

        var x = 1;
        var y = 2;
        y++;
        Test(ref x, y);

    }

    static void Test(ref int x, in int y)
    {
        WriteLine(x);
        WriteLine(y);
    }
}

[DebuggerDisplay("Ref = {Prop}")]
class Ref
{
    public int Prop { get; set; }
    public int Prop2 { get; set; }

    public Ref(int prop)
    {
        Prop = prop;
    }
}
