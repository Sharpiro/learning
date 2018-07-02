#include <iostream>

struct Vector
{
	int X;
	int Y;
};

void handleValue(Vector vector) {
	vector = Vector();
	vector.X = 10;
	std::cout << &vector << std::endl;
	/*std::cout << vector.X << std::endl;*/
}

void handlePointer(Vector* vector) {
	//vector = nullptr;
	//auto tempVector = Vector();
	*vector = Vector();
	//vector = new Vector();
	vector->X = 20;
	std::cout << std::endl << &vector << std::endl;
	/*std::cout << vector->X << std::endl;*/
	//return &tempVector;
}


void handleReference(Vector& vector) {
	vector = Vector();
	vector.X = 40;

	std::cout << std::endl << &vector << std::endl;
	//std::cout << vector.X << std::endl;
}

int main() {
	Vector vector = Vector();
	vector.X = 0;

	handleValue(vector);
	std::cout << vector.X << std::endl;

	handlePointer(&vector);
	std::cout << vector.X << std::endl;

	handleReference(vector);
	std::cout << vector.X << std::endl;

	std::cout << std::endl << &vector << std::endl;
	//std::cout << vector.X << std::endl;
	std::cin.get();
}