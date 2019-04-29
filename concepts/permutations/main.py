from pathlib import Path
txt = Path('expected.txt').read_text()
expected_values = list(list(index_0) for index_0 in txt.split("\n"))


def updateShift(shift):
    shift[3] += 1
    if shift[3] == 2:
        shift[3] = 0
        shift[2] += 1
    if shift[2] == 3:
        shift[2] = 0
        shift[1] += 1
    if shift[1] == 7:
        shift[1] = 0
        shift[0] += 1
    # if shift[0] == 25:
    #     shift[0] = 0
    #     shift[-1] = 1


def modify_list(original_list, old_index, new_index):
    if (old_index == new_index):
        return original_list.copy()
    left_list = original_list[:old_index]
    right_list = original_list[old_index + 1:]
    new_list = left_list + right_list
    new_list.insert(new_index, original_list[old_index])
    return new_list


# def traverse(input_list):
#     if len(input_list) != 2:
#         traverse(input_list[1:])
#     temp = 2
#     pass

actual = []
parent_zero = ["a", "b", "c", "d"]
# parent_zero = ["a", "b", "c"]
# index_0 = 0
# index_1 = 0
# index_2 = 0
# index_3 = 0


# def temp2(input_list, counter):
#     # if counter == 5:
#     #     return
#     if (len(input_list) == 2):
#         return [input_list, input_list[::-1]]

#     # print(input_list)
#     x = temp2(input_list[1:], counter + 1)
#     y = list(input_list[0:1] + i for i in x)
#     return y
# temp = [24, 6, 2, 1]
indexes = [0, 0, 0, 0]


# def temp3(input_list, x, y, max, counter):
def temp3(input_list, counter):
    if counter == 5:
        return

    # print current
    print(input_list)

    # update
    updateShift(indexes)

    x = 0
    y = 0
    if (indexes[3] == 0):
        return
    if (indexes[3] == 1):
        x = 3
        y = 2
    # elif (indexes[2] == 1):
    #     x = 3
    #     y = 2

    # call recurs
    temp_list = modify_list(input_list, x, y)
    temp3(temp_list, counter + 1)
    pass


def temp():
    for i in range(1):
        parent_one = modify_list(parent_zero, i, 0)
        actual.append(parent_one)
        text2 = modify_list(parent_one, len(parent_zero) - 1, len(parent_zero)-2)
        actual.append(text2)
        parent_two = modify_list(parent_one, 2, 1)
        actual.append(parent_two)
        text2 = modify_list(parent_two, len(parent_zero) - 1, len(parent_zero)-2)
        actual.append(text2)
        parent_two = modify_list(parent_one, 3, 1)
        actual.append(parent_two)
        text2 = modify_list(parent_two, len(parent_zero) - 1, len(parent_zero)-2)
        actual.append(text2)


def shift_easy(data):
    carry = 0
    data[len(data)-1] += 1
    for i in range(len(data)-1, -1, -1):
        data[i] += carry
        carry = 0
        if data[i] == len(data):
            data[i] = 0
            carry = 1


def has_dupes(data):
    sorted = data.copy()
    sorted.sort()
    for i in range(len(sorted)-1):
        if sorted[i] == sorted[i+1]:
            return True

    return False


def easy():
    actual = []
    temp = "abcd"
    data = list(i for i in range(len(temp)))
    uniques = 0
    ops = 0
    for _ in range(len(temp)**len(temp)):
        ops += 1
        if (has_dupes(data)):
            shift_easy(data)
            continue
        uniques += 1
        print("".join(temp[uniques] for uniques in data))
        actual.append("".join(temp[uniques] for uniques in data))
        # if len(actual) == 720:
        #     break
        shift_easy(data)
    print(data)
    print(uniques, "uniques")
    print((ops), "total operations")
    return actual


# print(has_dupes([3, 0, 0, 0]))
actual = easy()
# temp()
# temp3(parent_zero, 3, 2, 1, 0)
# temp3(parent_zero, 0)
# print(xyz)

# for x in actual:
#     print(x)
# assert txt == "\n".join(actual)
# assert expected_values[:len(actual)] == actual
# print(txt == "\n".join(actual))
# print(actual)
# print(expected_values)

# temp = [24, 6, 2, 1]
# shift = [0, 0, 0, 0]
# for i in range(6):

#     print(shift)
#     # print(i)
#     updateShift()
#     pass
