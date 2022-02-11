#![allow(dead_code)]

fn main() {
    // string
    println!("---string slice---");
    let data = get_stack_str_ref();
    let data_pointer = data as *const str as *const u8;
    let data_pointer_pointer = &data_pointer as *const *const u8;

    println!("---main---");
    println!("pointer address: {:?}", data_pointer_pointer);
    println!("pointer value: {:?}", data_pointer);
    unsafe {
        println!("pointer deref: {:?}", *data_pointer as u8);
    }

    // array
    println!("\n\n---array---");
    let data = get_stack_arr();
    let data_pointer = &data as *const [u8] as *const usize;
    let data_pointer_pointer = &data_pointer as *const *const usize;

    println!("---main---");
    println!("pointer address: {:?}", data_pointer_pointer);
    println!("pointer value: {:?}", data_pointer);
    unsafe {
        println!("pointer deref: {:?}", *data_pointer as u8);
    }

    // array
    println!("\n\n---array struct---");
    let data = get_stack_arr_struct();
    let data_pointer = &data as *const [NoCopyStruct] as *const usize;
    let data_pointer_pointer = &data_pointer as *const *const usize;

    println!("---main---");
    println!("pointer address: {:?}", data_pointer_pointer);
    println!("pointer value: {:?}", data_pointer);
    unsafe {
        println!("pointer deref: {:?}", *data_pointer as u8);
    }
}

/// returns an array deep copied from local 'arr'
fn get_stack_arr() -> [u8; 4] {
    let data = [1, 2, 3, 4];
    let data_pointer = &data as *const [u8] as *const usize;
    let data_pointer_pointer = &data_pointer as *const *const usize;

    println!("---func---");
    println!("pointer address: {:?}", data_pointer_pointer);
    println!("pointer value: {:?}", data_pointer);
    unsafe {
        println!("pointer deref: {:?}", *data_pointer as u8);
    }
    return data;
}

struct NoCopyStruct {
    x: u32,
}

/// returns an array of structs deep copied from local 'arr'
fn get_stack_arr_struct() -> [NoCopyStruct; 4] {
    let data = [
        NoCopyStruct { x: 2 },
        NoCopyStruct { x: 3 },
        NoCopyStruct { x: 4 },
        NoCopyStruct { x: 5 },
    ];
    let data_pointer = &data as *const [NoCopyStruct] as *const usize;
    let data_pointer_pointer = &data_pointer as *const *const usize;

    println!("---func---");
    println!("pointer address: {:?}", data_pointer_pointer);
    println!("pointer value: {:?}", data_pointer);
    unsafe {
        println!("pointer deref: {:?}", *data_pointer as u8);
    }
    return data;
}

/// returns a pointer to the string stored in the binary
fn get_stack_str_ref() -> &'static str {
    let data = "abcd";
    let data_pointer = data as *const str as *const usize;
    let data_pointer_pointer = &data_pointer as *const *const usize;

    println!("---func---");
    println!("pointer address: {:?}", data_pointer_pointer);
    println!("pointer value: {:?}", data_pointer);
    unsafe {
        println!("pointer deref: {:?}", *data_pointer as u8);
    }
    return data;
}
