import { Emp as Employee, Person } from "./testModule";
import * as TestModule from "./testModule";
import Test from "testModule";

let employee: TestModule.Person;

employee = new Employee();

console.log(employee.name);

const test = new Test();
console.log(test.defaultExportProperty);
