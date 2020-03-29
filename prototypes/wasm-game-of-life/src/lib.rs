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
pub struct ProgramIterator {
  index: usize,
  commands: Vec<char>,
  output: Vec<usize>,
  pub temp: u8,
}

#[wasm_bindgen]
impl ProgramIterator {
  pub fn new(program: String) -> ProgramIterator {
    ProgramIterator {
      index: 0,
      commands: program.chars().collect(),
      output: vec![9000001, 2, 3, 4, 5],
      temp: 12,
    }
  }
}

#[wasm_bindgen]
impl ProgramIterator {
  pub fn next(&mut self) -> Option<char> {
    let result = if self.index < self.commands.len() {
      log!("rust_log-{}", self.index);
      self.commands.get(self.index).copied()
    } else {
      None
    };
    self.index += 1;
    result
  }

  pub fn get_list(&mut self) -> Vec<usize> {
    // vec![9000000, 2, 3, 4, 5]
    let data = vec![9000000 + self.index, 2, 3, 4, 5];
    self.index += 1;
    data
  }

  pub fn bump_output(&mut self) {
    // self.output = vec![9000002, 2, 3, 4, 5]
    self.output[0] = 12;
  }
}

#[wasm_bindgen]
pub struct TempData {
  pub pointer_temp: *const u8,
}

#[wasm_bindgen]
struct TempData2 {
  pub data: u8,
  pub data2: u8,
  pub data3: u8,
  pub data4: u8,
  pub data5: u8,
  pub pointer_temp: *const u8,
}
