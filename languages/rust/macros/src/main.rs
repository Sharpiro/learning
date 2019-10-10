// cspell:disable

macro_rules! list {
    ( $( $x:expr ),* ) => {
        {
            let mut temp_vec = Vec::new();
            $(
                temp_vec.push($x);
                temp_vec.push($x);
            )*
            temp_vec
        }
    };
}

macro_rules! cond {
    (  $x:expr, $y:expr, $z:expr  ) => {{
        if $x {
            $y
        } else {
            $z
        }
    }};
}

fn main() {
    println!("{:?}", list!(5));
    println!("{:?}", cond!(true, "hi", "there"));
}
