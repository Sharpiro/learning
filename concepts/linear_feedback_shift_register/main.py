def linear_feedback_shift_register(shifter:int):
    first_pass = True
    val = 1
    while val != 1 or first_pass:
        first_pass = False
        print(str(hex(val))[2:])
        zero_bit = val & 1
        two_bit = (val & 4) >> shifter
        xor_op = zero_bit ^ two_bit
        val >>= 1
        val |= (xor_op << 3)

shifter = 2
linear_feedback_shift_register(shifter)
