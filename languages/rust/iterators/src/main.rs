fn _temp_1() {
    let mut arr = [1, 2, 3, 4, 5, 6];
    let temp = &mut arr[..];
    temp[0] = 88;
    println!("{:?}", arr);
    _modify_array(&mut arr[1..]);
    println!("{:?}", arr);
}

fn _modify_array(slice: &mut [i32]) {
    slice[0] = 99
}

fn main() {
    _temp_1();
}