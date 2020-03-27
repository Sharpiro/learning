mod utils;

use wasm_bindgen::prelude::*;

extern crate web_sys;

macro_rules! log {
    ( $( $t:tt )* ) => {
        web_sys::console::log_1(&format!( $( $t )* ).into());
    }
}

#[cfg(feature = "wee_alloc")]
#[global_allocator]
static ALLOC: wee_alloc::WeeAlloc = wee_alloc::WeeAlloc::INIT;

#[wasm_bindgen]
extern "C" {
    fn alert(s: &str);
}

#[wasm_bindgen]
pub fn get_counter() -> std::slice::Iter {
    // let item = Counter::new();
    // item
    let temp = vec![1, 2, 3, 4, 500];
    let temp2 = temp.iter();
    temp2
    // let temp = item.iter();
    // let temp2 = item.into_iter();
    // log!("{}", message);

    // let a = [1, 2, 3];
    // assert_eq!(a.iter().count(), 3);
    // for x in &a {
    //     println!("{}", x);
    // }

    // for x in item {
    //     println!("{}", x);
    // }
}

#[wasm_bindgen]
pub struct Counter {
    count: usize,
    // vector: Vec<usize>, // todo: what if i don't want a vec?
}

impl Counter {
    fn new() -> Counter {
        Counter {
            count: 0,
            // vector: vec![1, 2, 3, 4, 5],
        }
    }
}

impl Iterator for Counter {
    type Item = usize;

    fn next(&mut self) -> Option<Self::Item> {
        self.count += 1;
        log!("test-{}", self.count);

        if self.count < 6 {
            Some(self.count)
        } else {
            None
        }
    }
}

// impl IntoIterator for Counter {
//     type Item = usize;
//     type IntoIter = std::vec::IntoIter<Self::Item>;

//     fn into_iter(self) -> Self::IntoIter {
//         self.vector.into_iter()
//     }
// }
