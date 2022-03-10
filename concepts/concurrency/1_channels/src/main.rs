use std::sync::mpsc;
use std::{
    thread,
    time::{Duration, Instant},
};

// const MAX_NUMBER: u32 = 1_0_000_000;
// // const SECRET_NUMBER: u32 = MAX_NUMBER / 2;
// const SECRET_NUMBER: u32 = MAX_NUMBER;

fn main() {
    let (tx, rx) = mpsc::channel();
    let now = Instant::now();
    let thread_one = thread::spawn(move || {
        // let found_number = guess_number(0, MAX_NUMBER / 2);
        // let found_number = guess_number(0, MAX_NUMBER);
        thread::sleep(Duration::from_secs(5));
        tx.send(String::from("message from thread 1")).unwrap();
        println!("thread 1 done");
        // found_number
    });

    let thread_two = thread::spawn(move || {
        loop {
            // let result = rx.recv();
            let result = rx.try_recv();
            println!("{:?}", result);
            if let Ok(_) = result {
                break;
            }
            thread::sleep(Duration::from_secs(1));
        }
        println!("thread 2 done");
        // let found_number = guess_number(MAX_NUMBER / 2, MAX_NUMBER);
        // found_number
    });

    let guess_result = thread_one.join().unwrap();
    println!("result: {:?}", guess_result);
    let guess_result = thread_two.join().unwrap();
    println!("result: {:?}", guess_result);

    println!("elapsed: {:?}", now.elapsed());
}

// fn guess_number(start: u32, stop: u32) -> Option<u32> {
//     for i in start..=stop {
//         if i == SECRET_NUMBER {
//             return Some(i);
//         }
//         if i > 0 && i % 100 == 0 {
//             // check cancellation
//         }
//     }
//     return None;
// }
