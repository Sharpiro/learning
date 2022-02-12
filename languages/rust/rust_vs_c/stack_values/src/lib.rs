#[cfg(test)]
mod test {
    #[test]
    fn test_mod_array() {
        let arr = [1, 2, 3];
        dbg!(arr);
        mod_array_fail(arr);
        dbg!(arr);

        let mut arr = [1, 2, 3];
        dbg!(arr);
        mod_by_ptr(&mut arr);
        dbg!(arr);
    }

    fn mod_array_fail(mut arr: [i32; 3]) {
        arr[0] = 99;
    }

    fn mod_by_ptr(arr: &mut [i32]) {
        arr[0] = 99;
    }

    #[test]
    fn test_arr_copy_simple() {
        let mut arr = [1, 2, 3];
        let arr_two = arr;
        arr[0] = 99;

        assert_eq!(arr, [99, 2, 3]);
        assert_eq!(arr_two, [1, 2, 3]);
    }

    #[test]
    fn test_arr_copy_complex() {
        let arr = [1, 2, 3];
        println!("{:?}", arr);
        let a_ptr = &arr;
        let a_ptr_ptr = &a_ptr;

        let first_deref = *a_ptr_ptr;
        let mut second_deref = *first_deref;
        second_deref[0] = 99;

        println!("{:?}", arr);
        assert_eq!(arr, [1, 2, 3]);
    }
}
