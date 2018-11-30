interface A { a: any }
interface XYZ { x: any; y: any; z: any; }
interface XYZA extends XYZ, A { }

function dropXYZ<T extends XYZ>(obj: T) {
    let { x, y, z, ...rest } = obj;
    return rest;
}

function merge<T, U>(x: T, y: U) {
    return { ...x, ...y };
}

const data1: XYZ = { x: 1, y: 2, z: 3 }
const data2: A = { a: 99 }

// merge object using spread
const temp = merge(data1, data2)

// breakup object using rest
const x: Pick<XYZA, "a"> = dropXYZ(temp)
const y: Pick<XYZA, "a"> = temp
