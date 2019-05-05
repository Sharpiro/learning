def increment_indexes(indexes):
    carry = 0
    indexes[len(indexes)-1] += 1
    for i in range(len(indexes)-1, -1, -1):
        indexes[i] += carry
        carry = 0
        if indexes[i] == len(indexes):
            indexes[i] = 0
            carry = 1


def has_dupes(indexes):
    sorted = indexes.copy()
    sorted.sort()
    for i in range(len(sorted)-1):
        if sorted[i] == sorted[i+1]:
            return True

    return False


def get_permutations():
    permutations = []
    input = "abcd"
    indexes = list(i for i in range(len(input)))
    uniques = 0
    ops = 0
    for _ in range(len(input)**len(input)):
        ops += 1
        if (has_dupes(indexes)):
            increment_indexes(indexes)
            continue
        uniques += 1
        permutation = "".join(input[uniques] for uniques in indexes)
        permutations.append(permutation)
        increment_indexes(indexes)

    return (permutations, indexes, uniques, ops)


permutation_data = get_permutations()
print(permutation_data[0])
print(permutation_data[1])
print(permutation_data[2], "uniques")
print((permutation_data[3]), "total operations")
