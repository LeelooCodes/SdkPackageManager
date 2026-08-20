#include "pch.h"
#include "PackageNative.h"

int __cdecl CompareVersions(
	int firstMajor,
	int firstMinor,
	int firstPatch,
	int secondMajor,
	int secondMinor,
	int secondPatch
)
{
	if (firstMajor != secondMajor) {
		return firstMajor < secondMajor ? -1 : 1;
	}

	if (firstMinor != secondMinor) {
		return firstMinor < secondMinor ? -1 : 1;
	}

	if (firstPatch != secondPatch) {
		return firstPatch < secondPatch ? -1 : 1;
	}

	return 0;
}