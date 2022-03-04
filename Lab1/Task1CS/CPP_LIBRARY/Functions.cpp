#include "Functions.h"
#include <iostream>

extern "C" TO_EXPORT void hello()
{
	std::cout << "HELLO FROM C++\n";
}

extern "C" TO_EXPORT int sum(int a, int b)
{
	return a + b;
}

extern "C" TO_EXPORT int diff(int a, int b)
{
	return a - b;
}