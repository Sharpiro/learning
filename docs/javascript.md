# javascript

## prototypes

```js
class Temp {
  temp = {};
  static tempStatic = {};

  method() {}
  static methodStatic() {}
}

Temp.prototype.protoStatic = {};
Temp.prototype.protoMethodLambda = () => {};

console.log("Temp.tempStatic", Temp.tempStatic);
console.log("eq static", Temp.tempStatic === Temp.tempStatic);
console.log("-");

console.log("new Temp().temp", new Temp().temp);
console.log("eq instance", new Temp().temp === new Temp().temp);
console.log("-");

console.log("Temp.protoStatic", Temp.protoStatic);
console.log("new Temp().protoStatic", new Temp().protoStatic);
console.log("eq instance", new Temp().protoStatic === new Temp().protoStatic);
console.log("-");

console.log("static access", Temp.tempStatic);
console.log("static access on instance", new Temp().tempStatic);

console.log(Temp);
console.log(new Temp());
```
