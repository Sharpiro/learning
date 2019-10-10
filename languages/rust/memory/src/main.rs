use std::collections::hash_map::DefaultHasher;
use std::hash::{Hash, Hasher};

#[derive(Debug, Hash)]
struct Test {
    x: i32,
}

fn main() {
    let _test = Test { x: 1 };
    println!("1. {:?}", calculate_hash(&format!("{:?}", &_test as *const _)));
    let _test2 = _test;
    println!("2. {:?}", calculate_hash(&format!("{:?}", &_test2 as *const _)));
}

// fn main() {
//     let mut _test = Test { x: 1 };
//     println!("1. {:?}", calculate_hash(&format!("{:?}", &_test as *const _)));
//     // let _test2 = take(_test);
//     _test = take(_test);
//     // println!("3. {:?}", calculate_hash(&format!("{:?}", &_test2 as *const _)));
//     println!("3. {:?}", calculate_hash(&format!("{:?}", &_test as *const _)));
// }

// fn take(_test: Test) -> Test {
//     let _addr = &_test as *const _;
//     println!("2. {:?}", calculate_hash(&format!("{:?}", &_test as *const _)));
//     _test
// }

// fn main() {
//     let _test = Test { x: 1 };
//     let mut _addr = &_test as *const _;
//     println!("1. {:?}", calculate_hash(&format!("{:?}", _addr)));
//     let _test2 = borrow(&_test);
//     println!("3. {:?}", calculate_hash(&format!("{:?}", _addr)));
//     _addr = _test2 as *const _;
//     println!("4. {:?}", calculate_hash(&format!("{:?}", _addr)));
// }

// fn borrow(_test: &Test) -> &Test {
//     let _addr = _test as *const _;
//     println!("2. {:?}", calculate_hash(&format!("{:?}", _addr)));
//     _test
// }

fn calculate_hash<T: Hash>(t: &T) -> u64 {
    let mut s = DefaultHasher::new();
    t.hash(&mut s);
    s.finish()
}