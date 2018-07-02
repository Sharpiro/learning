package main

import "fmt"

type Vertex struct {
	X int
	Y int
}

func main() {
	v := Vertex{X: 0}

	handleValue(v)
	fmt.Println(v.X)

	handlePointer(&v)
	fmt.Println(v.X)

	fmt.Printf("\n%p\n", &v)
	// handleValue(v)
}

func handleValue(vertex Vertex) {
	vertex = Vertex{X: 10}
	fmt.Printf("%p\n", &vertex)
	// fmt.Println(vertex)
}

func handlePointer(vertex *Vertex) {
	*vertex = Vertex{X: 20}
	fmt.Printf("\n%p\n", &vertex)
}
