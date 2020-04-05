# sudo dnf install python3-tkinter // required for viewing charts

import matplotlib.pyplot as plt

def c_shift_right_64_bit(num, shift):
    c_shifter = shift % 64
    return num >> c_shifter

total_halvings = 256
x = list(range(0, total_halvings))
y = []

coin = 100_000_000
monetary_base = 0
reward = 50 * coin
curr_reward = reward
blocksPerHalving = 210_000
for i in x:
    display_y = monetary_base / coin / 1_000_000
    y.append(display_y)
    print(display_y)

    rewards_per_halving = curr_reward * blocksPerHalving
    monetary_base += rewards_per_halving
    # curr_reward = reward >> i
    curr_reward = c_shift_right_64_bit(reward, i + 1)


fig, ax = plt.subplots()
x = [2009 + i * 4 for i in x]
ax.set_title("monetary base")
ax.set_xlabel('year')
ax.set_ylabel('bitcoin (millions)')
ax.plot(x, y)

fig.savefig("test.png")
plt.show()
