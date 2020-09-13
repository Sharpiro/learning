#! /bin/python
# cspell:disable

import socket

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect(("localhost", 8080))
s.sendall(b"hello\n")
# buffer = s.recv(1_000)
print(s.recv(1_000))
s.sendall(b"get_something\n")
buffer = s.recv(1_000)[:-1]
big_num = int.from_bytes(buffer, "big")
print(len(buffer))
print(list(buffer))
print(big_num)
# int.from_bytes()
# print(int.from_bytes([0, 0, 1, 255], "big"))
