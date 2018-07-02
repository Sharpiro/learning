interface Person {
    name: string;
}

class Employee implements Person {
    name = "dave";
}

export default class DefaultExport {
    defaultExportProperty = true;
}

export { Person, Employee as Emp };