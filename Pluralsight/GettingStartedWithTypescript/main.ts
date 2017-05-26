const test = [1, 2, 3];
test[0] = 99;

let unionType: number | string;
unionType = 2;

unionType = "2"
unionType = "2";
console.log(test);

let temp: string | null = null;
console.log(temp);

let value: any = 5.49;
var fixed = (<ITest>value).toString()
console.log(fixed);

;
// var el = document.getElementById("test");
// el.innerText = "x";

var x: ITest = { getString: () => null };


var testString = x.getString();
if (testString !== null)
    testString.toString();

interface ITest {
    getString(): string | null;
}
