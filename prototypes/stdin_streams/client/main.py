#! /bin/python
# cspell:disable

import random
import struct
import socket

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect(("localhost", 8080))
s.sendall(b"hello\n")
print(s.recv(1_000))


# s.sendall(b"get_something\n")
# buffer = s.recv(1_000)[:-1]
# big_num = int.from_bytes(buffer, "big")
# print(len(buffer))
# print(list(buffer))
# print(big_num)
# int.from_bytes()
# print(int.from_bytes([1, 0, 1, 255], "big"))


def egcd(a, b):
    if a == 0:
        return (b, 0, 1)
    else:
        gcd, x, y = egcd(b % a, a)
        _temp = b//a * x
        return (gcd, y - (b//a) * x, x)


def simple_hash_u32(x):
  buffer = struct.pack(">i", x)
  return simple_hash(buffer)


def simple_hash(buffer):
  MASK_U32 = 2**32-1
  hash_address = 5381

  for byte in buffer:
    hash_address = (((hash_address << 5) & MASK_U32) + hash_address & MASK_U32) + byte 
    print("ha:", hash_address)
  return hash_address


p = 37
q = 53
N = p*q
phi_N = (p-1)*(q-1)
e = 17
d = egcd(e, phi_N)[1]
x = 1227
y = 80
# x = random.randrange(N)
# y = x**e % N
k = simple_hash_u32(x)
# s.sendall(y)
# print(bytes(812))
print("x:", x)
print("y:", y)
print(list(struct.pack(">i", y)))
print("k", k)
s.sendall(struct.pack(">i", y))


# def is_prime(n):
#     skip_map = set()
#     for i in range(2, n):
#         # if i in skip_map:
#         #     continue
#         quotient, remainder = divmod(n, i)
#         if remainder == 0:
#             # skip_map.add(quotient)
#             print(i)


# is_prime(52)
