#include "CppLibrary.h"
#include <iostream>

JNIEXPORT void JNICALL Java_CppLibrary_hello
  (JNIEnv *, jobject) {
  	std::cout << "HELLO FROM C++\n";
  }

JNIEXPORT jint JNICALL Java_CppLibrary_sum
  (JNIEnv *, jobject, jint a, jint b){
  	return a + b;
  }


JNIEXPORT jint JNICALL Java_CppLibrary_diff
  (JNIEnv *, jobject, jint a, jint b) {
  	return a - b;
  }

