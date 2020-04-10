declare function assertType<T>(_item: T): void;
declare function assertTrue<_T extends true>(): void;
declare function assertFalse<_T extends false>(): void;

// todo: combine assert and type equal? hard
type TypeEqual<X, Y> =
  (<T>() => T extends X ? true : false) extends
  (<T>() => T extends Y ? true : false) ? true : false;

type AliceFavFoods = "eggs" | "tacos";
type BobFavFoods = "pasta" | "tacos";
type AliceOrBobFoods = AliceFavFoods | BobFavFoods;
type AliceAndBobFoods = AliceFavFoods & BobFavFoods;
type AliceAndBobFoods2 = ("eggs" | "tacos") & ("pasta" | "tacos");
type AliceAndBobFoods3 = ("eggs" & "pasta") | ("eggs" & "tacos") | ("tacos" & "pasta") | ("tacos" & "tacos");
type AliceAndBobFoods4 = ("eggs" & "pasta") | ("eggs" & "tacos") | ("tacos" & "pasta") | "tacos";

assertTrue<TypeEqual<AliceAndBobFoods, AliceAndBobFoods2>>();
assertTrue<TypeEqual<AliceAndBobFoods, AliceAndBobFoods3>>();
assertTrue<TypeEqual<AliceAndBobFoods, AliceAndBobFoods4>>();
assertTrue<TypeEqual<"eggs" | "tacos" | "pasta", AliceOrBobFoods>>();
assertTrue<TypeEqual<"tacos", AliceAndBobFoods>>();

type EggsObj = { common: 0, eggs: "eggs"; special: true; };
type TacosObj = { common: 0, tacos: "tacos", sauce: "marinara"; };
type PastaObj = { common: 0, pasta: "pasta", sauce: "spaghetti", special: false; };

type AliceFavObj = EggsObj | TacosObj;
type BobFavObj = PastaObj | TacosObj;
type AliceOrBobFav = AliceFavObj | BobFavObj;

type AliceAndBobFav = AliceFavObj & BobFavObj;
type AliceAndBobFav2 = (EggsObj | TacosObj) & (PastaObj | TacosObj);
type AliceAndBobFav3 = (EggsObj & PastaObj) | (EggsObj & TacosObj) | (TacosObj & PastaObj) | (TacosObj & TacosObj);

assertTrue<TypeEqual<AliceAndBobFav, AliceAndBobFav3>>();
assertTrue<TypeEqual<AliceAndBobFav, AliceAndBobFav2>>();

type AliceAndBobFavKeys = keyof AliceAndBobFav;
assertTrue<TypeEqual<"common", keyof AliceOrBobFav>>();
assertTrue<TypeEqual<"common" | "sauce", AliceAndBobFavKeys>>();

// unions (OR) give access to properties on both types only until narrowed
type EggsOrTacosKeys = keyof (EggsObj | TacosObj);
assertTrue<TypeEqual<"common", EggsOrTacosKeys>>();

// x-sections (AND) give access to all properties on both types
type EggsAndTacosKeys = keyof (EggsObj & TacosObj);
assertTrue<TypeEqual<"eggs" | "tacos" | "common" | "special" | "sauce", EggsAndTacosKeys>>();
