use std::io::{self, Read, Write};
use std::str;
use rand;

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
    eprintln!("---data: {:?}", str::from_utf8(read_buffer)?);

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
        //     Ok(data) => {
        //       state = State::Initialized;
        //       io::stdout().write_all(data)?
        //     }
        //     Err(err) => io::stdout().write_all(err)?,
        //   },
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
    Ok(b"connection initialized")
  } else {
    Err(b"bad client hello")
  }
}

fn big_number(_buffer: &[u8]) -> [u8; 16] {
  // let temp = u32::from_be_bytes(b"abcd");
  // let data = 10032_u32.to_be_bytes();
  let data = rand::random();
  // let data = &[0, 0, 1, 255, 0x0a];
  data
}
