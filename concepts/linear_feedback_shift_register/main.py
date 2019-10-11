def linear_feedback_shift_register(tap_index: int, bits: int):
    first_pass = True
    val = 1
    tap_mask = 2**tap_index
    counter = 0
    for _ in range(0, 3):
    # while val != 1 or first_pass:
        first_pass = False
        print(str(hex(val))[2:])
        counter+=1
        zero_bit = val & 1
        two_bit = (val & tap_mask) >> tap_index
        xor_op = zero_bit ^ two_bit
        val >>= 1
        val |= (xor_op << (bits - 1))
    print("count", counter)

# capture right-most output bit
# shift all right
# if output bit is 1:
#     xor output bit with tap indexes
# or output bit with left-most input bit
def galois_linear_feedback_shift_register(tap_index: int, bits: int):
    val = 1
    for _ in range(0, 5):
        print(val, bin(val))
        # print(str(hex(val))[2:])
        last_bit = val & 1
        val >>= 1

        if (last_bit == 1):
            tap_value = (val & 2**tap_index) >> tap_index
            xor_result = last_bit ^ tap_value
            val |= (xor_result << tap_index)
            val |= 8
    

tap_index = 2
bits = 4
linear_feedback_shift_register(tap_index, bits)
galois_linear_feedback_shift_register(tap_index, bits)

print("\nbits", bits)
