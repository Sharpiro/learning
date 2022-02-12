/**
 * @param {number[]} arr (C - number *) (Rust - &mut [i32])
 */
function mod_by_ptr(arr) {
  arr[0] = 99;
}

/** @typedef { { arr: number[] } } Ctnr */

/**
 * @param {Ctnr} ctnr (C - Ctnr *) (Rust - &mut Ctnr)
 */
function mod_by_another_pointer(ctnr) {
  // ctnr = {};
  ctnr.arr[0] = 99;
}

console.log("---mod_by_ptr---");
const arr = [1, 2, 3];
console.log(arr);
mod_by_ptr(arr); // (&arr)
console.log(arr);

console.log("---mod_by_another_ptr---");
const arr_temp = [1, 2, 3];
const arr_container = { arr: arr_temp };
console.log(arr_temp);
mod_by_another_pointer(arr_container); // (&arr_container)
console.log(arr_temp);
