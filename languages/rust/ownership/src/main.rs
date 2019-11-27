fn main() {
    println!("integer");
    let x = 5;
    println!("{:?}", x);
    borrow_int(x);
    println!("{:?}", x);

    println!("string");
    let mut x = String::from("test");
    println!("{:?}", x);
    borrow_string_ref(&x); // string can still be used after this
    println!("{:?}", x);
    x = take_and_return(x); // string can be reassigned b/c of 
    println!("{:?}", x);
    borrow_string(x); // string cannot be used after this point!
}

fn borrow_int(num: i32) {
    println!("{:?}", num);
}

fn borrow_string(data: String) {
    println!("{:?}", data);
}

fn borrow_string_ref(data: &String) {
    println!("{:?}", data);
}

fn take_and_return(data: String) -> String {
    data
}
