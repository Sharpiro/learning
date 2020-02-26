export function* validateDefined(obj: { [x: string]: any }) {
  for (const x in obj) {
    if (obj[x] == undefined) {
      yield `property '${x}' was undefined on class '${obj.constructor.name}'`
    }
  }
}
