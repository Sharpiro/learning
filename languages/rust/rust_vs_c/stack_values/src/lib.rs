#[cfg(test)]
mod test {
    #[test]
    fn test_something() {
        let arr = [1, 2, 3];
        dbg!(arr);
        mod_array_fail(arr);
        dbg!(arr);
    }

    fn mod_array_fail(mut arr: [i32; 3]) {
        arr[0] = 99;
    }
}
