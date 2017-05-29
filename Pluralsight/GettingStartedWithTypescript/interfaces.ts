interface IEmployee {
    name: string
    title: string
}

interface IPerson {
    name: string
    age?: number
}

doPeronStuff({ name: "test" })

function doPeronStuff(person: IPerson) {

}

doStuff({ name: "dave", title: "sharp" })

function doStuff(employee: IEmployee): void {
    console.log(employee)
}

let doStufflet = (employee: IEmployee) => {
    console.log(employee)
}

doStufflet({ name: "dave", title: "sharp" })

class Test {
    public doMethod(): void {

    }

    public doExpression = (): void => {

    }
}

let testClass = new Test()
