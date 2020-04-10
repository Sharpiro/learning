/**
 * summary: this does **NOT** directly correlate to intersections/unions in set theory...sigh
 * when type is A | B then you have access to properties of A AND B (all in common)
 * when type is A & B then you have access to properties of A OR B (all)
 */

declare function assertTrue<_T extends true>(): void;
declare function assertFalse<_T extends false>(): void;
/** Expands `&` types */
type Evaluate<T> = { [P in keyof T]: T[P] };
/**
 * Determines if types `X` and `Y` are equal.  
 * Since we only have `extends` available, we must instead compare functions to determine equality
 */
type TypeEqual<X, Y> = // todo: combine assert and type equal? ...hard, impossible?
  (<T>() => T extends X ? true : false) extends
  (<T>() => T extends Y ? true : false) ? true : false;

/** ands and ors with string literal types */

type AliceFavFoods = "eggs" | "tacos";
type BobFavFoods = "pasta" | "tacos";
type AliceOrBobFoods = AliceFavFoods | BobFavFoods;
assertTrue<TypeEqual<"eggs" | "tacos" | "pasta", AliceOrBobFoods>>();

type AliceAndBobFoods = AliceFavFoods & BobFavFoods;
type AliceAndBobFoods2 = ("eggs" | "tacos") & ("pasta" | "tacos");
type AliceAndBobFoods3 = ("eggs" & "pasta") | ("eggs" & "tacos") | ("tacos" & "pasta") | ("tacos" & "tacos");
type AliceAndBobFoods4 = ("eggs" & "pasta") | ("eggs" & "tacos") | ("tacos" & "pasta") | "tacos";
type AliceAndBobFoods5 = never | never | never | "tacos";
type AliceAndBobFoods6 = "tacos";

assertTrue<TypeEqual<AliceAndBobFoods, AliceAndBobFoods2>>();
assertTrue<TypeEqual<AliceAndBobFoods, AliceAndBobFoods3>>();
assertTrue<TypeEqual<AliceAndBobFoods, AliceAndBobFoods4>>();
assertTrue<TypeEqual<AliceAndBobFoods, AliceAndBobFoods5>>();
assertTrue<TypeEqual<AliceAndBobFoods, AliceAndBobFoods6>>();
assertTrue<TypeEqual<"tacos", AliceAndBobFoods>>();

/** ands and ors with object types */

type EggsObj = { common: 0, eggs: "eggs"; special: true; };
type TacosObj = { common: 0, tacos: "tacos", sauce: "marinara"; };
type PastaObj = { common: 0, pasta: "pasta", sauce: "spaghetti", special: false; };

type AliceFavObj = EggsObj | TacosObj;
type BobFavObj = PastaObj | TacosObj;
type AliceOrBobFav = AliceFavObj | BobFavObj;

type AliceAndBobFav = AliceFavObj & BobFavObj;
type AliceAndBobFav2 = (EggsObj | TacosObj) & (PastaObj | TacosObj);
type AliceAndBobFav3 = (EggsObj & PastaObj) | (EggsObj & TacosObj) | (TacosObj & PastaObj) | TacosObj;

assertTrue<TypeEqual<AliceAndBobFav, AliceAndBobFav3>>();
assertTrue<TypeEqual<AliceAndBobFav, AliceAndBobFav2>>();

type AliceAndBobFavKeys = keyof AliceAndBobFav;
assertTrue<TypeEqual<"common", keyof AliceOrBobFav>>();
assertTrue<TypeEqual<"common" | "sauce", AliceAndBobFavKeys>>();

type EggsOrTacosKeys = keyof (EggsObj | TacosObj);
assertTrue<TypeEqual<"common", EggsOrTacosKeys>>();

type EggsAndTacos = Evaluate<EggsObj & TacosObj>;
type EggsAndTacosKeys = keyof EggsAndTacos;
assertTrue<TypeEqual<"eggs" | "tacos" | "common" | "special" | "sauce", EggsAndTacosKeys>>();

/** merging objects with same property name but different type */

type PropType1 = { x: number }
type PropType2 = { x: string }
type MergedProp = (PropType1 & PropType2)["x"]

assertTrue<TypeEqual<MergedProp, never>>()
