// cspell:disable
use rand::prelude::*;

fn _random_byte_array() {
    println!("random byte array");
    // simple shorthands
    // max gen of 32
    let random_bytes = rand::random::<[u8; 32]>();
    println!("{:?}", random_bytes);

    // fill array
    let mut bytes = [0; 64];
    rand::thread_rng().fill_bytes(&mut bytes);
    println!("{:?}", &bytes[..]);
}

fn _random_byte_vector() {
    println!("random byte vector");
    // let my_vec = vec![1, 2, 3, 4, 5, 6, 7, 8, 9];

    let random_bytes = (0..64).map(|_| rand::random::<u8>()).collect::<Vec<u8>>();
    let _rand_vec = Vec::from(rand::random::<[u8; 32]>());
    println!("{:?}", random_bytes);
}

fn _random_number() {
    println!("random number");
    let num = rand::random::<u8>();
    println!("{:?}", num);
}

fn _shuffle() {
    println!("random shuffle");
    let mut nums: Vec<i32> = (1..100).collect();
    nums.shuffle(&mut rand::thread_rng());
    println!("{:?}", nums);
}

fn main() {
    _shuffle();
    _random_number();
    _random_byte_array();
    _random_byte_vector();
}
