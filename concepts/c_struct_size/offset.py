# cspell:disable

# struct field must start at multiple of its size
# struct size must be multiple of largest field size

from typing import List


def is_aligned(index, size):
    res = index % size
    is_aligned = res == 0
    field_offset = size - res
    return (is_aligned, field_offset)


# byte
assert(is_aligned(0, 1)[0] == True)
assert(is_aligned(1, 1)[0] == True)
assert(is_aligned(2, 1)[0] == True)
assert(is_aligned(3, 1)[0] == True)
assert(is_aligned(4, 1)[0] == True)
assert(is_aligned(5, 1)[0] == True)
assert(is_aligned(6, 1)[0] == True)
assert(is_aligned(7, 1)[0] == True)
assert(is_aligned(8, 1)[0] == True)

# short
assert(is_aligned(0, 2)[0] == True)
assert(is_aligned(1, 2)[0] == False)
assert(is_aligned(2, 2)[0] == True)
assert(is_aligned(3, 2)[0] == False)
assert(is_aligned(4, 2)[0] == True)
assert(is_aligned(5, 2)[0] == False)
assert(is_aligned(6, 2)[0] == True)
assert(is_aligned(7, 2)[0] == False)
assert(is_aligned(8, 2)[0] == True)

# int
assert(is_aligned(0, 4)[0] == True)
assert(is_aligned(1, 4)[0] == False)
assert(is_aligned(2, 4)[0] == False)
assert(is_aligned(3, 4)[0] == False)
assert(is_aligned(4, 4)[0] == True)
assert(is_aligned(5, 4)[0] == False)
assert(is_aligned(6, 4)[0] == False)
assert(is_aligned(7, 4)[0] == False)
assert(is_aligned(8, 4)[0] == True)

# long
assert(is_aligned(1, 8)[0] == False)
assert(is_aligned(1, 8)[1] == 7)


def get_struct_size(struct: List[int]):
    max_field_size = 1
    total_bytes = 0
    for size_bits in struct:
        size_bytes = size_bits // 8
        max_field_size = max(max_field_size, size_bytes)
        (field_aligned, field_offset) = is_aligned(total_bytes, size_bytes)
        if not field_aligned:
            total_bytes += field_offset
        total_bytes += size_bytes
    (struct_aligned, struct_offset) = is_aligned(total_bytes, max_field_size)
    if not struct_aligned:
        total_bytes += struct_offset
    return total_bytes


struct1 = [8, 8]
struct2 = [8, 16]
struct3 = [8, 8, 16, 32]
struct4 = [8, 8, 16, 8]
struct5 = [8, 8, 16, 32, 8]
struct6 = [8, 64, 32]
struct7 = [64, 32, 8]
struct8 = [8, 8, 8, 16, 16]

assert(get_struct_size(struct1) == 2)
assert(get_struct_size(struct2) == 4)
assert(get_struct_size(struct3) == 8)
assert(get_struct_size(struct4) == 6)
assert(get_struct_size(struct5) == 12)
assert(get_struct_size(struct6) == 24)
assert(get_struct_size(struct7) == 16)
assert(get_struct_size(struct8) == 8)
