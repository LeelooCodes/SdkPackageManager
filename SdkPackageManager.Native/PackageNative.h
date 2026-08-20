#pragma once


extern "C" __declspec(dllexport)
int __cdecl CompareVersions(
	int firstMajor,
	int firstMinor,
	int firstPatch,
	int secondMajor,
	int secondMinor,
	int secondPatch
);