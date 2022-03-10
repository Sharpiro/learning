use std::{
    sync::{
        atomic::{AtomicBool, Ordering},
        Arc,
    },
    thread,
    time::Duration,
};

fn main() {
    let is_done = Arc::new(AtomicBool::new(false));
    let is_done_clone_1 = Arc::clone(&is_done);
    let is_done_clone_2 = Arc::clone(&is_done);

    // note: spawn order doesn't matter
    let handles = vec![
        thread::spawn(move || {
            for i in 0..10 {
                println!("thread 1 tick...");
                thread::sleep(Duration::from_secs(1));
                if i == 5 {
                    is_done_clone_1.store(true, Ordering::SeqCst);
                    break;
                }
            }
        }),
        thread::spawn(move || loop {
            thread::sleep(Duration::from_secs(1));
            println!("thread 2 infinite loop...");
            if is_done_clone_2.load(Ordering::SeqCst) {
                break;
            }
        }),
    ];

    for (i, handle) in handles.into_iter().enumerate() {
        handle.join().unwrap();
        println!("thread {} done", i + 1);
    }

    println!("is done: {:?}", is_done);
}
