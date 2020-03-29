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
  output: Vec<u8>,
  memory: Vec<u8>,
}

#[wasm_bindgen]
impl ProgramIterator {
  pub fn new(program: String, memory_size: usize) -> ProgramIterator {
    ProgramIterator {
      index: 0,
      commands: program.chars().collect(),
      output: Vec::with_capacity(1),
      memory: vec![0; memory_size],
    }
  }

  pub fn next(&mut self) -> Option<char> {
    let result = if self.index < self.commands.len() {
      log!("rust_log-{}", self.index);
      self.output.push(0);
      self.commands.get(self.index).copied()
    } else {
      None
    };
    self.index += 1;
    result
  }

  pub fn get_output(&self) -> Vec<u8> {
    self.output.clone()
  }

  pub fn get_memory_ptr(&self) -> *const u8 {
    self.memory.as_ptr()
  }

  pub fn get_output_ptr(&self) -> *const u8 {
    log!("output len: {:?}", self.output.len());
    log!("output cap: {:?}", self.output.capacity());
    self.output.as_ptr()
  }

  pub fn bump_memory(&mut self) {
    self.memory[0] = self.memory[0] + 1;
    self.index += 1;
  }

  pub fn bump_output(&mut self) {
    self.output.push(self.index as u8);
    self.index += 1;
  }
}
