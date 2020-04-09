declare function assertType<T>(_item: T): void;
declare function assertTrue<_T extends true>(): void;
declare function assertFalse<_T extends false>(): void;

// todo: combine assert and type equal?
type TypeEqual<X, Y> =
  (<T>() => T extends X ? true : false) extends
  (<T>() => T extends Y ? true : false) ? true : false;

type AliceFavFoods = "eggs" | "tacos"
type BobFavFoods = "pasta" | "tacos"
type AliceOrBobFoods = AliceFavFoods | BobFavFoods // gets all unique
type AliceAndBobFoods = AliceFavFoods & BobFavFoods // gets in common

assertTrue<TypeEqual<"eggs" | "tacos" | "pasta", AliceOrBobFoods>>();
assertTrue<TypeEqual<"tacos", AliceAndBobFoods>>();

type EggsObj = { common: 0, eggs: "eggs" }
type TacosObj = { common: 0, tacos: "tacos" }
type PastaObj = { common: 0, tacos: "tacos" }

declare type EggsOrTacos = EggsObj | TacosObj;
type EggsOrTacosKeys = keyof EggsOrTacos;

assertTrue<TypeEqual<"common", EggsOrTacosKeys>>();

declare type EggsAndTacos = EggsObj & TacosObj;
type EggsAndTacosKeys = keyof EggsAndTacos;

assertTrue<TypeEqual<"eggs" | "tacos" | "common", EggsAndTacosKeys>>();
