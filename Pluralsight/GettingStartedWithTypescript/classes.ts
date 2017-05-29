class Octopus {
    public static testStatic = "hi"

    private testField = "nada"
    public publicField = "x"

    constructor(init?: Partial<Octopus>) {
        Object.assign(this, init)
    }

    get test(): string {
        return this.testField
    }

    set test(v: string) {
        this.testField = v
    }
}

let octopus = new Octopus()
let octopus2 = new Octopus({ publicField: "hi", test: "oh" })
console.log(octopus.test)
octopus.test = "not test"
console.log(octopus.test)
// octopus.test = undefined;

let temp123 = Octopus.testStatic


class MealEntry {
    id: number
    mealEntryNumber: number
    mealId: number
    foodId: number
    calories: number

    public constructor(init?: Partial<MealEntry>) {
        Object.assign(this, init)
    }
}

let xxxxx = Number("a")
let yyyy = String(1)