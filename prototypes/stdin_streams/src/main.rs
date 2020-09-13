use std::io::{self, Read, Write};
use std::str;

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
      eprintln!("exited cleanly");
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
          io::stdout().write_all(data)?
        }
        Err(err) => io::stdout().write_all(err)?,
      },
      State::Initialized => {
        let data = big_number(read_buffer);
        io::stdout().write_all(data)?;
      } //   State::Initialized => match big_number(read_buffer) {
        //     Ok(data) => {
        //       state = State::Initialized;
        //       io::stdout().write_all(data)?
        //     }
        //     Err(err) => io::stdout().write_all(err)?,
        //   },
        // }
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
    Ok(b"connection initialized\n")
  } else {
    Err(b"bad client hello\n")
  }
}

// fn big_number(_buffer: &[u8]) -> Result<&[u8], &[u8]> {
fn big_number(_buffer: &[u8]) -> &[u8] {
  let _data = &[0, 0, 1, 255, 0x0a];
  _data
}
