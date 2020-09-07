use std::io::{self, Read, Write};
use std::str;
// use std::thread;

fn main() -> Result<(), Box<dyn std::error::Error>> {
  println!("starting stdin_streams");
  let mut i = 0;
  loop {
    if i > 50 {
      panic!("max loop iterations")
    }

    let mut buffer = [0u8; 100];
    let bytes_read = io::stdin().read(&mut buffer)?;
    if bytes_read == 0 {
      io::stdout().write_all(b"done to stdout")?;
      return Ok(());
    }
    i += 1;

    let read_buffer = &buffer[..bytes_read];
    println!("read: {}", bytes_read);
    println!("data: {:02x?}", read_buffer);
    println!("data: {:?}", str::from_utf8(read_buffer)?);
    // thread::sleep(std::time::Duration::from_secs(5))
  }
}
