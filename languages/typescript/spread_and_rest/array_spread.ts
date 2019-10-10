// Basic javascript spread does not work in typescript

// function sum(x, y, z) {
//     return x + y + z;
// }

// const numbers = [1, 2, 3];

// console.log(sum(...numbers));


// https://github.com/Microsoft/TypeScript/pull/24897

function foo(...args: [number, string, boolean]): void {
    console.log(args);
    console.log(args[0]);
    console.log(args[1]);
    console.log(args[2]);
    // console.log(args[3]); // error
}

const args: [number, string, boolean] = [42, "hello", true];
foo(42, "hello", true);
foo(args[0], args[1], args[2]);
foo(...args);
