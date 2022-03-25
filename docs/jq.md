# JQ - JSON Query

## sample data

`a.json`

```json
[
  {
    "x": 2
  },
  {
    "x": 1
  },
  {
    "x": 3
  },
  {
    "x": 1
  }
]
```

`b.json`

```json
[
  {
    "y": "b"
  },
  {
    "y": "a"
  },
  {
    "y": "c"
  },
  {
    "y": "a"
  }
]
```

## filtering

```sh
jq '[.[] | select(.x == 1)]' a.json
```

## projection

```sh
jq  ".[] | {y: (.x + 1), z: 99, a: (null != null), b: (true == false)}" a.json
```

## projection append

```sh
jq  ".[] += {z: 99}" a.json
```

## common filters

```sh
jq "[.[].x] | sort | unique | add/length" a.json
```

- add
- sort
- unique
- max
- min
- length

## merge json arrays

- `--slurp/-s`
  - run filter after reading all inputs instead of on each input

```sh
jq -s "." a.json b.json
```

## merge objects

```sh
jq  -s '.[0][0] * .[1][0]' a.json b.json
```

## zip/transpose arrays

```sh
jq -s ". | transpose" a.json b.json
```

## zip array and merge objects

```sh
jq -s '. | transpose[] | .[0] * .[1]' a.json b.json
# or
jq -s '. | transpose[] | {x: .[0].x, y: .[1].y}' a.json b.json
# or
jq -s '(. | transpose[]) as [$a, $b] | {x: $a.x, b: $b.y}' a.json b.json
```
