use rand;
use std::convert::TryInto;
use std::io::{self, Read, Write};
use std::str;

#[allow(unused_macros)]
macro_rules! writeln_buffer {
  ($buffer:expr) => {{
    let mut stdout = io::stdout();
    stdout.write_all(&$buffer[..])?;
    stdout.write_all(b"\n")
  }};
}

fn main() -> Result<(), Box<dyn std::error::Error>> {
  eprintln!("---starting stdin_streams");

  let mut state = State::Open;
  let mut i = 0;
  loop {
    if i > 50 {
      panic!("max loop iterations")
    }

    let mut buffer = [0u8; 1000];
    let bytes_read = io::stdin().read(&mut buffer)?;
    if bytes_read == 0 {
      eprintln!("---returned cleanly");
      return Ok(());
    }
    i += 1;

    let read_buffer = &buffer[..bytes_read];
    eprintln!("---read: {}", bytes_read);
    eprintln!("---data: {:02x?}", read_buffer);
    if let Ok(data) = str::from_utf8(read_buffer) {
      eprintln!("---data: {:?}", data);
    }

    match state {
      State::Open => match client_hello(read_buffer) {
        Ok(data) => {
          state = State::Initialized;
          writeln_buffer!(data)?;
        }
        Err(err) => writeln_buffer!(err)?,
      },
      State::Initialized => {
        let data = big_number(read_buffer);
        writeln_buffer!(data)?;
      }
    }
  }
}

#[derive(PartialEq)]
enum State {
  Open,
  Initialized,
}

fn client_hello(buffer: &[u8]) -> Result<&[u8], &[u8]> {
  if buffer == b"hello\n" {
    Ok(b"hello")
  } else {
    Err(b"bad client hello")
  }
}

fn big_number(buffer: &[u8]) -> [u8; 16] {
  // let (split, _) =buffer.split_at(std::mem::size_of::<u32>());
  let _temp2 = u32::from_be_bytes(buffer.try_into().expect("failure"));
  eprintln!("---received encrypted value {}", _temp2);
  let p = 37;
  let q = 53;
  let n = 1961;
  let phi_n = (p - 1) * (q - 1);
  let e = 17;
  let (_, d, _) = egcd(e, phi_n);
  let y = _temp2;
  let x = pow_mod(y, d as u32, n);
  let k = simple_hash_u32(x);
  eprintln!("---decrypted value {}", x);
  eprintln!("---computed key {}", k);
  //  let y = x**e%N;
  // let data = 10032_u32.to_be_bytes();
  let data = rand::random();
  // let data = &[0, 0, 1, 255, 0x0a];
  data
}

fn egcd(a: i32, b: i32) -> (i32, i32, i32) {
  if a == 0 {
    return (b, 0, 1);
  } else {
    let (gcd, x, y) = egcd(b % a, a);

    let _temp = (b / a) * x;
    return (gcd, y - (b / a) * x, x);
  }
}

fn pow_mod(x: u32, y: u32, n: u32) -> u32 {
  let mut product = x;
  for _ in 0..y - 1 {
    product = product * x % n;
  }
  return product;
}

fn simple_hash_u32(x: u32) -> u32 {
  let buffer = x.to_be_bytes();
  simple_hash(&buffer)
}

fn simple_hash(buffer: &[u8]) -> u32 {
  const MASK_U32: u64 = std::u32::MAX as u64;
  let mut hash_address = 5381;

  for &byte in buffer {
    hash_address = (((hash_address << 5u64) & MASK_U32) + hash_address
      & MASK_U32)
      + byte as u64;
    eprintln!("{}", hash_address);
  }
  hash_address as u32
}

#[test]
fn egcd_test() {
  let e = 17;
  let phi_n = 1872;
  let (_, d, _) = egcd(e, phi_n);
  assert!(d == 881);
}

#[test]
fn hash_test() {
  let buffer = [0, 1, 2, 3, 4];
  let hash = simple_hash(&buffer);
  println!("{}", hash);
  assert!(hash == 134190447)
}

#[test]
fn hash_test_num() {
  let x = 1227;
  let hash = simple_hash_u32(x);
  println!("{}", hash);
}
