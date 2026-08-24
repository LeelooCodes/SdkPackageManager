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

int __cdecl ValidatePackageName(
	const wchar_t* packageName
) 
{
	if (packageName == nullptr) 
	{
		return 0;
	}

	if (*packageName == L'\0')
	{
		return 0;
	}

	constexpr int maxLength = 64;

	int length = 0;
	const wchar_t* current = packageName;		//creates another pointer pointing at the beginning of the same character buffer.

	while (*current != L'\0')		//reads the character at the memory address current points to
	{
		++length;

		if (length > maxLength)
		{
			return 0;
		}

		++current;		//moves the pointer to the next wchar_t
	}

	return 1;
}