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
pub struct Counter {
    index: usize,
    data: Vec<char>,
}

#[wasm_bindgen]
impl Counter {
    pub fn new(data: String) -> Counter {
        Counter {
            index: 0,
            data: data.chars().collect(),
        }
    }
}

#[wasm_bindgen]
impl Counter {
    pub fn next(&mut self) -> Option<char> {
        let result = if self.index < self.data.len() {
            log!("rust_log-{}", self.index);
            self.data.get(self.index).copied()
        } else {
            None
        };
        self.index += 1;
        result
    }
}
