#ifndef FUNCTIONS_H
#define FUNCTIONS_H

#define TO_EXPORT __declspec(dllexport)

extern "C" TO_EXPORT void hello();
extern "C" TO_EXPORT int sum(int a, int b);
extern "C" TO_EXPORT int diff(int a, int b);

#endif