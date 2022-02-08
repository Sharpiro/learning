#![allow(dead_code)]
#![allow(unused_variables)]

#[derive(Debug)]
struct MyStruct {
    x: i32,
    y: i32,
}

// fn mod_member(my_struct: &mut MyStruct) {
//     my_struct.x = 2;
// }

fn mod_obj(my_struct: &mut MyStruct) {
    *my_struct = MyStruct { x: 3, y: 4 };
}

fn main() {
    let mut my_struct = MyStruct { x: 1, y: 2 };
    println!("{:?}", my_struct);
    mod_obj(&mut my_struct);
    println!("{:?}", my_struct);
}
