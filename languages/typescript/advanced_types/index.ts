/** Pick properties from a class */
type Props<T> = Pick<T, Exclude<({ [K in keyof T]: T[K] extends Function ? never : K; }[keyof T]), undefined>>;

/** force a type to be evaluated now */
type Eval<T> = { [P in keyof T]: T[P] };

/**
 * Determines if types `X` and `Y` are equal.  
 * Since we only have `extends` available, we must instead compare functions to determine equality
 */
type TypeEqual<X, Y> = // todo: combine assert and type equal? ...hard, impossible?
  (<T>() => T extends X ? true : false) extends
  (<T>() => T extends Y ? true : false) ? true : false;

// /**   */

/**
 * Asserts a type evaluates to true
 * @example assertTrue<TypeEqual<{ x: number; }, { x: number; }>>();
 */
declare function assertTrue<_T extends true>(): void;

/** 
 * Asserts a type evaluates to false 
 * @example assertFalse<TypeEqual<{ x: number; }, { x: number; y: number; }>>();
 */
declare function assertFalse<_T extends false>(): void;
